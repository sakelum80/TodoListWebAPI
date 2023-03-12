using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Data.Entities;

namespace TodoList.Data
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options) : base(options)
        {
                
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
