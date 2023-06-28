using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.DB;
using TaskManager.Data.Models;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly IDBManager _db;
        public TaskController(IDBManager dBManager)
        {
            _db = dBManager;
        }

        [HttpGet]
        public ViewResult Index()
        {
            // Get all tasks from database
            List<Tasks> tasks = _db.getAllTasks();
            // Pass then to the view to be rendered
            return View(tasks);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tasks new_Task)
        {
            // If the received task is not valid for any reason, return to all tasks
            if(!ModelState.IsValid) return RedirectToAction("Index");

            try
            {
                // If received task is ok
                // Add the new task to the context
                bool result = _db.CreateTask(new_Task);
                // Return to the view of all tasks
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ErrorViewModel details = new ErrorViewModel()
                {
                    RequestId = "",

                };
                return View("Error", details);
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Find if there is a task with the required id
            Tasks task = _db.GetTask(id);
            // If no task, return to all tasks
            if(task == null) return RedirectToAction("Index");
            // If there is a task, pass it to the view to be rendered
            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(int id, [FromForm] Tasks eTask)
        {
            try
            {
                // If received model is not correct for any reason, return to the list of tasks
                if(!ModelState.IsValid) return RedirectToAction("Index");
                // If task is ok, update to the context
                bool result = _db.UpdateTask(eTask);
                // Return to the list of all tasks
                return RedirectToAction("Index");
            }
            catch
            {
                ErrorViewModel details = new ErrorViewModel()
                {
                    RequestId = "",

                };
                return View("Error", details);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            // This will render the task in the delete view

            // Find the requested task
            Tasks task = _db.GetTask(id);
            // If there is not task, return to all tasks
            if(task == null) return RedirectToAction("Index");
            //If there is a task, pass it to the view to be rendered
            return View(task);
        }

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // Find the requested task
                Tasks task = _db.GetTask(id);
                // If there is not task, go back to all tasks
                if (task == null) return RedirectToAction("Index");
                // If there is a task, remove it from database
                _db.DeleteTask(task);
                // Display all remaining tasks
                return RedirectToAction("Index");
            }
            catch
            {
                ErrorViewModel details = new ErrorViewModel()
                {
                    RequestId = "",

                };
                return View("Error", details);
            }
        }

        [HttpGet]
        public ActionResult TasksPDF()
        {
            try
            {
                List<Tasks> tasks = _db.getAllTasks();

                ReportDocument document = new ReportDocument();
                document.Load("TasksReport.rpt");
                document.SetDataSource(tasks);

                Stream stream = document.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "TasksList.pdf");
            }
            catch (Exception ex) 
            {
                ErrorViewModel details = new ErrorViewModel()
                { 
                    RequestId = "",
                    
                };
                return View("Error", details);
            }
        }
    }
}