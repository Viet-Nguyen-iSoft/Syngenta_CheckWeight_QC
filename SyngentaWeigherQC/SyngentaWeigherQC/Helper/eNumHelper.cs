using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Helper
{
  public class eNumHelper
  {
    public static string GetDescription(Enum value)
    {
      if (value == null) return "N/A";

      FieldInfo field = value.GetType().GetField(value.ToString());

      if (field != null)
      {
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        if (attribute != null)
          return attribute.Description;
      }

      return value.ToString();
    }
  }
}
