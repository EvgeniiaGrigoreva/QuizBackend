using System.Net;
using Newtonsoft.Json;
using QuizBackend.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Controllers;




namespace QuizBackend.Tests
{
    [TestClass]


    public class UsersControllerTests
    {
        private AppDbContext GetMemoryContext()
        {
            // Создаем уникальное имя базы для каждого теста, чтобы они не мешали друг другу
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [TestMethod]
        public async Task PostUser_ReturnsBadRequest_WhenUserIsNull()
        {
            // 1. Arrange (Подготовка)
            var db = GetMemoryContext();
            var controller = new UsersController(db);

            // 2. Act (Действие)
            var result = await controller.PostUser(null);

            // 3. Assert (Проверка)
            // Мы ожидаем, что вернется BadRequest, так как в коде стоит if (user == null)
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PostUser_AddsUserToDb_WhenDataIsValid()
        {
            // Arrange
            var db = GetMemoryContext();
            var controller = new UsersController(db);
            var testUser = new User { UserId = 1, Login = "Student", Password = "123" };

            // Act
            await controller.PostUser(testUser);

            // Assert
            // Проверяем, что в нашей "памяти" теперь реально лежит 1 пользователь
            var count = await db.Users.CountAsync();
            Assert.AreEqual(1, count);
        }
    }
    }
