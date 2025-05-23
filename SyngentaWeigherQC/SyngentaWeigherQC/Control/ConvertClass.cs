using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using static SyngentaWeigherQC.eNum.enumSoftware;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    public StatisticalData CvtDatalogWeightToStatisticalData(List<DatalogWeight> datalogWeights, Production production)
    {
      try
      {
        var numbersOver = (datalogWeights != null) ? datalogWeights?.Count(x => x.Value > production.UpperLimitFinal) : 0;
        var numbersLower = (datalogWeights != null) ? datalogWeights?.Count(x => x.Value < production.LowerLimitFinal) : 0;

        var listValue = datalogWeights.Select(x => x.Value).ToList();
        double stdev = MathHelper.Stdev(listValue, 3);
        double average = Math.Round(listValue.Average(), 3);

        double target = production.StandardFinal;
        double min = production.LowerLimitFinal;
        double max = production.UpperLimitFinal;
        //Đánh giá
        eEvaluate eEvaluate = (average >= production.StandardFinal && average <= production.UpperLimitFinal) ? eEvaluate.Pass : eEvaluate.Fail;

        int totalSamples = datalogWeights.Count();
        int numberSamplesOver = (int)datalogWeights?.Count(x => x.Value > max);
        int numberSamplesLower = (int)datalogWeights?.Count(x => x.Value < min);

        double rateError = (totalSamples != 0) ? ((double)((numberSamplesOver + numberSamplesLower) * 100) / (double)(totalSamples)) : 0;
        rateError = Math.Round(rateError, 2);

        double rateLoss = (average > target) ? Math.Round((((average - target) * 100) / target), 2) : 0;

        StatisticalData statisticalData = new StatisticalData()
        {
          Shift = datalogWeights.FirstOrDefault().Shift.Name,
          Stdev = stdev,
          Average = average,
          Target = production.StandardFinal,
          eEvaluate = eEvaluate,
          TotalSample = datalogWeights.Count(),
          NumberSampleOver = numberSamplesOver,
          NumberSampleLower = numberSamplesLower,
          RateError = rateError,
          RateLoss = rateLoss,
        };

        return statisticalData;
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        return new StatisticalData();
      }
    }

    public List<TableDatalogDTO> ConvertToDTOList(List<DatalogWeight> dataList)
    {
      var result = new List<TableDatalogDTO>();

      if (dataList == null || dataList.Count == 0)
        return result;

      int groupSize = 10;
      dataList = dataList.OrderBy(x => x.Id).ToList();

      // Nhóm theo ShiftId
      var groupedByShift = dataList.GroupBy(x => x.ShiftId);

      foreach (var shiftGroup in groupedByShift)
      {
        var items = shiftGroup.ToList();
        int totalGroups = (int)Math.Ceiling((double)items.Count / groupSize);

        int productId = (int)(items.FirstOrDefault()?.ProductionId ?? 0);
        Production production = _listProductsForStation?.FirstOrDefault(x => x.Id == productId);

        for (int i = 0; i < totalGroups; i++)
        {
          var group = items
              .Skip(i * groupSize)
              .Take(groupSize)
              .ToList();

          if (group.Count == 0) continue;

          double avgRaw = Math.Round(group.Average(x => x.Value), 2);

          var dto = new TableDatalogDTO
          {
            No = result.Count + 1,
            Shift = group.FirstOrDefault()?.Shift?.Name,
            DateTime = group.FirstOrDefault()?.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
            DatalogWeights = group,
            AvgRaw = avgRaw,
            AvgTotal = 0, // sẽ cập nhật sau
            eEvaluate = EvaluateData(avgRaw, production)
          };

          result.Add(dto);
        }
      }

      // Tính lại AvgTotal chung từ tất cả các AvgRaw
      if (result.Count > 0)
      {
        double averageTotal = Math.Round(result.Select(x => x.AvgRaw).Average(), 3);
        result.ForEach(x => x.AvgTotal = averageTotal);
      }

      return result;
    }

    //public List<TableDatalogDTO> ConvertToDTOList(List<DatalogWeight> dataList)
    //{
    //  var result = new List<TableDatalogDTO>();

    //  if (dataList == null || dataList.Count == 0)
    //    return result;

    //  int groupSize = 10;
    //  int totalGroups = (int)Math.Ceiling((double)dataList.Count / groupSize);

    //  dataList = dataList?.OrderBy(x => x.Id).ToList();

    //  int productId = (int)dataList.FirstOrDefault().ProductionId;
    //  Production production = _listAllProductsBelongLine?.Where(x => x.Id == productId).FirstOrDefault();

    //  for (int i = 0; i < totalGroups; i++)
    //  {
    //    var group = dataList
    //        .Skip(i * groupSize)
    //        .Take(groupSize)
    //        .ToList();

    //    if (group.Count == 0) continue;

    //    var dto = new TableDatalogDTO
    //    {
    //      No = i + 1,
    //      Shift = group.First()?.Shift?.Name,
    //      DateTime = group.First().CreatedAt.ToString("yyyy-MM-dd HH:mm:ss") ?? "",
    //      DatalogWeights = group,
    //      AvgRaw = Math.Round(group.Average(x => x.Value), 2),
    //      AvgTotal = Math.Round(dataList.Average(x => x.Value), 2),
    //      eEvaluate = EvaluateData(Math.Round(group.Average(x => x.Value), 2), production)
    //    };

    //    result.Add(dto);
    //  }

    //  if (result.Count>0)
    //  {
    //    double averageTotal =Math.Round( result.Select(x => x.AvgRaw).Average(),3);
    //    result.ForEach(x => x.AvgTotal = averageTotal);
    //  }  


    //  return result;
    //}

    public ChartLineData CvtDatalogWeightToChartLine(List<DatalogWeight> datalogWeights, Production production)
    {
      try
      {
        var result = new ChartLineData();

        if (datalogWeights == null || datalogWeights.Count() == 0) return result;
        List<double> averageList = new List<double>();

        int groupSize = 10;
        int totalGroups = (int)Math.Ceiling((double)datalogWeights.Count / groupSize);
        datalogWeights = datalogWeights.OrderBy(x => x.Id).ToList();
        for (int i = 0; i < totalGroups; i++)
        {
          double average = datalogWeights
              .Skip(i * groupSize)
              .Take(groupSize)
              .Average(dw => dw.Value);

          average = Math.Round(average, 2);
          averageList.Add(average);
        }

        return new ChartLineData()
        {
          Min = production.LowerLimitFinal,
          Max = production.UpperLimitFinal,
          Target = production.StandardFinal,
          Average = Math.Round( averageList.Average(),2),
          AverageRaw = averageList,
        };
      }
      catch (Exception ex)
      {
        LoggerHelper.LogErrorToFileLog(ex);
        return new ChartLineData();
      }
    }
  }
}
