using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data;
using TodoList.Data.Entities;

namespace TodoList.Service
{
    public class TodoListRepoService : ITodoListRepo
    {
        private readonly TodoListDBContext _context;

        public TodoListRepoService(TodoListDBContext context)
        {
            _context = context;
        }
        public void CreateItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAllItems()
        {
            return _context.TodoItems.ToList();
        }

        public TodoItem GetItem(int id)
        {
            return _context.TodoItems.FirstOrDefault(item => item.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemFromDatabase = GetItem(item.Id);

            itemFromDatabase.Description = item.Description;
            itemFromDatabase.IsCompleted = item.IsCompleted;

            _context.Update(itemFromDatabase);
            _context.SaveChanges();
        }

        public bool UpdateStatus(int id, bool status)
        {  
            if (id > 0)
            {
                var itemFromDatabase = GetItem(id);
                itemFromDatabase.IsCompleted = status;
                _context.Update(itemFromDatabase);
            }

            if (_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
