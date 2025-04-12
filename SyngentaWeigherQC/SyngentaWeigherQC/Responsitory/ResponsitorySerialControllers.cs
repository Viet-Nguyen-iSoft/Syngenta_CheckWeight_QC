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
  public class ResponsitorySerialControllers : GenericRepository<SerialControllers, ConfigDBContext>
  {
    public ResponsitorySerialControllers(DbContext context) : base(context)
    {

    }

  }
}
