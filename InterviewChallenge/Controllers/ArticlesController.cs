using InterviewChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InterviewChallenge.Api.Controllers;

public class ArticlesController : Controller
{
    private readonly RepositoryContext _databaseContext;

    public ArticlesController(RepositoryContext databaseContext)
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
}