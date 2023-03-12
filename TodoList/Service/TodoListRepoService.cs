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
        private readonly TodoListDBContext _toDoListContext;

        public TodoListRepoService(TodoListDBContext toDoListDBContext)
        {
            _toDoListContext = toDoListDBContext;
        }
        public void CreateItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _toDoListContext.TodoItems.Add(item);
            _toDoListContext.SaveChanges();
        }

        public void DeleteItem(TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _toDoListContext.TodoItems.Remove(item);
            _toDoListContext.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAllItems()
        {
            return _toDoListContext.TodoItems.ToList();
        }

        public TodoItem GetItem(int id)
        {
            return _toDoListContext.TodoItems.FirstOrDefault(item => item.Id == id);
        }

        public bool SaveChanges()
        {
            return (_toDoListContext.SaveChanges() >= 0);
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

            _toDoListContext.Update(itemFromDatabase);
            _toDoListContext.SaveChanges();
        }

        public bool UpdateStatus(int id, bool status)
        {  
            if (id > 0)
            {
                var itemFromDatabase = GetItem(id);
                itemFromDatabase.IsCompleted = status;
                _toDoListContext.Update(itemFromDatabase);
            }

            if (_toDoListContext.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
