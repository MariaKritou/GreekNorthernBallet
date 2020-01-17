using Microsoft.AspNetCore.Http;
using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class EventViewModel
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Title")]
    public string title { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Description")]
    public string description { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Cost")]
    public int cost { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Number of Participants")]
    public int numOfParticipants { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "The field {0} is required")]
    [Display(Name = "Date")]
    public DateTime date { get; set; }

    [Display(Name = "Photo")]
    public IFormFile uploadImage { get; set; }

  }
}

