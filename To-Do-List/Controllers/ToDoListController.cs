using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace To_Do_List.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class ToDoListController : ControllerBase
    {
        List<ToDoList> _toDoLists = new List<ToDoList>()
        {
            new ToDoList() {Id = 0, Date = DateTime.Today, Name = "Estudar", Execution = true},
            new ToDoList() {Id = 1, Date = DateTime.Today, Name = "Andar de bicicleta", Execution = true},
            new ToDoList() {Id = 2, Date = DateTime.Today, Name = "Academia", Execution = false},
            new ToDoList() {Id = 3, Date = DateTime.Today, Name = "Arrumar o quarto", Execution = true},
            new ToDoList() {Id = 4, Date = DateTime.Today, Name = "Reunião", Execution = true}
        };

        [HttpGet]
        public IActionResult Gets()
        {
            Return _toDoLists;
        }

        [HttpPost]
        public IActionResult Posts(int id, DateTime date, string name, bool execution)
        {
            if (!int.IsNullOrEmpty(id)
               _toDoLists.Add(new ToDoList(id));
        }
    }
}
