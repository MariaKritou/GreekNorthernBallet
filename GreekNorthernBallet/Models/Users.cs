using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class Users
  {
   
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Username")]
    [SQWFieldMap("USERNAME")]
    public string username { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Password")]
    [SQWFieldMap("PASSWORD")]
    public string password { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "First Name")]
    [SQWFieldMap("NAME")]
    public string name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Last Name")]
    [SQWFieldMap("SURNAME")]
    public string surname { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Address")]
    [SQWFieldMap("ADDRESS")]
    public string address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Age")]
    [SQWFieldMap("AGE")]
    public int age { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Mobile")]
    [SQWFieldMap("MOBILE")]
    public string mobile { get; set; }

    [DisplayFormat(DataFormatString = "{0:d/M/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Registration")]
    [SQWFieldMap("DATE_REGISTERED")]
    public DateTime dateRegistered { get; set; }

    [SQWFieldMap("IMAGE_PATH")]
    public string imagePath { get; set; }

    [SQWFieldMap("IS_ADMIN")]
    public bool isAdmin { get; set; }

    public List<ProfileDetails> listOfDetails { get; set; } //payments information
    public List<Events> listOfEvents { get; set; }
  }
}
