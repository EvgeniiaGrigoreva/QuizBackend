using System.Net;
using Newtonsoft.Json;
using QuizBackend.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;




namespace QuizBackend.Tests
{
    [TestClass]

   
    public class Quiztests
    {
        [TestMethod]
        public async Task TestResultAdd()
        {

            WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            HttpClient client = application.CreateClient();

            Result result = new Result();
            result.CorAnswer = 15;
            result.Date = DateTime.Now;
                                 
            string input = JsonConvert.SerializeObject(result);
            StringContent content = new StringContent(input, Encoding.UTF8, "application/json");

            var responsePost = await client.PostAsync("api/Results", content);

            Assert.AreEqual("Created", responsePost.StatusCode.ToString());

        }
        [TestMethod]
        public async Task TestResultGetId5()
        {
            var app = new WebApplicationFactory<Program>();
            var client = app.CreateClient();

            var getResponse = await client.GetAsync("api/Results/5");
            Assert.AreEqual(HttpStatusCode.OK, getResponse.StatusCode);
        }

            [TestMethod]
        public async Task TestResultDeleteAllWithCorAnswer15()
        {
            var app = new WebApplicationFactory<Program>();
            var client = app.CreateClient();
                      
            var getResponse = await client.GetAsync("api/Results");
            getResponse.EnsureSuccessStatusCode();

            var json = await getResponse.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<Result>>(json);

            
            var toDelete = results.Where(r => r.CorAnswer == 15).ToList();

            
            foreach (var r in toDelete)
            {
                var deleteResponse = await client.DeleteAsync($"api/Results/{r.Id}");
                Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            }
        }
    }
}
