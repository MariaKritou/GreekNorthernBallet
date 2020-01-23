using System;
using System.Linq;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class NewsController : Controller
  {

    private readonly NewsDatabaseRepository newsDatabaseRepository;

    public NewsController(NewsDatabaseRepository newsDatabaseRepository)
    {
      this.newsDatabaseRepository = newsDatabaseRepository;
    }


    public IActionResult Index(int? pageNumber)
    {
      int pageSize = 6;
      var data = newsDatabaseRepository.getAllNews().OrderBy(x=>x.dateUploaded);
      return View(PaginatedList<News>.Create(data, pageNumber ?? 1, pageSize));
    }

    [HttpPost]
    public IActionResult PostEdit(News news)
    {
      newsDatabaseRepository.editNews(news);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PostDelete(int id)
    {
      newsDatabaseRepository.deleteNews(id);
      return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
      return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public IActionResult PostCreate(News news)
    {
      if (ModelState.IsValid)
      {
        news.dateUploaded = DateTime.Now;
        newsDatabaseRepository.createNews(news);
        return RedirectToAction("Index");
      }
      return View("Create");
    }

  }
}