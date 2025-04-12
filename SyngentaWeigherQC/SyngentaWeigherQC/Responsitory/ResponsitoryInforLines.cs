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
  public class ResponsitoryInforLines : GenericRepository<InforLine, ConfigDBContext>
  {
    public ResponsitoryInforLines(DbContext context) : base(context)
    {

    }
    public async Task<List<InforLine>> GetAsync()
    {
      return await this.Context.Set<InforLine>()
        .Include(x=>x.Productions).Where(x=>x.IsDelete==false)
        .Include(x=>x.ShiftLeader).Where(x=>x.IsDelete==false)
        .ToListAsync();
    }



  }
}
