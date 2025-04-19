using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Control
{
  public partial class AppCore
  {
    public async Task ReloadInforLine()
    {
      //Line
      _listInforLine = await LoadInforLines();
    }

    public async Task<List<InforLine>> LoadInforLines()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryInforLines(context);
        return await repo.GetList();
      }
    }

    public async Task<bool> Update(Production production)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        return await repo.Update(production);
      }
    }
    public async Task<bool> UpdateRange(List<Production> productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        return await repo.UpdateRange(productions);
      }
    }

    public async Task<bool> Update(InforLine inforLine)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<InforLine, ConfigDBContext>(context);
        return await repo.Update(inforLine);
      }
    }

    public async Task<bool> Update(DatalogWeight datalogWeight)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<DatalogWeight, ConfigDBContext>(context);
        return await repo.Update(datalogWeight);
      }
    }


    public async Task AddRangeProducts(List<Production> productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.AddRange(productions);
      }
    }

    public async Task<DatalogTare> Add(DatalogTare datalogTare)
    {
      try
      {
        using (var context = new ConfigDBContext())
        {
          var repo = new GenericRepository<DatalogTare, ConfigDBContext>(context);
          return await repo.Add(datalogTare);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      
    }


    public async Task<DatalogWeight> Add(DatalogWeight datalogWeight)
    {
      try
      {
        using (var context = new ConfigDBContext())
        {
          var repo = new GenericRepository<DatalogWeight, ConfigDBContext>(context);
          return await repo.Add(datalogWeight);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      
    }


    public async Task<List<ShiftLeader>> LoadAllShiftLeader()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task<List<DatalogWeight>> LoadAllDatalogWeight()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalogWeight(context);
        return await repo.GetList();
      }
    }


    public async Task<List<Production>> LoadAllProducts()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryProducts(context);
        return await repo.GetAllAsync();
      }
    }


  }
}
