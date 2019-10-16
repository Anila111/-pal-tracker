using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using PalTracker;

[Route("/time-entries")]
public class TimeEntryController : ControllerBase
{

    private ITimeEntryRepository repo { get; set; }
    private IOperationCounter<TimeEntry> opCounter { get; set; }
    public TimeEntryController(ITimeEntryRepository timeRepository, IOperationCounter<TimeEntry> operationCounter)
    {
        repo = timeRepository;
        opCounter = operationCounter;
    }

    [HttpGet("{id}", Name = "GetTimeEntry")]
    public IActionResult  Read(long id)
    {
        opCounter.Increment(TrackedOperation.Read);
        if (!repo.Contains(id))
            return NotFound();
        else
            return Ok(repo.Find(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        opCounter.Increment(TrackedOperation.Delete);
        if (repo.Contains(id))
        {
            repo.Delete(id);
            return NoContent();
        }
        else
            return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] TimeEntry toCreate)
    {
        opCounter.Increment(TrackedOperation.Create);
        var response = repo.Create(toCreate);
        return CreatedAtRoute("GetTimeEntry", new {id=response.Id}, response);
    }

    [HttpGet]
    public IActionResult List()
    {
        opCounter.Increment(TrackedOperation.List);
        var response = repo.List();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id,[FromBody] TimeEntry toUpdate)
    {
        opCounter.Increment(TrackedOperation.Update);
        if (!repo.Contains(id))
        {
            return NotFound();
        }
        var response = repo.Update(id, toUpdate);
        return Ok(response);
    }
}