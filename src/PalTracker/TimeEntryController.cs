using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
[Route("/time-entries")]
public class TimeEntryController : ControllerBase
{

    private ITimeEntryRepository repo { get; set; }
    public TimeEntryController(ITimeEntryRepository timeRepository)
    {
        repo = timeRepository;
    }

    [HttpGet("{id}", Name = "GetTimeEntry")]
    public IActionResult  Read(long id)
    {
        if (!repo.Contains(id))
            return NotFound();
        else
            return Ok(repo.Find(id));
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
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
        var response = repo.Create(toCreate);
        return CreatedAtRoute("GetTimeEntry", new {id=response.Id}, response);
    }

    [HttpGet]
    public IActionResult List()
    {
        var response = repo.List();
        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult Update(long id,[FromBody] TimeEntry toUpdate)
    {
        if (!repo.Contains(id))
        {
            return NotFound();
        }
        var response = repo.Update(id, toUpdate);
        return Ok(response);
    }
}