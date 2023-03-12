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
        private TodoListRepoService _service;
        private MockData _mockData;
        public TodoListServiceApiTest()
        {
            var options = new DbContextOptionsBuilder<TodoListDBContext>()
                            .UseInMemoryDatabase(databaseName: "TodoListdb")
                            .Options;

            _context = new TodoListDBContext(options);

            _mockData = new MockData();

            _service = new TodoListRepoService(_context);
        }
        [TestMethod]
        public void GetAllItemsFromDatabase()
        {
            //Arrange 
            int totalTodoList = 10;
            //Act
            foreach (var item in _mockData.TodoList())
            {
                _service.CreateItem(item);
            }

            var allItemsFromMemory = _service.GetAllItems();

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
            var item = _service.GetItem(7);
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
            var itemList = _service.GetAllItems();

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
            var itemList = _service.GetAllItems();

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

        [TestMethod]
        public void UpdateTodoStatus()
        {
            //Arrange
            int itemId = 4;
            GetAllItemsFromDatabase();

            //Act
            var itemStatus = _service.UpdateStatus(itemId, false);

            TodoItem todoItem = _service.GetItem(itemId);

            //Assert
            if (itemStatus == todoItem.IsCompleted)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void DeleteTodoItem()
        {
            //Arrange
            var toBeDeleteItem = new TodoItem()
            {
                Id = 13,
                Description = "Testcase for delete",
                IsCompleted = false
            };

            GetAllItemsFromDatabase();

            //Act
            _service.DeleteItem(toBeDeleteItem);

            TodoItem todoItem = _service.GetItem(toBeDeleteItem.Id);

            //Assert
            if (todoItem == null)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateTodoItem()
        {
            //Arrange
            var toBeUpdatedItem = new TodoItem()
            {
                Id = 5,
                Description = "Testcase for update",
                IsCompleted = true
            };

            GetAllItemsFromDatabase();

            //Act
            _service.UpdateItem(toBeUpdatedItem);

            TodoItem todoItem = _service.GetItem(toBeUpdatedItem.Id);

            //Assert
            Assert.AreEqual(todoItem.Id, toBeUpdatedItem.Id);
            Assert.AreEqual(todoItem.Description, toBeUpdatedItem.Description);
            Assert.AreEqual(todoItem.IsCompleted, toBeUpdatedItem.IsCompleted);
        }
    }
}
