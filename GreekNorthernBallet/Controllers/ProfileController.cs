using System;
using System.IO;
using System.Security.Claims;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using GreekNorthernBallet.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class ProfileController : Controller
  {
    private readonly UserDatabaseRepository userDatabaseRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IHostingEnvironment hostingEnvironment;
    private readonly EventDatabaseRepository eventDatabaseRepository;
    private readonly PaymentsDatabaseRepository paymentsDatabaseRepository;

    public ProfileController(UserDatabaseRepository userDatabaseRepository, IHttpContextAccessor httpContextAccessor,
      IHostingEnvironment hostingEnvironment, EventDatabaseRepository eventDatabaseRepository, PaymentsDatabaseRepository paymentsDatabaseRepository)
    {
      this.userDatabaseRepository = userDatabaseRepository;
      this.httpContextAccessor = httpContextAccessor;
      this.hostingEnvironment = hostingEnvironment;
      this.eventDatabaseRepository = eventDatabaseRepository;
      this.paymentsDatabaseRepository = paymentsDatabaseRepository;
    }


    public IActionResult Index()
    {
      var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

      Users user = userDatabaseRepository.getUserById(id);

      user.listOfEvents = eventDatabaseRepository.getEventsByUserId(id);
      user.listOfDetails = paymentsDatabaseRepository.getPaymentsByUserId(id);

      return View(user);
    }


    [HttpPost]
    public IActionResult PostEdit(Users user, string currentPassword)
    {
      var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      int userId = int.Parse(id);
      user.userId = userId;

      var password = userDatabaseRepository.getUserById(userId).password;
      if (user.password.isNotNull()) //giati an den alaxei password xtypaei oti einai null
      {
        if (PasswordHasher.validatePassword(currentPassword, password))
        {
          var newPassword = PasswordHasher.hashPassword(user.password);
          user.password = newPassword;
          userDatabaseRepository.editUser(user);
          return RedirectToAction("Index");
        }
        return RedirectToAction("Index", new { message = "Wrong Current Password" });
      }
      else
      {
        user.password = password;
        userDatabaseRepository.editUser(user);
        return RedirectToAction("Index");
      }
    }

    public IActionResult EditPhoto(int id)
    {
      return View(userDatabaseRepository.getUserById(id));
    }

    [HttpPost]
    public IActionResult ChangePhoto(UserChangeImage model)
    {
      var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      model.userId = int.Parse(id);

      string uniqueFileName = null;
      if (model.uploadImage != null)
      {
        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.uploadImage.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        model.uploadImage.CopyTo(new FileStream(filePath, FileMode.Create));
      }

      userDatabaseRepository.changeProfilePic(uniqueFileName, model.userId);
      return RedirectToAction("Index");
    }

  }
}