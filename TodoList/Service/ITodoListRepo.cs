using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Entities;

namespace TodoList.Service
{
    public interface ITodoListRepo
    {
        IEnumerable<TodoItem> GetAllItems();
        TodoItem GetItem(int id);
        void CreateItem(TodoItem item);
        void UpdateItem(TodoItem item);

        void DeleteItem(TodoItem item);

        bool UpdateStatus(int id, bool status);

        bool SaveChanges();

    }
}
