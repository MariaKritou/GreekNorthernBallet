using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class SearchPayments
  {

    //users
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }

    [Display(Name = "First Name")]
    [SQWFieldMap("NAME")]
    public string name { get; set; }

    [Display(Name = "Last Name")]
    [SQWFieldMap("SURNAME")]
    public string surname { get; set; }

    [SQWFieldMap("IMAGE_PATH")]
    public string imagePath { get; set; }

    [Display(Name = "Mobile")]
    [SQWFieldMap("MOBILE")]
    public string mobile { get; set; }



    //dance_categories
    [SQWFieldMap("CATEGORY_ID")]
    public int danceId { get; set; }
    
    [Display(Name = "Dance Name")]
    [SQWFieldMap("Category")] 
    public string danceName { get; set; }



    //user_payments 
    [Display(Name = "Date")]
    [SQWFieldMap("DATE_OF_PAYMENT")]  
    public DateTime dateOfPayment { get; set; }

    [Display(Name = "Date")]
    [SQWFieldMap("DATE_OF_SUBSCRIPTION")]   
    public DateTime dateOfSubscription { get; set; }

    [Display(Name = "Payment")]
    [SQWFieldMap("PAYMENT")]   
    public int payment { get; set; }

    [Display(Name = "Comments")]
    [SQWFieldMap("COMMENTS")]   
    public string comments { get; set; }

  }
}
