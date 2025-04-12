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
        return await repo.GetAsync();
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


    public async Task AddRangeProducts(List<Production> productions)
    {
      using (var context = new ConfigDBContext())
      {
        var repo = new GenericRepository<Production, ConfigDBContext>(context);
        await repo.AddRange(productions);
      }
    }
  }
}
