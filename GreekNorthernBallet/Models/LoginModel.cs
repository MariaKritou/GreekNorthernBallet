using System.ComponentModel.DataAnnotations;

namespace GreekNorthernBallet.Models
{
  public class LoginModel
  {
    [Display(Name = "Username")]
    [Required(ErrorMessage = "The field {0} is required")]
    public string username { get; set; }


    [Display(Name = "Password")]
    [Required(ErrorMessage = "The field {0} is required")]
    public string password { get; set; }
  }
}
