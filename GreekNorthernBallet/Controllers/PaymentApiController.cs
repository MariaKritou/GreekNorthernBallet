using System;
using System.Collections.Generic;
using GreekNorthernBallet.Models;
using GreekNorthernBallet.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GreekNorthernBallet.Controllers
{
  public class PaymentApiController : Controller
  {
    private readonly UserDatabaseRepository userDatabaseRepository;

    public PaymentApiController(UserDatabaseRepository userDatabaseRepository)
    {
      this.userDatabaseRepository = userDatabaseRepository;
    }

    public JsonResult GetAllPayments(string name, DateTime? date, int? pageNumber)
    {
      List<SearchPayments> search = null;
      int pageSize = 3;

      if (string.IsNullOrEmpty(name) && date != null)
      {
        //DateTime parsedDate = DateTime.Parse(date);
        search = userDatabaseRepository.getAllUsersAndPaymentsByDate(date);
      }
      else if (!string.IsNullOrEmpty(name) && date == null)
      {
        search = userDatabaseRepository.getAllUsersAndPaymentsByName(name);
      }
      else if (string.IsNullOrEmpty(name) && date == null)
      {
        search = userDatabaseRepository.getAllUsersAndPayments();
      }
      else
      {
       // DateTime parsedDate = DateTime.Parse(date);
        search = userDatabaseRepository.getAllUsersAndPaymentsByDateAndName(date, name);
      }

      var result = PaginatedList<SearchPayments>.Create(search, pageNumber ?? 1, pageSize);
      return Json(new {data = result, hasPreviousPage = result.HasPreviousPage, hasNextPage = result.HasNextPage });
    }
  }
}