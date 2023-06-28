using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Controllers;
using TaskManager.Data.DB;
using TaskManager.Data.Models;

namespace TaskManager.Test.Controller
{
    public class TaskControllerTest
    {
        private readonly Mock<IDBManager> _database;
        private readonly TaskController _controller;

        public TaskControllerTest()
        {
            _database = new Mock<IDBManager>();
            _controller = new TaskController(_database.Object);
        }

        // Tests if the Tasks view is returning a view as result
        [Fact]
        public void Test_TaskView()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        // Tests to verify if the data is being rendered as expected in the Tasks view
        [Fact]
        public void Test_TaskList()
        {
            var tasks = new List<Tasks>()
        {
            new Tasks()
            {
                Descripcion = "Task #1 description",
                Estado = "Pending",
                Fecha = DateTime.Now,
                Prioridad = 1,
                Id = 1
            },
            new Tasks()
            {
                Descripcion = "Task #2 description",
                Estado = "Done",
                Fecha = DateTime.Now.AddDays(-3),
                Prioridad = 4,
                Id = 2
            }
        };

            _database.Setup(repo => repo.getAllTasks()).Returns(tasks);

            var result = _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var employees = Assert.IsType<List<Tasks>>(viewResult.Model);
            Assert.Equal(2, employees.Count);
        }

        // Tests if the application is returning the spected view when inserting a new task
        [Fact]
        public void Test_TaskInsertionView()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }
    }
}