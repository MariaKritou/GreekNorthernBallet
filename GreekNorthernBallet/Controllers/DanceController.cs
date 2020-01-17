using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class DanceController : Controller
  {

    private readonly PaymentsDatabaseRepository paymentsDatabaseRepository;

    public DanceController(PaymentsDatabaseRepository paymentsDatabaseRepository)
    {
      this.paymentsDatabaseRepository = paymentsDatabaseRepository;
    }

    //view all categories  
    public IActionResult Index()
    {
      return View(paymentsDatabaseRepository.getAllDanceCategories());
    }

    public IActionResult CreateDance()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    public IActionResult PostCreateDance(DanceCategory danceCategory)
    {
      if (ModelState.IsValid)
      {
        paymentsDatabaseRepository.createDanceCategory(danceCategory);
        return RedirectToAction("Index");
      }
      return View("CreateDance");
    }

    [HttpPost]
    public IActionResult PostDelete(int danceId)
    {
      paymentsDatabaseRepository.deleteDancecategory(danceId);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PostEdit(DanceCategory danceCategory)
    {
      paymentsDatabaseRepository.editDanceCategory(danceCategory);
      return RedirectToAction("Index");
    }
  }
}