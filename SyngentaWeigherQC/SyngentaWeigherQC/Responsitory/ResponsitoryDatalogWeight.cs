using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitoryDatalogWeight : GenericRepository<DatalogWeight, ConfigDBContext>
  {
    public ResponsitoryDatalogWeight(DbContext context) : base(context)
    {

    }

    public async Task<List<DatalogWeight>> GetList()
    {
      return await this.Context.Set<DatalogWeight>()
        .Include(x => x.Production)
        .Include(x => x.InforLine)
        .ToListAsync();
    }

    public async Task<DatalogWeight> GetProductById(int Id)
    {
      return await this.Context.Set<DatalogWeight>()
        .Include(x=>x.Production)
        .Where(x => x.Id== Id)
        .FirstOrDefaultAsync();
    }

  }
}
