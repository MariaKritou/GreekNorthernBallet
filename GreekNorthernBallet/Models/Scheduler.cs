using Newtonsoft.Json;
using SQW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Models
{
  public class Scheduler
  {
    [SQWFieldMap("SCHEDULE_ID")]
    [JsonProperty(PropertyName = "ScheduleId")]
    public int scheduleId { get; set; }

    [SQWFieldMap("TEXT")]
    [JsonProperty(PropertyName = "Text")]
    public string text { get; set; }

    [SQWFieldMap("START_DATE")]
    [JsonProperty(PropertyName = "StartDate")]
    public DateTime startDate { get; set; }

    [SQWFieldMap("END_DATE")]
    [JsonProperty(PropertyName = "EndDate")]
    public DateTime endDate { get; set; }

    [SQWFieldMap("ALL_DAY")]
    [JsonProperty(PropertyName = "AllDay")]
    public bool allDay { get; set; }

    [SQWFieldMap("DESCRIPTION")]
    [JsonProperty(PropertyName = "Description")]
    public string description { get; set; }

    [SQWFieldMap("RECURRENCE_RULE")]
    [JsonProperty(PropertyName = "RecurrenceRule")]
    public string recurrenceRule { get; set; }

    [SQWFieldMap("HTML")]
    public string html { get; set; }

    [SQWFieldMap("DISABLED")]
    public bool disabled { get; set; }

    [SQWFieldMap("END_DATE_TIME_ZONE")]
    public string endDateTimeZone { get; set; }

    [SQWFieldMap("RECURRENCE_EXCEPTION")]
    public string recurrenceException { get; set; }

    [SQWFieldMap("START_DATE_TIME_ZONE")]
    public string startDateTimeZone { get; set; }

    [SQWFieldMap("VISIBLE")]
    public bool visible { get; set; }
  }
}
