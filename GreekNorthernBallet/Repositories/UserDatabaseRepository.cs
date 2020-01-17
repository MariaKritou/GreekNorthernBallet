using GreekNorthernBallet.Models;
using GreekNorthernBallet.Security;
using SQW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Repositories
{
  public class UserDatabaseRepository
  {
    private readonly ISQWWorker worker;

    public UserDatabaseRepository(ISQWWorker worker)
    {
      this.worker = worker;
    }

    public void createUser(Users user)
    {
      worker.run(context =>
      {
        context.createCommand(@"INSERT INTO MARIADEMO.GNB_USERS (USERNAME, PASSWORD, NAME, SURNAME, ADDRESS, AGE, DAY_REGISTERED, IMAGE_PATH, MOBILE) 
                                                          VALUES(:USERNAME, :PASSWORD, :NAME, :SURNAME, :ADDRESS, :AGE, :DAY_REGISTERED, :IMAGE_PATH, :MOBILE)")
        .addStringInParam("USERNAME", user.username)
        .addStringInParam("PASSWORD", PasswordHasher.hashPassword(user.password))
        .addStringInParam("NAME", user.name)
        .addStringInParam("SURNAME", user.surname)
        .addNumericInParam("AGE", user.age)
        .addStringInParam("IMAGE_PATH", user.imagePath)
        .addDateTimeInParam("DAY_REGISTERED", user.dateRegistered)
        .addStringInParam("ADDRESS", user.address)
        .addStringInParam("MOBILE", user.mobile)
        .execute();
      });
    }

    public void editUser(Users user)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.GNB_USERS SET USERNAME= :USERNAME, PASSWORD= :PASSWORD, NAME= :NAME, " +
        "SURNAME= :SURNAME, ADDRESS= :ADDRESS, AGE= :AGE, MOBILE= :MOBILE WHERE USER_ID= :USER_ID")
        .addStringInParam("USERNAME", user.username)
        .addStringInParam("PASSWORD", user.password)
        .addStringInParam("NAME", user.name)
        .addStringInParam("SURNAME", user.surname)
        .addNumericInParam("AGE", user.age)
        .addStringInParam("IMAGE_PATH", user.imagePath)
        .addDateTimeInParam("DAY_REGISTERED", user.dateRegistered)
        .addStringInParam("ADDRESS", user.address)
        .addNumericInParam("USER_ID", user.userId)
        .addStringInParam("MOBILE", user.mobile)
        .execute();
      });
    }

    public void deleteUser(int userId)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.GNB_USERS WHERE USER_ID=:USER_ID")
        .addNumericInParam("USER_ID", userId)
        .execute();
      });
    }

    public Users getUserById(int userId)
    {

      Users user = null;

      worker.run(context =>
      {
        user = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USERS WHERE USER_ID=:USER_ID")
        .addNumericInParam("USER_ID", userId)
        .first<Users>();
      });

      return user;
    } 

    public List<Users> getUserByName(string name)
    {
      List<Users> users = null;

      worker.run(context =>
      {
        users = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USERS WHERE NAME=:NAME")
        .addStringInParam("NAME", name)
        .select<Users>();
      });

      return users;
    }

    public List<Users> getAllUsers()
    {
      List<Users> users = null;

      worker.run(context =>
      {
        users = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USERS")
        .select<Users>();
      });

      return users;
    }
 


    public List<SearchPayments> getAllUsersAndPayments()
    {
      List<SearchPayments> vMs = null;

      worker.run(context =>
      {
        vMs = context
        .createSpCommand("MARIADEMO.MAIN.GET_ALL_PAYMENTS")
        .addCursorOutParam("A_RET_VAL")
        .select<SearchPayments>();
      });

      return vMs;
    }

    public List<SearchPayments> getAllUsersAndPaymentsByName(string name)
    {
      List<SearchPayments> vM = null;

      worker.run(context =>
      {
        vM = context
        .createSpCommand("MARIADEMO.MAIN.get_all_payments_by_name")
        .addCursorOutParam("A_RET_VAL")
        .addStringInParam("A_NAME", name)
        .select<SearchPayments>();
      });

      return vM;
    }

    public List<SearchPayments> getAllUsersAndPaymentsByDate(DateTime? date)
    {
      List<SearchPayments> vM = null;

      worker.run(context =>
      {
        vM = context
        .createSpCommand("MARIADEMO.MAIN.get_all_payments_by_date")
        .addCursorOutParam("A_RET_VAL")
        .addDateTimeInParam("A_DATE", date)
        .select<SearchPayments>();
      });

      return vM;
    }

    public List<SearchPayments> getAllUsersAndPaymentsByDateAndName(DateTime? date , string name)
    {
      List<SearchPayments> vM = null;

      worker.run(context =>
      {
        vM = context
        .createSpCommand("MARIADEMO.MAIN.get_all_payments_by_date_name")
        .addCursorOutParam("A_RET_VAL")
        .addDateTimeInParam("A_DATE", date)
        .addStringInParam("A_NAME", name)
        .select<SearchPayments>();
      });

      return vM;
    }




    public Users login(string username, string password)
    {

      Users user = null;
      worker.run(context =>
      {
        user = context
        .createCommand("SELECT * FROM MARIADEMO.GNB_USERS WHERE USERNAME=:USERNAME")
        .addStringInParam("USERNAME", username)
        .first<Users>();

      });

      if (user == null)
      {
        return user;
      }


      if (PasswordHasher.validatePassword(password, user.password))
      {
        return user;
      }

      return null;

    }

    public void changeProfilePic(string imagePath, int userId)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.GNB_USERS SET IMAGE_PATH=:IMAGE_PATH WHERE USER_ID=:USER_ID")
        .addStringInParam("IMAGE_PATH", imagePath)
        .addNumericInParam("USER_ID", userId)
        .execute();
      });
    }

   
  }
}
