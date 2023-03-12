using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoList.Data;
using System.Linq;
using TodoListApi.Data;
using System.Collections.Generic;
using TodoList.Data.Entities;

namespace TodoList.Service.UnitTests
{
    [TestClass]
    public class TodoListServiceApiTest
    {
        private readonly TodoListDBContext _context;
        private TodoListRepoService _toDoListService;
        private MockData _mockData;
        public TodoListServiceApiTest()
        {
            var options = new DbContextOptionsBuilder<TodoListDBContext>()
                            .UseInMemoryDatabase(databaseName: "TodoListdb")
                            .Options;

            _context = new TodoListDBContext(options);

            _mockData = new MockData();

            _toDoListService = new TodoListRepoService(_context);
        }
        [TestMethod]
        public void GetAllItemsFromDatabase()
        {
            //Arrange 
            int totalTodoList = 10;
            //Act
            foreach (var item in _mockData.TodoList())
            {
                _toDoListService.CreateItem(item);
            }

            var allItemsFromMemory = _toDoListService.GetAllItems();

            //Assert
            Assert.IsNotNull(allItemsFromMemory);
            Assert.IsTrue(allItemsFromMemory.Count().Equals(totalTodoList));
        }

        [TestMethod]
        public void GetOneItemFromList()
        {
            //Arrange
            GetAllItemsFromDatabase();
            //Act
            var item = _toDoListService.GetItem(7);
            //Assert
            Assert.IsNotNull(item);       
        }

        [TestMethod]
        public void GetOnlyCompletedItemList()
        {
            //Arrange
            int completeItemCount = 7;
            GetAllItemsFromDatabase();
            //Act
            var itemList = _toDoListService.GetAllItems();

            List<TodoItem> completedItemList = new List<TodoItem>();

            foreach (var item in itemList)
            {
                if (item.IsCompleted)
                {
                    completedItemList.Add(item);
                }                
            }

            //Assert
            Assert.IsNotNull(completedItemList);
            Assert.IsTrue(completedItemList.Count().Equals(completeItemCount));
        }

        [TestMethod]
        public void GetOnlyPendingItemList()
        {
            //Arrange
            int completeItemCount = 3;
            GetAllItemsFromDatabase();
            //Act
            var itemList = _toDoListService.GetAllItems();

            List<TodoItem> pendingItemList = new List<TodoItem>();

            foreach (var item in itemList)
            {
                if (!item.IsCompleted)
                {
                    pendingItemList.Add(item);
                }
            }

            //Assert
            Assert.IsNotNull(pendingItemList);
            Assert.IsTrue(pendingItemList.Count().Equals(completeItemCount));
        }
    }
}
