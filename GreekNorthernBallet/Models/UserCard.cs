using SQW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class UserCard
  {
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }
    [SQWFieldMap("CATEGORY_ID")]
    public int categoryId { get; set; }
    [SQWFieldMap("DATE_OF_PAYMENT")]
    public DateTime dateOfPayment { get; set; }
    [SQWFieldMap("DATE_OF_SUBSCRIPTION")]
    public DateTime dateOfSubscription { get; set; }
    [SQWFieldMap("PAYMENT")]
    public int payment { get; set; }
    [SQWFieldMap("COMMENTS")]
    public string comments { get; set; }

    public bool checkBoxPaid { get; set; }
 
  }
}
