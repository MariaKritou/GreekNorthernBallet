using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreekNorthernBallet.Controllers
{
  public class SchedulerController : Controller
  {

    private readonly SchedulerDatabaseRepository schedulerDatabaseRepository;

    public SchedulerController(SchedulerDatabaseRepository schedulerDatabaseRepository)
    {
      this.schedulerDatabaseRepository = schedulerDatabaseRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public object GetSchedulers(DataSourceLoadOptions loadOptions)
    {
      return DataSourceLoader.Load(schedulerDatabaseRepository.getAllSchedules(), loadOptions); ;
    }

    [HttpPut]
    public IActionResult PostEdit(string values, int key)
    {
      var schedule = schedulerDatabaseRepository.getScheduleById(key);
      JsonConvert.PopulateObject(values, schedule);
      schedulerDatabaseRepository.editSchedule(schedule);
      return Ok();
    }

    [HttpDelete]
    public IActionResult PostDelete(int key)
    {
      schedulerDatabaseRepository.deleteSchedule(key);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PostCreate(string values)
    {
      var scheduler = new Scheduler();
      JsonConvert.PopulateObject(values, scheduler);

      schedulerDatabaseRepository.createSchedule(scheduler);
      return Ok();
    }

  }
}