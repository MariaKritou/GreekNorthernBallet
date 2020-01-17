using GreekNorthernBallet.Models;
using SQW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Repositories
{
  public class NewsDatabaseRepository
  {
    private readonly ISQWWorker worker;

    public NewsDatabaseRepository(ISQWWorker worker)
    {
      this.worker = worker;
    }

    public void createNews(News news)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.NEWS (TITLE, DATE_UPLOADED, TEXT, SUBTEXT) VALUES (:TITLE, :DATE_UPLOADED, :TEXT, :SUBTEXT)")
        .addStringInParam("TITLE", news.title)
        .addStringInParam("TEXT", news.text)
        .addStringInParam("SUBTEXT", news.subText)
        .addDateTimeInParam("DATE_UPLOADED", news.dateUploaded)
        .execute();
      });
    }

    public void editNews(News news)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.NEWS SET TITLE= :TITLE, TEXT= :TEXT, SUBTEXT= :SUBTEXT WHERE ID= :ID")
        .addStringInParam("TITLE", news.title)
        .addStringInParam("TEXT", news.text)
        .addStringInParam("SUBTEXT", news.subText)
        .addNumericInParam("ID", news.id)
        .execute();
      });
    }

    public void deleteNews(int id)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.NEWS WHERE ID= :ID")
        .addNumericInParam("ID", id)
        .execute();
      });
    }

    public List<News> getAllNews()
    {
      List<News> news = null;

      worker.run(context =>
        {
          news= 
          context.createCommand("SELECT * FROM MARIADEMO.NEWS")
          .select<News>();
        });

      return news;
    }

    public News getNewsById(int id)
    {
      News news = null; 

      worker.run(context =>
      {
        news = context
        .createCommand("SELECT * FROM MARIADEMO.NEWS WHERE ID= :ID")
        .addNumericInParam("ID", id)
        .first<News>();
      });

      return news;
    }
  }
}
