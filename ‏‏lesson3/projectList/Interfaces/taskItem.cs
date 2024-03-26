using projectList.Models;

namespace projectList.Interfaces;

public interface taskItem
{
    List<Tasks> GetAll();

    Tasks Get(int id);
    int Post(Tasks newTask);
    void Put(int id, Tasks newTask);
    void Delete(int id);
}