using Irony.Parsing;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.DTO;
using SyngentaWeigherQC.Helper;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static SyngentaWeigherQC.eNum.enumSoftware;
using Production = SyngentaWeigherQC.Models.Production;

namespace SyngentaWeigherQC.UI.UcUI
{
  public partial class UcReport : UserControl
  {
    public UcReport()
    {
      InitializeComponent();
    }

    public void SetTitle(string titleReport)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetTitle(titleReport);
        }));
        return;
      }

      this.lbTitle.Text = titleReport;
    }


    public void SetInfor(ExcelClassInforProduct excelClassInforProduct)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetInfor(excelClassInforProduct);
        }));
        return;
      }

      this.ucTemplateExcel1.UpdateInforProductUI(excelClassInforProduct);
    }

    public void StatisticalProductUI(DataByProduction dataByProduction)
    {
      ucTemplateExcel1.ClearSumary();
      eEvaluate eEvaluate = eEvaluate.None;

      if (dataByProduction.DataByProductionByShifts?.Count > 0)
      {
        eEvaluate = eEvaluate.Pass;

        foreach (var item in dataByProduction.DataByProductionByShifts)
        {
          var rs = AppCore.Ins.SumaryDTO(item.DatalogWeights);
          foreach (var item2 in rs)
          {
            ucTemplateExcel1.SetValueSumary(item2);

            if (item2.eEvaluate == eEvaluate.Fail)
            {
              eEvaluate = eEvaluate.Fail;
            }
          }
        }
      }

      //Kết quả
      this.ucTemplateExcel1.SetResultFinal(eEvaluate);
    }

    public void SetChartHistogram(List<DatalogWeight> datalogWeights, Production production)
    {
      ucChartHistogram1.SetDataChart(datalogWeights, production);
    }




    public void SetChartLine(ChartLineData chartLineData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetChartLine(chartLineData);
        }));
        return;
      }
      this.ucChartLine1.SetDataChart(chartLineData);
    }


    private int _colShift = 0;
    private int _colNo = 1;
    private int _colDatetime = 2;
    private int _colDataWeight = 3; //.....
    private int _colAvgRaw = 13;
    private int _colAvgTotal = 14;
    private int _colEvaluate = 15;
    public void SetDataTable(List<TableDatalogDTO> tableDatalogDTOs, Production production)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataTable(tableDatalogDTOs, production);
        }));
        return;
      }

      if (tableDatalogDTOs.Count() > 0)
      {
        tableDatalogDTOs.Reverse();
        foreach (var item in tableDatalogDTOs)
        {
          dataGridView1.Rows.Insert(0);
          int cnt_number_raw = item.DatalogWeights.Count();

          dataGridView1.Rows[0].Cells[_colShift].Value = item.Shift;
          dataGridView1.Rows[0].Cells[_colNo].Value = item.No;
          dataGridView1.Rows[0].Cells[_colDatetime].Value = item.DateTime;

          for (int i = 1; i <= cnt_number_raw; i++)
          {
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Value = item.DatalogWeights.ElementAtOrDefault(i - 1).Value;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Tag = item.DatalogWeights.ElementAtOrDefault(i - 1);

            var color = AppCore.Ins.EvaluateRetureColor(item.DatalogWeights.ElementAtOrDefault(i - 1), production);
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.BackColor = color.Item1;
            dataGridView1.Rows[0].Cells[_colDataWeight - 1 + i].Style.ForeColor = color.Item2;
          }

          dataGridView1.Rows[0].Cells[_colAvgRaw].Value = item.AvgRaw;
          dataGridView1.Rows[0].Cells[_colAvgTotal].Value = item.AvgTotal;
          dataGridView1.Rows[0].Cells[_colEvaluate].Value = eNumHelper.GetDescription(item.eEvaluate);

          dataGridView1.Rows[0].Cells[_colEvaluate].Style.BackColor = item.eEvaluate == eEvaluate.Pass ? Color.Green : Color.Red;
          dataGridView1.Rows[0].Cells[_colEvaluate].Style.ForeColor = Color.White;
        }
      }
    }

  }
}
