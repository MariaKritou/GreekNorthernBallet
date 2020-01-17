using Microsoft.AspNetCore.Http;
using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class UserChangeImage
  {
    [SQWFieldMap("USER_ID")]
    public int userId { get; set; }

    [Display(Name = "Username")]
    [SQWFieldMap("USERNAME")]
    public string username { get; set; }

    [SQWFieldMap("IMAGE_PATH")]
    public IFormFile uploadImage { get; set; }
  }
}
