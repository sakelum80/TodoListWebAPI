using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Data.Entities;

namespace TodoListApi.Data
{
    [TestClass]
    public class MockData
    {

        public IEnumerable<TodoItem> TodoList()
        {
            var toDoList = new List<TodoItem>
            {
                new TodoItem {Id =1, Description="Understand the Todlist project user stories", IsCompleted=true},
                new TodoItem {Id =2, Description="Complete high level design document", IsCompleted=true},
                new TodoItem {Id =3, Description="Create project template for API", IsCompleted=true},
                new TodoItem {Id =4, Description="Create project template for Unit testing", IsCompleted=true},
                new TodoItem {Id =5, Description="Commit the changes into github", IsCompleted=true},
                new TodoItem {Id =6, Description="Use Nuget packages - Automapper, swagger, EF-Inmemory", IsCompleted=true},
                new TodoItem {Id =7, Description="Implement User stories", IsCompleted=true},
                new TodoItem {Id =8, Description="Is complete all the userstories ", IsCompleted=false},
                new TodoItem {Id =9, Description="Integrated API with React 100%", IsCompleted=false},
                new TodoItem {Id =10, Description="Happy coding and have fun!", IsCompleted=false}
            };

            return toDoList;
        }        

    }
}
