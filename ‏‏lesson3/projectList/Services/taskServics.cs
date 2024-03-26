using projectList.Models;
using projectList.Interfaces;
using System.Collections.Generic;

namespace projectList.Services;

public class taskService : taskItem
{

    private List<Tasks> _arr;

    public taskService()
    {
        _arr = new List<Tasks>
        {
            new Tasks { Id = 1, Name = "laundry", IsDone = true },
            new Tasks { Id = 2, Name = "wash the dishes", IsDone = false },
            new Tasks { Id = 3, Name = "wash floor", IsDone = true },
        };
    }

    public IEnumerable<Tasks> Get()
    {
        return _arr;
    }

    public Tasks Get(int id)
    {
        return _arr.FirstOrDefault(t => t.Id == id);
    }

    public int Post(Tasks newTask)
    {
        int max = _arr.Max(p => p.Id);
        newTask.Id = max + 1;
        _arr.Add(newTask);
        return newTask.Id;
    }

    public void Put(int id, Tasks newTask)
    {
        if (id == newTask.Id)
        {
            var task = _arr.Find(p => p.Id == id);
            if (task != null)
            {
                int index = _arr.IndexOf(task);
                _arr[index] = newTask;
            }
        }
    }

    public void Delete(int id)
    {
        var task = _arr.Find(p => p.Id == id);
        if (task != null)
        {
            _arr.Remove(task);
        }
    }

    public List<Tasks> GetAll()
    {
        return _arr;
    }
}