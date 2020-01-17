using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class ProfileDetails
  {

    //user
    public Users user { get; set; }
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }



    //dance_categories
    [SQWFieldMap("CATEGORY_ID")]
    public int danceId { get; set; }

    [Display(Name = "Dance Name")]
    [SQWFieldMap("CATEGORY")]
    public string danceName { get; set; }



    //payments
    [Display(Name = "Date")]
    [SQWFieldMap("DATE_OF_PAYMENT")]
    public DateTime dateOfPayment { get; set; }

    [Display(Name = "Date")]
    [SQWFieldMap("DATE_OF_SUBSCRIPTION")]
    public DateTime dateOfSubscription { get; set; }

    [Display(Name = "Payment")]
    [SQWFieldMap("PAYMENT")]
    public int payment { get; set; }

  }
}
