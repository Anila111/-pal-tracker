using System;
using System.Collections.Generic;
public interface ITimeEntryRepository
{   

    void Delete(long id);
    TimeEntry Find(long id);
    bool Contains(long id);
    TimeEntry Create(TimeEntry toCreate);
    TimeEntry Update(long id, TimeEntry time);
    List<TimeEntry> List();
}