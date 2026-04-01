using System.Net;
using Newtonsoft.Json;
using QuizBackend.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Controllers;
using static QuizBackend.Controllers.Endpoints;




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

    [TestClass]
    public class EndpointsTests
    {
        private AppDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [TestMethod]
        public async Task SaveList_ShouldCreateQuizWithQuestions_WhenDataIsValid()
        {
            var db = GetMemoryContext();
            var controller = new Endpoints(db);

            var request = new CreateQuiz
            {
                UserID = 1,
                QName = "Science Quiz",
                NewQuiz = new List<NewQuiz>
                 {
                    new NewQuiz
                    {
                        Text = "Is the Earth round?",
                        AnsTrue = "Yes",
                        AnsFalse1 = "No",
                        AnsFalse2 = "It is a cube"
                    }
        }
            };
                       
        var result = await controller.SaveList(request);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var savedQuiz = await db.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.QuizName == "Science Quiz");

            Assert.IsNotNull(savedQuiz, "Quiz was not saved to the database.");
            Assert.AreEqual(1, savedQuiz.Questions.Count);

            var firstQuestion = savedQuiz.Questions.First();
            Assert.AreEqual(3, firstQuestion.Answers.Count);
            Assert.IsTrue(firstQuestion.Answers.Any(a => a.IsCorrect && a.AnswerText == "Yes"));
        }
         [TestMethod]
        public async Task SaveList_ShouldReturnBadRequest_WhenQuestionsListIsEmpty()
    {
        var db = GetMemoryContext();
        var controller = new Endpoints(db);
        var request = new CreateQuiz
        {
            UserID = 1,
            QName = "Empty Quiz",
            NewQuiz = new List<NewQuiz>() 
        };
        var result = await controller.SaveList(request);

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

}
[TestClass]
public class ResultsControllerTests
{
    private AppDbContext GetMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [TestMethod]
    public async Task GetLatest10Results_ShouldReturnOnlyTenRecords_InDescendingOrder()
    {
        
        var db = GetMemoryContext();
        var controller = new ResultsController(db);

        for (int i = 1; i <= 12; i++)
        {
            db.Results.Add(new Result { Id = i, CorAnswer = i * 10 });
        }
        await db.SaveChangesAsync();

      
        var actionResult = await controller.GetLatest10Results();

        var results = actionResult.Value as List<Result>;

        Assert.IsNotNull(results, "The result list should not be null.");

        Assert.AreEqual(10, results.Count, "Should return exactly 10 records.");

        Assert.AreEqual(12, results.First().Id, "The first record should be the latest one (ID 12).");

        Assert.AreEqual(3, results.Last().Id, "The last record in the list should be ID 3.");
    }
}

