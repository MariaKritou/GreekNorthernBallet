using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace GreekNorthernBallet.Controllers
{
  public class UserCardController : Controller
  {
    private readonly UserDatabaseRepository userDatabaseRepository;
    private readonly PaymentsDatabaseRepository paymentsDatabaseRepository;
    public UserCardController(UserDatabaseRepository userDatabaseRepository, PaymentsDatabaseRepository paymentsDatabaseRepository)
    {
      this.userDatabaseRepository = userDatabaseRepository;
      this.paymentsDatabaseRepository = paymentsDatabaseRepository;
    }


    public IActionResult Index()
    {   
      return View();
    }

  }
}