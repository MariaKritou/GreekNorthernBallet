using GreekNorthernBallet.Models;
using SQW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Repositories
{
  public class EventDatabaseRepository
  {
    private readonly ISQWWorker worker;

    public EventDatabaseRepository(ISQWWorker worker)
    {
      this.worker = worker;
    }

    public void createEvent(Events events)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.EVENTS (TITLE, DESCRIPTION_OF_EVENT, COST_OF_EVENT, NUM_OF_PARTICIPANTS, DATE_PLACED, IMAGE_PATH) " +
        "VALUES (:TITLE, :DESCRIPTION_OF_EVENT, :COST_OF_EVENT, :NUM_OF_PARTICIPANTS, :DATE_PLACED, :IMAGE_PATH)")
        .addNumericInParam("COST_OF_EVENT", events.cost)
        .addNumericInParam("NUM_OF_PARTICIPANTS", events.numOfParticipants)
        .addStringInParam("TITLE", events.title)
        .addStringInParam("DESCRIPTION_OF_EVENT", events.description)
        .addStringInParam("IMAGE_PATH", events.imagePath)
        .addDateTimeInParam("DATE_PLACED", events.date)
        .execute();
      });
    }

    public void deleteEvent(int eventId)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.EVENTS WHERE EVENT_ID =:EVENT_ID")
        .addNumericInParam("EVENT_ID", eventId)
        .execute();
      });
    }

    public void editEvent(Events events)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.EVENTS SET TITLE =:TITLE, DESCRIPTION_OF_EVENT =:DESCRIPTION_OF_EVENT," +
        " COST_OF_EVENT =:COST_OF_EVENT, NUM_OF_PARTICIPANTS =:NUM_OF_PARTICIPANTS, DATE_PLACED =:DATE_PLACED, IMAGE_PATH =:IMAGE_PATH WHERE EVENT_ID =:EVENT_ID")
        .addNumericInParam("COST_OF_EVENT", events.cost)
        .addNumericInParam("NUM_OF_PARTICIPANTS", events.numOfParticipants)
        .addStringInParam("TITLE", events.title)
        .addStringInParam("DESCRIPTION_OF_EVENT", events.description)
        .addStringInParam("IMAGE_PATH", events.imagePath)
        .addDateTimeInParam("DATE_PLACED", events.date)
        .addNumericInParam("EVENT_ID", events.eventId)
        .execute();
      });
    }

    public List<Events> getAllEvents()
    {
      List<Events> events = null;

      worker.run(context =>
      {
        events = context
        .createCommand("SELECT * FROM MARIADEMO.EVENTS")
        .select<Events>();
      });

      return events;
    }

    public void eventRegistration(int userId, int eventId)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.GNB_REGISTRATIONS (EVENT_ID, USER_ID) VALUES (:EVENT_ID, :USER_ID)")
        .addNumericInParam("EVENT_ID", eventId)
        .addNumericInParam("USER_ID", userId)
        .execute();        
      });

      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.EVENTS SET NUM_OF_PARTICIPANTS = NUM_OF_PARTICIPANTS-1 WHERE EVENT_ID =:EVENT_ID")
        .addNumericInParam("EVENT_ID", eventId)
        .execute();
      });
    }

    public List<Events> getEventsByUserId(int userId)
    {
      List<Events> eventsByUser = null;

      worker.run(context =>
      {
        eventsByUser = context
        .createCommand("SELECT EVENT_ID FROM MARIADEMO.GNB_REGISTRATIONS WHERE USER_ID = :USER_ID")
        .addNumericInParam("USER_ID", userId)
        .select<Events>();
      });

      
      foreach (var events in eventsByUser)
      {
        var aEvent = getEventByEventId(events.eventId);
        events.title = aEvent.title;     
      }
      return eventsByUser;
    }
    
    public Events getEventByEventId(int eventId)
    {
      Events events = null;

      worker.run(context =>
      {
        events = context
         .createCommand("SELECT * FROM MARIADEMO.EVENTS WHERE EVENT_ID=:EVENT_ID")
         .addNumericInParam("EVENT_ID", eventId)
         .first<Events>();
      });
      return events;
    }

    public void deleteRegistration(int userId, int eventId)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.GNB_REGISTRATIONS WHERE (USER_ID=:USER_ID AND EVENT_ID=:EVENT_ID)")
        .addNumericInParam("USER_ID", userId)
        .addNumericInParam("EVENT_ID", eventId)
        .execute();
      });

      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.EVENTS SET NUM_OF_PARTICIPANTS = NUM_OF_PARTICIPANTS+1 WHERE EVENT_ID =:EVENT_ID")
        .addNumericInParam("EVENT_ID", eventId)
        .execute();
      });
    }
  }
}
