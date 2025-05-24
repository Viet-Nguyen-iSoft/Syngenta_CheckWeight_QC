using Irony.Parsing;
using SyngentaWeigherQC.Models;
using SyngentaWeigherQC.Responsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Production = SyngentaWeigherQC.Models.Production;

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

    public async Task AddRange(List<Production> productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.AddRange(productions);
      }
    }

    public async Task AddRange(List<ShiftLeader> shiftLeaders)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.AddRange(shiftLeaders);
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
    public async Task<bool> Update(ConfigSoftware configSoftware)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ConfigSoftware, ConfigDBContext>(context);
        return await repo.Update(configSoftware);
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


    public async Task<List<ShiftLeader>> GetList(bool contain_isDelete=false)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryShiftLeader(context);
        return await repo.GetList(contain_isDelete);
      }
    }
    public async Task<bool> Update(ShiftLeader shiftLeader)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        return await repo.Update(shiftLeader);
      }
    }
    public async Task<ShiftLeader> Add(ShiftLeader shiftLeader)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        return await repo.Add(shiftLeader);
      }
    }

    public async Task<HistoricalChangeMasterData> Add(HistoricalChangeMasterData historicalChangeMasterData)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<HistoricalChangeMasterData, ConfigDBContext>(context);
        return await repo.Add(historicalChangeMasterData);
      }
    }

    public async Task<List<Roles>> LoadAllRole()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Roles, ConfigDBContext>(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task<List<Production>> LoadAllProducts(bool is_contain_isDelete=false)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryProducts(context);
        return await repo.GetList(is_contain_isDelete);
      }
    }

    public async Task<List<DatalogWeight>> LoadAllDatalogWeight(int LineId, DateTime from, DateTime to, int shiftId=0)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryDatalogWeight(context);
        return await repo.GetProductByDate(LineId, from, to, shiftId);
      }
    }


    public async Task<List<ConfigSoftware>> LoadConfigSoftware()
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new ResponsitoryConfigSoftware(context);
        return await repo.GetAllAsync();
      }
    }

    public async Task UpdateRange(List<Roles> roles)
    {
      using (var context = new ConfigDBContext())
      {
        GenericRepository<Roles, ConfigDBContext> repo = new ResponsitoryRoles(context);
        await repo.UpdateRange(roles);
      }
    }


    public async Task UpdateRange(List<ShiftLeader> shiftLeaders)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<ShiftLeader, ConfigDBContext>(context);
        await repo.UpdateRange(shiftLeaders);
      }
    }
  }
}
