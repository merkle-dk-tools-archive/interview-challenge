using InterviewChallenge.Api.Controllers;
using InterviewChallenge.Repository;
using InterviewChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Serilog;
using Serilog.Sinks.InMemory;

namespace InterviewChallenge.ApiFixtures
{
    [TestFixture]
    public class ArticlesControllerFixture
    {
        private RepositoryContext _databaseContext;
        private ILogger _logger;
        private UnstableMediaService _mediaService;

        [OneTimeSetUp]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseInMemoryDatabase("interviewTestDB");
            _databaseContext = new RepositoryContext(optionsBuilder.Options);
            _logger = new LoggerConfiguration()
                .WriteTo.InMemory()
                .CreateLogger();
            _mediaService = new UnstableMediaService();
        }

        [Test]
        public async Task GetArticleById_WithExistingId_ReturnsArticleInOKResult()
        {
            //Arrange
            var controller = new ArticlesController(_databaseContext, _logger, _mediaService);
            var articleId = new Guid("{851A0AE9-CAB3-4F6B-984C-C747072F094E}"); // id is from seed - expected to exist

            //Act
            IActionResult result = await controller.GetArticle(articleId);

            //Assert
            Assert.AreEqual(true, result is OkObjectResult);
            Assert.IsNotNull((result as OkObjectResult)?.Value);
        }

        /// <summary>
        /// Task #2 Get by author name
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetArticlesByAuthorName_WithExistingAuthorName_ReturnsOKResult()
        {
            //Arrange 
            var controller = new ArticlesController(_databaseContext, _logger, _mediaService);
            var authorName = "J.K. Brawling";

            //Act
            IActionResult result = await controller.GetArticlesByAuthor(authorName);

            //Assert
            Assert.AreEqual(true, result is OkObjectResult);
            Assert.IsNotNull((result as OkObjectResult)?.Value);
        }

        /// <summary>
        /// Task #2 Get by category name
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetArticlesByCategoryName_WithExistingCategoryName_ReturnsOKResult()
        {
            //Arrange 
            var controller = new ArticlesController(_databaseContext, _logger, _mediaService);
            var categoryName = "Category 1";

            //Act
            IActionResult result = await controller.GetArticlesByCategory(categoryName);

            //Assert
            Assert.AreEqual(true, result is OkObjectResult);
            Assert.IsNotNull((result as OkObjectResult)?.Value);
        }

        /// <summary>
        /// Task #2 Get by time range
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetArticlesByTimeRange_WithValidTimeRange_ReturnsArticleInOKResult()
        {
            //Arrange 
            var controller = new ArticlesController(_databaseContext, _logger, _mediaService);
            var startTime = new DateTime(2015, 4, 1);
            var endTime = new DateTime(2016, 1, 1);

            //Act
            IActionResult result = await controller.GetArticlesInTimeRange(startTime, endTime);

            //Assert
            Assert.AreEqual(true, result is OkObjectResult);
            Assert.IsNotNull((result as OkObjectResult)?.Value);
        }
    }
}