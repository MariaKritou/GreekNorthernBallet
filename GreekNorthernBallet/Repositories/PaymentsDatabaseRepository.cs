using GreekNorthernBallet.Models;
using SQW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Repositories
{
  public class PaymentsDatabaseRepository
  {
    private readonly ISQWWorker worker;

    public PaymentsDatabaseRepository(ISQWWorker worker)
    {
      this.worker = worker;
    }

    public void insertPayment(UserCard userCard)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.GNB_USER_PAYMENTS (USER_ID, CATEGORY_ID, DATE_OF_PAYMENT, PAYMENT, DATE_OF_SUBSCRIPTION, COMMENTS) " +
        "VALUES (:USER_ID, :CATEGORY_ID, :DATE_OF_PAYMENT, :PAYMENT, :DATE_OF_SUBSCRIPTION, :COMMENTS)")
        .addNumericInParam("USER_ID", userCard.userId)
        .addNumericInParam("CATEGORY_ID", userCard.categoryId)
        .addDateTimeInParam("DATE_OF_PAYMENT", userCard.dateOfPayment)
        .addDateTimeInParam("DATE_OF_SUBSCRIPTION", userCard.dateOfPayment)
        .addNumericInParam("PAYMENT", userCard.payment)
        .addStringInParam("COMMENTS", userCard.comments)
        .execute();
      });
    }

    public List<UserCard> getAllPaymentsPerUserAndCategory(int userId, int categoryId, DateTime dateTime)
    {
      List<UserCard> userCards = null;

      worker.run(context =>
      {
        userCards = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_PAYMENTS " +
        "WHERE (USER_ID=:USER_ID AND CATEGORY_ID=:CATEGORY_ID AND DATENAME(month, DAY_OF_PAYMENT)=:DATE_INSERTED)")
        .addNumericInParam("USER_ID", userId)
        .addNumericInParam("CATEGORY_ID", categoryId)
        .addDateTimeInParam("DATE_INSERTED", dateTime)
        .select<UserCard>();
      });

      return userCards;
    }

    public UserCard getUserCard(int userId)
    {
      UserCard user = null;

      worker.run(context =>
      {
        user = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USER_PAYMENTS WHERE USER_ID=:USER_ID")
        .addNumericInParam("USER_ID", userId)
        .first<UserCard>();
      });

      return user;
    }

    public List<ProfileDetails> getPaymentsByUserId(int userId)
    {
      List<ProfileDetails> paymentDetails = null;

      worker.run(context =>
      {
        paymentDetails = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USER_PAYMENTS WHERE USER_ID=:USER_ID")
        .addNumericInParam("USER_ID", userId)
        .select<ProfileDetails>();
      });

      foreach (var payment in paymentDetails)
      {
        var aCategory = getDanceCategory(payment.danceId);
        payment.danceName = aCategory.danceName;
      }

      return paymentDetails;
    }

    public void createDanceCategory(DanceCategory danceCategory)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.GNB_DANCES (CATEGORY, COST) VALUES (:CATEGORY, :COST)")
        .addStringInParam("CATEGORY", danceCategory.danceName)
        .addNumericInParam("COST", danceCategory.cost)
        .execute();
      });
    }

    public void editDanceCategory(DanceCategory danceCategory)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.GNB_DANCES SET CATEGORY=:CATEGORY , COST=:COST WHERE CATEGORY_ID=:CATEGORY_ID")
        .addStringInParam("CATEGORY", danceCategory.danceName)
        .addNumericInParam("COST", danceCategory.cost)
        .addNumericInParam("CATEGORY_ID", danceCategory.danceId)
        .execute();
      });
    }

    public void deleteDancecategory(int id)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.GNB_DANCES WHERE CATEGORY_ID=:CATEGORY_ID")
        .addNumericInParam("CATEGORY_ID", id)
        .execute();
      });
    }

    public List<DanceCategory> getAllDanceCategories()
    {
      List<DanceCategory> danceCategories = null;

      worker.run(context =>
      {
        danceCategories = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_DANCES")
        .select<DanceCategory>();
      });

      return danceCategories;
    }

    public DanceCategory getDanceCategory(int id)
    {
      DanceCategory category = null;

      worker.run(context =>
      {
        category = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_DANCES WHERE CATEGORY_ID = :CATEGORY_ID")
        .addNumericInParam("CATEGORY_ID", id)
        .first<DanceCategory>();
      });

      return category;
    }
  }
}
