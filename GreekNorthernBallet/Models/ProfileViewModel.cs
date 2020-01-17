using Microsoft.AspNetCore.Http;
using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class ProfileViewModel
  {
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }

    [Display(Name = "Username")]
    [SQWFieldMap("USERNAME")]
    public string username { get; set; }

    [Display(Name = "Password")]
    [SQWFieldMap("PASSWORD")]
    public string password { get; set; }

    [Display(Name = "First Name")]
    [SQWFieldMap("NAME")]
    public string name { get; set; }

    [Display(Name = "Last Name")]
    [SQWFieldMap("SURNAME")]
    public string surname { get; set; }

    [Display(Name = "Address")]
    [SQWFieldMap("ADDRESS")]
    public string address { get; set; }

    [Display(Name = "Age")]
    [SQWFieldMap("AGE")]
    public int age { get; set; }

    [Display(Name = "Mobile")]
    [SQWFieldMap("MOBILE")]
    public string mobile { get; set; }

    [Display(Name = "Date of Registration")]
    [SQWFieldMap("DATE_REGISTERED")]
    public DateTime dateRegistered { get; set; }

    [SQWFieldMap("IMAGE_PATH")]
    public IFormFile uploadImage { get; set; }

    [SQWFieldMap("IS_ADMIN")]
    public bool isAdmin { get; set; }
  }
}
