using Microsoft.AspNetCore.Mvc;
using projectList.Interfaces;
using projectList.Models;

namespace projectList.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase {

    private readonly taskItem _taskService;

    public TaskController(taskItem taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Tasks>> Get()
    {
        return _taskService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Tasks> Get(int id)
    {
        var task = _taskService.Get(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Post(Tasks newTask)
    {
        var newId = _taskService.Post(newTask);
        return CreatedAtAction(nameof(Post), new { id = newId }, newTask);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Tasks newTask)
    {
        _taskService.Put(id, newTask);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _taskService.Delete(id);
        return Ok();
    }
}

