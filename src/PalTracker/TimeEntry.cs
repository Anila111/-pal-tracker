using System;
public struct TimeEntry
{
    public long? Id;
    public long UserId;
    public long ProjectId;
    public DateTime Date;
    public int Hours;

    public TimeEntry(long id, long projectId, long userId, DateTime date, int hours)
    {
        Id = id;
        UserId = userId;
        ProjectId = projectId;
        Date = date;
        Hours = hours;
    }

    public TimeEntry(long projectId, long userId, DateTime date, int hours)
    {
        Id = null;
        UserId = userId;
        ProjectId = projectId;
        Date = date;
        Hours = hours;
    }

}