using TaskManager.Data.Models;

namespace TaskManager.Data.DB
{
    public interface IDBManager
    {
        public List<Tasks> getAllTasks();
        public bool CreateTask(Tasks newTask);
        public Tasks GetTask(int id);
        public bool UpdateTask(Tasks task);
        public bool DeleteTask(Tasks task);
    }
}
