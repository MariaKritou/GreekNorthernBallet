using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class EventController : Controller
  {
    private readonly EventDatabaseRepository eventDatabaseRepository;
    private readonly IHostingEnvironment hostingEnvironment;
    private readonly UserDatabaseRepository userDatabaseRepository;
    public EventController(EventDatabaseRepository eventDatabaseRepository, IHostingEnvironment hostingEnvironment, UserDatabaseRepository userDatabaseRepository)
    {
      this.eventDatabaseRepository = eventDatabaseRepository;
      this.hostingEnvironment = hostingEnvironment;
      this.userDatabaseRepository = userDatabaseRepository;
    }

    public IActionResult Index(string message)
    {
      var events = eventDatabaseRepository.getAllEvents();
      ViewData["message"] = message;
      return View(events);
    }

    public IActionResult Create()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult PostCreate(EventViewModel model)
    {
      if (ModelState.IsValid)
      {
        string uniqueFileName = null;
        if (model.uploadImage != null)
        {
          string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
          uniqueFileName = Guid.NewGuid().ToString() + "_" + model.uploadImage.FileName;
          string filePath = Path.Combine(uploadsFolder, uniqueFileName);
          model.uploadImage.CopyTo(new FileStream(filePath, FileMode.Create));
        }

        Events events = new Events
        {
          title = model.title,
          description = model.description,
          cost = model.cost,
          numOfParticipants = model.numOfParticipants,
          date = model.date,
          imagePath = uniqueFileName
        };

        eventDatabaseRepository.createEvent(events);
        return RedirectToAction("Index");
      }
      return View("Create");
    }

    [HttpPost]
    public IActionResult PostDelete(int eventId)
    {
      eventDatabaseRepository.deleteEvent(eventId);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PostEdit(Events events)
    {
      eventDatabaseRepository.editEvent(events);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Registration(int eventId)
    {
      var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      int userId = int.Parse(id);

      var checkIfRegistered = eventDatabaseRepository.getEventsByUserId(userId);
      var check = checkIfRegistered.Any(x => x.eventId == eventId);
      if (User.IsInRole("user"))
      {
        if (!check)
        {
          var numberOfpositions = eventDatabaseRepository.getEventByEventId(eventId).numOfParticipants;
          if (numberOfpositions > 0)
          {
            eventDatabaseRepository.eventRegistration(userId, eventId);
            return RedirectToAction("Index", new { message = "Successfully registered" });
          }
          return RedirectToAction("Index", new { message = "There are no positions left in this event" });
        }
        return RedirectToAction("Index", new { message = "Only members can register" });
      }
      return RedirectToAction("Index", new { message = "You are already registered to this event" });
    }

    [HttpPost]
    public IActionResult Unregister(int eventId)
    {
      var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      int userId = int.Parse(id);
      eventDatabaseRepository.deleteRegistration(userId, eventId);

      return RedirectToAction("Index");
    }

  }
}