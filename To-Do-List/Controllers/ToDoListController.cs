using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List.Data;
using To_Do_List.Models;
using To_Do_List.ViewModels;

namespace To_Do_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoListContext _context;

        public ToDoListController(ToDoListContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> PostToDoList(ToDoList toDoList)
        {
            _context.ToDoLists.Add(toDoList);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetToDoList), new { id = toDoList.Id }, toDoList);
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoViewModel> GetToDoList(int id)
        {
            var toDoList = _context.ToDoLists.Find(id);

            if (toDoList == null)
            {
                return NotFound();
            }

            return ViewModelToDo(toDoList);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoViewModel>> GetToDoList()
        {
            return _context.ToDoLists.Select(x => new ToDoViewModel()
            {
                Date = x.Date,
                Execution = x.Execution,
                Name = x.Name
            }).ToList();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDolist(int id)
        {
            var toDoList = await _context.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            _context.ToDoLists.Remove(toDoList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static ToDoViewModel ViewModelToDo(ToDoList toDoList) =>
            new ToDoViewModel
            {
                Id = toDoList.Id,
                Date = toDoList.Date,
                Name = toDoList.Name,
                Execution = toDoList.Execution
            };
    }
}

