﻿using SyngentaWeigherQC.Logs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyngentaWeigherQC.Helper
{
  public class LoggerHelper
  {
    public static void LogErrorToFileLog(string content)
    {
      string NameFileLog = Application.StartupPath + $"\\log.txt";
      if (!File.Exists(NameFileLog))
      {
        File.Create(NameFileLog);
      }
      string contentLog = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + $": Content: {content} \r\n";
      File.AppendAllText(NameFileLog, contentLog);
    }

    public static void LogErrorToFileLog(Exception content)
    {
      string NameFileLog = Application.StartupPath + $"\\log.txt";
      if (!File.Exists(NameFileLog))
      {
        File.Create(NameFileLog);
      }
      string contentLog = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + $": Content: {content.Message} ---  {content.StackTrace} \r\n";
      File.AppendAllText(NameFileLog, contentLog);
    }
  }
}
