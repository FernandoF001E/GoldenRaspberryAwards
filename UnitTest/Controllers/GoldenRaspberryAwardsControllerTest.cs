using Entity.Data;
using Infrastruture.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApi.Controllers;

namespace UnitTest.Controllers
{
    public class GoldenRaspberryAwardsControllerTest
    {
        [Fact]
        public void GetIntervals_ShouldReturnCorrectResults()
        {
            var options = new DbContextOptionsBuilder<AwardsContext>()
                .UseInMemoryDatabase(databaseName: "AwardsTestDB")
                .Options;

            using var context = new AwardsContext(options);

            context.Awards.AddRange(new List<Award>
            {
                new() { Year = 1980, Title = "Movie A", Producers = "Producer 1", Winner = "yes" },
                new() { Year = 1981, Title = "Movie B", Producers = "Producer 1", Winner = "yes" },
                new() { Year = 2020, Title = "Movie C", Producers = "Producer 2", Winner = "yes" },
                new() { Year = 2024, Title = "Movie D", Producers = "Producer 2", Winner = "yes" }
            });

            context.SaveChanges();

            var controller = new GoldenRaspberryAwardsController(context);

            var result = controller.GetIntervals() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var json = JsonSerializer.Serialize(result.Value);
            var response = JsonSerializer.Deserialize<IntervalResponse>(json);

            Assert.NotNull(response);

            Assert.Equal(2, response.Min.Count);
            Assert.Equal("Producer 1", response.Min.First().Producer);
            Assert.Equal(1, response.Min.First().Interval);

            Assert.Equal(2, response.Max.Count);
            Assert.Equal("Producer 2", response.Max.First().Producer);
            Assert.Equal(4, response.Max.First().Interval);
        }
    }
}
