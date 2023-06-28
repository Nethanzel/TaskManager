using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Models;

namespace TaskManager.Data.DB
{
    public class DBManager : IDBManager
    {
        DBContext _database = null;
        public DBManager(DBContext dBContext)
        {
            this._database = dBContext;
        }

        public List<Tasks> getAllTasks()
        {
            return _database.Tasks.ToList();
        }

        public bool CreateTask(Tasks newTask)
        {
           _database.Tasks.Add(newTask);
           return _database.SaveChanges() > 0 ? true : false;
        }

        public Tasks GetTask(int id)
        {
            return _database.Tasks.SingleOrDefault(tk => tk.Id == id);
        }

        public bool UpdateTask(Tasks task) 
        {
            _database.Tasks.Update(task);
           return _database.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteTask(Tasks task) 
        {
            _database.Tasks.Remove(task);
           return _database.SaveChanges() > 0 ? true : false;
        }
    }
}
