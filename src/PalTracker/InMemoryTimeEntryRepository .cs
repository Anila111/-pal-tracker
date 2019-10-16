using System;
using System.Collections.Generic;
using System.Linq;
public class InMemoryTimeEntryRepository : ITimeEntryRepository
{
    private readonly IDictionary<long,TimeEntry> repo = new Dictionary<long,TimeEntry>();
    public bool Contains(long id) => repo.ContainsKey(id);

    public TimeEntry Create(TimeEntry toCreate)
    {
        var id = repo.Count+1;
        toCreate.Id = id;
        repo.Add(id,toCreate);
        return repo[id];
    }

    public void Delete(long id)=> repo.Remove(id);

    public TimeEntry Find(long id)=> repo[id];

    public IEnumerable<TimeEntry> List() => repo.Values.ToList();

    public TimeEntry Update(long id, TimeEntry toUpdate)
    {
        toUpdate.Id = id;
        repo[id] = toUpdate;
        return repo[id];
    }
}