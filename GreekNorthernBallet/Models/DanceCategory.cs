using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class DanceCategory
  {

    [SQWFieldMap("CATEGORY_ID")]
    [Display(Name = "Id")]
    public int danceId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [SQWFieldMap("CATEGORY")]
    [Display(Name = "Dance Name")]
    public string danceName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [SQWFieldMap("COST")]
    [Display(Name = "Cost")]
    public int cost { get; set; }

  }
}
