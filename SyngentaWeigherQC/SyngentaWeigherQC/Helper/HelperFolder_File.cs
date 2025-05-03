using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class HelperFolder_File
  {
    public static void CreateFolderIfExits(string pathFolder)
    {
      if (!Directory.Exists(pathFolder))
      {
        Directory.CreateDirectory(pathFolder);
      }
    }
  }
}
