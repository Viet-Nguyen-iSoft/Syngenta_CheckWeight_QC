using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyngentaWeigherQC.Models
{
  public class BaseModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Browsable(false)]
    public int Id { get; set; }

    [DisplayName("Ngày tạo")]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    //[Browsable(false)]

    [DisplayName("Ngày cập nhật")]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

  }
}
