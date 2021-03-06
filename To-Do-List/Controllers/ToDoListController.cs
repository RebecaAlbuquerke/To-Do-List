using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List.Data;
using To_Do_List.InputModel;
using To_Do_List.Models;
using To_Do_List.ViewModels;

namespace To_Do_List.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly ToDoItemContext _context;

        public ToDoListController(ToDoItemContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoViewModel>> PostToDoList(ToDoViewModel toDoViewModel)
        {
            var toDoItem = new ToDoItem
            {
                Date = toDoViewModel.Date,
                Name = toDoViewModel.Name,
                Execution = toDoViewModel.Execution
            };

            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetToDoList), new { id = toDoItem.Id }, ViewModelToDo(toDoItem));
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoViewModel> GetToDoList(int id)
        {
            var toDoItem = _context.ToDoItems.Find(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return ViewModelToDo(toDoItem);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ToDoViewModel>> GetToDoList(int? page, int pageSize)
        {
            var items = _context.ToDoItems.Select(x => new ToDoViewModel()
            {
                Id = x.Id,
                Date = x.Date,
                Execution = x.Execution,
                Name = x.Name
            });

            if (pageSize >= 15 && pageSize <= 100)
            {
                var paginatedItems = items.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();
                return paginatedItems;
            }

            return BadRequest("O tamanho deve está entre 15 e 100");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDolist(int id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoList(int id, ToDoInputModel toDoInputModel)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            toDoItem.Date = toDoInputModel.Date;
            toDoItem.Name = toDoInputModel.Name;
            toDoItem.Execution = toDoInputModel.Execution;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static ToDoViewModel ViewModelToDo(ToDoItem toDoItem) =>
            new ToDoViewModel
            {
                Id = toDoItem.Id,
                Date = toDoItem.Date,
                Name = toDoItem.Name,
                Execution = toDoItem.Execution
            };
    }
}

