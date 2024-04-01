using projectList.Models;
using projectList.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace projectList.Services;

public class taskService : taskItem
{
    private List<Tasks> _arr;
    private string filePath;

    public taskService(IWebHostEnvironment webHost)
    {
        this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "tasks.json");  // Corrected filename pluralization

        if (File.Exists(filePath))
        {
            using (var jsonFile = File.OpenText(filePath))
            {
                _arr = JsonSerializer.Deserialize<List<Tasks>>(jsonFile.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
        else
        {
            _arr = new List<Tasks>(); 
        }
    }

    private void saveToFile()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, JsonSerializer.Serialize(_arr));
    }

    public List<Tasks> GetAll()
    {
        return _arr;
    }

    public Tasks Get(int id)
    {
        return _arr.FirstOrDefault(t => t.Id == id);
    }

    public void Post(Tasks newTask)
    {
        newTask.Id = _arr.Count + 1;  
        _arr.Add(newTask);
        saveToFile();
    }

    public void Put(Tasks task)  
    {
        int index = _arr.FindIndex(t => t.Id == task.Id);
        if (index != -1) 
        {
            _arr[index] = task;
            saveToFile();
        }
    }

    public void Delete(int id)
    {
        var task = _arr.Find(t => t.Id == id);
        if (task != null) 
        {
            _arr.Remove(task);
            saveToFile();
        }
    }

    public void Put(int id, Tasks newTask)
    {
        throw new NotImplementedException();
    }

    int taskItem.Post(Tasks newTask)
    {
        throw new NotImplementedException();
    }

    public int Count => _arr.Count;
}