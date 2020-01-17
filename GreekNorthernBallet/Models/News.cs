using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using SQW;

namespace GreekNorthernBallet.Models
{
  public class News
  {
    [SQWFieldMap("ID")]
    [Display(Name = "Id")]
    public int id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [SQWFieldMap("TITLE")]
    [Display(Name = "Title")]
    public string title { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [SQWFieldMap("TEXT")]
    [Display(Name = "Text")]
    public string text { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [SQWFieldMap("SUBTEXT")]
    [Display(Name = "Subtext")]
    public string subText { get; set; }

    [SQWFieldMap("DATE_UPLOADED")]
    [Display(Name = "Date Uploaded")]
    public DateTime dateUploaded { get; set; }

  }
}
