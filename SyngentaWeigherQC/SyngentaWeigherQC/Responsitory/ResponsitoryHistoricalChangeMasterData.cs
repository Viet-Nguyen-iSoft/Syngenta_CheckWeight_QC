﻿using Microsoft.EntityFrameworkCore;
using SyngentaWeigherQC.Control;
using SyngentaWeigherQC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Responsitory
{
  public class ResponsitoryHistoricalChangeMasterData : GenericRepository<HistoricalChangeMasterData, ConfigDBContext>
  {
    public ResponsitoryHistoricalChangeMasterData(DbContext context) : base(context)
    {

    }

  }
}
