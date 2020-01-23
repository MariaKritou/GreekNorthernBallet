using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class UserController : Controller
  {
    private readonly UserDatabaseRepository userDatabaseRepository;
    private readonly PaymentsDatabaseRepository paymentsDatabaseRepository;
  

    public UserController(UserDatabaseRepository userDatabaseRepository, PaymentsDatabaseRepository paymentsDatabaseRepository)
    {
      this.userDatabaseRepository = userDatabaseRepository;
      this.paymentsDatabaseRepository = paymentsDatabaseRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {

      if (ModelState.IsValid)
      {
        var user = userDatabaseRepository.login(model.username,model.password);
        if (user != null)
        {
          var claims = new List<Claim>
        {
          new Claim(ClaimTypes.Role, "user"),
          new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
          new Claim(ClaimTypes.Name, user.username)
        };

          if (user.isAdmin)
          {
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
          }

          var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

          return RedirectToAction("Index", "Profile");
        }
        return View("Index");
      }
      return View("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult UnauthorizedView()
    {
      return View();
    }

    public IActionResult Create()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult PostCreate(Users user)
    {
      if (ModelState.IsValid)
      {
        userDatabaseRepository.createUser(user);
        return RedirectToAction("Index");
      }
      return View("Create");
    }

    public IActionResult PostDelete(int id)
    {
      userDatabaseRepository.deleteUser(id);
      return RedirectToAction("FindPlayers");
    }

    public IActionResult FindPlayers( int? pageNumber, string name = null)
    {
      int pageSize = 6;
      var data = string.IsNullOrEmpty(name) ? userDatabaseRepository.getAllUsers() : userDatabaseRepository.getUserByName(name);
      UserSearchContextMenuVM vm = new UserSearchContextMenuVM
      {
        users = PaginatedList<Users>.Create(data, pageNumber ?? 1, pageSize),
        categories = paymentsDatabaseRepository.getAllDanceCategories()
      };
     
      return View(vm);
    }

    [HttpPost]
    public IActionResult AddSubscription(UserCard userCard)
    {
      userCard.dateOfSubscription = DateTime.Now;
      //ισως το αλλαξουμε αυτο γιατι μπορει να καταχωρησει τη πληρωμη αλλη μερα οχι την ιδια που εκανε το subscription
      userCard.dateOfPayment = userCard.dateOfSubscription;

      if (userCard.checkBoxPaid)
      {       
        userCard.payment = paymentsDatabaseRepository.getDanceCategory(userCard.categoryId).cost;
      }

      paymentsDatabaseRepository.insertPayment(userCard);
      return RedirectToAction("FindPlayers");

    }

  }
}