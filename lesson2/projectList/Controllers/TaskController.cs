
using Microsoft.AspNetCore.Mvc;
using projectList.Models;

namespace projectList.Controllers;

[ApiController]
[Route("[controller]")]

public class TaskController : ControllerBase {


    private List<Tasks> arr;
    public TaskController()  
    {
        arr = new List<Tasks>
        {
            new Tasks { Id = 1, Name = "laundry", IsDone = true},
            new Tasks { Id = 2, Name = "wash the dishes", IsDone = false},
            new Tasks { Id = 3, Name = "wash floor", IsDone = true},
        };
    }

    [HttpGet]
    public IEnumerable<Tasks> Get()
    {
        return arr;
    }
    
    [HttpGet("{id}")]
    public Tasks Get(int id)
    {
        return arr.FirstOrDefault(t => t.Id == id);
    }

        
    [HttpPost]
    public Tasks Post(Tasks newTask)
    {
        int max = arr.Max(p => p.Id);
        newTask.Id = max + 1;
        arr.Add(newTask);  
        return newTask;     
    }
        
    [HttpPut("{id}")]
    public void Put(int id, Tasks newTask)
    {
        if (id == newTask.Id)
        {
            var Tasks = arr.Find(p => p.Id == id);
            if (Tasks != null)
            {
                int index = arr.IndexOf(Tasks);
                arr[index] =newTask;
            }
        }
    }
        
    [HttpDelete("{id}")]
    public void Delete(int id)
    {

            var Tasks = arr.Find(p => p.Id == id);
            if (Tasks != null)
            {
                arr.Remove(Tasks);
            }

    }


}

