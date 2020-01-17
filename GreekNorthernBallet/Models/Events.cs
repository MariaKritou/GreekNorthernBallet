using SQW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class Events
  {
    [SQWFieldMap("EVENT_ID")]
    [Display(Name = "Id")]
    public int eventId { get; set; }

    [SQWFieldMap("TITLE")]
    [Display(Name = "Title")]
    public string title { get; set; }

    [SQWFieldMap("DESCRIPTION_OF_EVENT")]
    [Display(Name = "Description")]
    public string description { get; set; }

    [SQWFieldMap("COST_OF_EVENT")]
    [Display(Name = "Cost")]
    public int cost { get; set; }

    [SQWFieldMap("NUM_OF_PARTICIPANTS")]
    [Display(Name = "Number of Participants")]
    public int numOfParticipants { get; set; }

    [SQWFieldMap("DATE_PLACED")]
    [Display(Name = "Date")]
    public DateTime date { get; set; }

    [SQWFieldMap("IMAGE_PATH")]
    [Display(Name = "Image Url")]
    public string imagePath { get; set; }

  }
}
