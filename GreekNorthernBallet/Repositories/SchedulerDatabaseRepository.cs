using GreekNorthernBallet.Models;
using SQW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreekNorthernBallet.Repositories
{
  public class SchedulerDatabaseRepository
  {
    private readonly ISQWWorker worker;

    public SchedulerDatabaseRepository(ISQWWorker worker)
    {
      this.worker = worker;
    }

    public void createSchedule(Scheduler scheduler)
    {
      worker.run(context =>
      {
        context
        .createCommand("INSERT INTO MARIADEMO.SCHEDULER " +
        "(TEXT, START_DATE, END_DATE, ALL_DAY, DESCRIPTION, RECURRENCE_RULE, HTML, DISABLED, END_DATE_TIME_ZONE, RECURRENCE_EXCEPTION, START_DATE_TIME_ZONE) " +
        "VALUES" +
        "(:TEXT, :START_DATE, :END_DATE, :ALL_DAY, :DESCRIPTION, :RECURRENCE_RULE, :HTML, :DISABLED, :END_DATE_TIME_ZONE, :RECURRENCE_EXCEPTION, :START_DATE_TIME_ZONE)")
        .addStringInParam("TEXT", scheduler.text)
        .addDateTimeInParam("START_DATE", scheduler.startDate)
        .addDateTimeInParam("END_DATE", scheduler.endDate)
        .addStringInParam("ALL_DAY", scheduler.allDay)
        .addStringInParam("DESCRIPTION", scheduler.description)
        .addStringInParam("RECURRENCE_RULE", scheduler.recurrenceRule)
        .addStringInParam("HTML", scheduler.html)
        .addStringInParam("DISABLED", scheduler.disabled)
        .addStringInParam("END_DATE_TIME_ZONE", scheduler.endDateTimeZone)
        .addStringInParam("RECURRENCE_EXCEPTION", scheduler.recurrenceException)
        .addStringInParam("START_DATE_TIME_ZONE", scheduler.startDateTimeZone)
        .execute();
      });
    }

    public void editSchedule(Scheduler scheduler)
    {
      worker.run(context =>
      {
        context
        .createCommand("UPDATE MARIADEMO.SCHEDULER SET " +
        "TEXT= :TEXT, START_DATE= :START_DATE, END_DATE= :END_DATE, ALL_DAY= :ALL_DAY, " +
        "DESCRIPTION= :DESCRIPTION, RECURRENCE_RULE= :RECURRENCE_RULE, HTML= :HTML, DISABLED= :DISABLED, " +
        "END_DATE_TIME_ZONE= :END_DATE_TIME_ZONE, RECURRENCE_EXCEPTION= :RECURRENCE_EXCEPTION, START_DATE_TIME_ZONE= :START_DATE_TIME_ZONE " +
        "WHERE SCHEDULE_ID= :SCHEDULE_ID")
        .addStringInParam("TEXT", scheduler.text)
        .addDateTimeInParam("START_DATE", scheduler.startDate)
        .addDateTimeInParam("END_DATE", scheduler.endDate)
        .addStringInParam("ALL_DAY", scheduler.allDay)
        .addStringInParam("DESCRIPTION", scheduler.description)
        .addStringInParam("RECURRENCE_RULE", scheduler.recurrenceRule)
        .addStringInParam("HTML", scheduler.html)
        .addStringInParam("DISABLED", scheduler.disabled)
        .addStringInParam("END_DATE_TIME_ZONE", scheduler.endDateTimeZone)
        .addStringInParam("RECURRENCE_EXCEPTION", scheduler.recurrenceException)
        .addStringInParam("START_DATE_TIME_ZONE", scheduler.startDateTimeZone)
        .addNumericInParam("SCHEDULE_ID", scheduler.scheduleId)
        .execute();
      });
    }

    public void deleteSchedule(int scheduleId)
    {
      worker.run(context =>
      {
        context
        .createCommand("DELETE FROM MARIADEMO.SCHEDULER WHERE SCHEDULE_ID= :SCHEDULE_ID")
        .addNumericInParam("SCHEDULE_ID", scheduleId)
        .execute();
      });
    }

    public List<Scheduler> getAllSchedules()
    {
      List<Scheduler> schedules = null;

      worker.run(context =>
      {
        schedules = context
        .createCommand("SELECT * FROM MARIADEMO.SCHEDULER")
        .select<Scheduler>();
      });

      return schedules;
    }

    public Scheduler getScheduleById(int scheduleId)
    {

      var schedule = new Scheduler();
      worker.run(context =>
        {
          context
          .createCommand("SELECT * FROM MARIADEMO.SCHEDULER WHERE SCHEDULE_ID= :SCHEDULE_ID")
          .addNumericInParam("SCHEDULE_ID", scheduleId)
          .first<Scheduler>();
        });

      return schedule;
    }
  }
}
