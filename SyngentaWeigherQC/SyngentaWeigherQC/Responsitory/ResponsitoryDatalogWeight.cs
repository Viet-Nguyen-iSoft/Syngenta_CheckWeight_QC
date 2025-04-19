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
        .Include(x => x.Shift)
        .ToListAsync();
    }

    public async Task<DatalogWeight> GetProductById(int Id)
    {
      return await this.Context.Set<DatalogWeight>()
        .Include(x=>x.Production)
        .Where(x => x.Id== Id)
        .FirstOrDefaultAsync();
    }

    public async Task<List<DatalogWeight>> GetProductByDate(int LineId, DateTime from, DateTime to, int shiftId)
    {
      if (LineId==0)
      {
        if (shiftId==0)
        {
          return await this.Context.Set<DatalogWeight>()
            .Include(x => x.Production)
            .Include(x => x.Shift)
            .Include(x => x.InforLine)
            .Where(x => x.CreatedAt.Value.Date >= from.Date &&
                        x.CreatedAt.Value.Date <= to.Date
                        )
            .ToListAsync();
        }
        else
        {
          return await this.Context.Set<DatalogWeight>()
            .Include(x => x.Production)
            .Include(x => x.Shift)
            .Include(x=>x.InforLine)
            .Where(x => x.CreatedAt.Value.Date >= from.Date &&
                        x.CreatedAt.Value.Date <= to.Date &&
                        x.ShiftId == shiftId
                        )
            .ToListAsync();
        }  
      }
      else
      {
        if (shiftId == 0)
        {
          return await this.Context.Set<DatalogWeight>()
          .Include(x => x.Production)
          .Include(x => x.Shift)
          .Include(x=>x.InforLine)
          .Where(x => x.CreatedAt.Value.Date >= from.Date &&
                      x.CreatedAt.Value.Date <= to.Date &&
                      x.InforLineId == LineId
                      )
          .ToListAsync();
        }
        else
        {
          return await this.Context.Set<DatalogWeight>()
          .Include(x => x.Production)
          .Include(x => x.Shift)
          .Include(x => x.InforLine)
          .Where(x => x.CreatedAt.Value.Date >= from.Date &&
                      x.CreatedAt.Value.Date <= to.Date &&
                      x.ShiftId == shiftId && 
                      x.InforLineId == LineId
                      )
          .ToListAsync();
        }  
      }  
    }

  }
}
