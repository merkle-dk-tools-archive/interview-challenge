using InterviewChallenge.Repository;
using InterviewChallenge.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InterviewChallenge.Api.Controllers;

public class ArticlesController : Controller
{
    private readonly IRepositoryContext _databaseContext;

    public ArticlesController(IRepositoryContext databaseContext)
    {
        _databaseContext = databaseContext;
        databaseContext.Database.EnsureCreated();
    }

    // GET
    [HttpGet("get-article-by-id")]
    public async Task<IActionResult> GetArticle(Guid articleId)
    {
        var article = await _databaseContext.GetArticleById(articleId);

        if (article == null)
        {
            return NotFound();
        }

        return new OkObjectResult(article);
    }

    // GET
    [HttpGet("get-articles-by-author-name")]
    public async Task<IActionResult> GetArticlesByAuthor(string authorName)
    {
        var articles = _databaseContext.GetArticlesByAuthorName(authorName);

        return new OkObjectResult(articles);
    }

    // GET
    [HttpGet("get-articles-by-category-name")]
    public async Task<IActionResult> GetArticlesByCategory(string categoryName)
    {
        var articles = _databaseContext.GetArticlesByCategoryName(categoryName);

        return new OkObjectResult(articles);
    }

    // GET
    [HttpGet("get-articles-in-time-range")]
    public async Task<IActionResult> GetArticlesInTimeRange(DateTime startTime, DateTime endTime)
    {
        var articles = _databaseContext.GetArticlesInTimeRange(startTime, endTime);

        return new OkObjectResult(articles);
    }
}