using InterviewChallenge.Repository;
using InterviewChallenge.Repository.Interfaces;
using InterviewChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace InterviewChallenge.Api.Controllers;

public class ArticlesController : Controller
{
    private readonly IRepositoryContext _databaseContext;
    private readonly ILogger _logger;
    private readonly UnstableMediaService _mediaService;

    public ArticlesController(IRepositoryContext databaseContext, ILogger logger, UnstableMediaService mediaService)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
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

        article.Images = await GetImages(articleId);

        return new OkObjectResult(article);
    }

    // GET
    [HttpGet("get-articles-by-author-name")]
    public async Task<IActionResult> GetArticlesByAuthor(string authorName)
    {
        var articles = _databaseContext.GetArticlesByAuthorName(authorName);

        foreach (var article in articles)
        {
            article.Images = await GetImages(article.ArticleId);
        }

        return new OkObjectResult(articles);
    }

    // GET
    [HttpGet("get-articles-by-category-name")]
    public async Task<IActionResult> GetArticlesByCategory(string categoryName)
    {
        var articles = _databaseContext.GetArticlesByCategoryName(categoryName);

        foreach (var article in articles)
        {
            article.Images = await GetImages(article.ArticleId);
        }

        return new OkObjectResult(articles);
    }

    // GET
    [HttpGet("get-articles-in-time-range")]
    public async Task<IActionResult> GetArticlesInTimeRange(DateTime startTime, DateTime endTime)
    {
        var articles = _databaseContext.GetArticlesInTimeRange(startTime, endTime);

        foreach (var article in articles)
        {
            article.Images = await GetImages(article.ArticleId);
        }

        return new OkObjectResult(articles);
    }

    // GET
    [HttpGet("like-article")]
    public async Task<IActionResult> LikeArticle(Guid articleId)
    {
        await _databaseContext.LikeArticle(articleId);

        return Ok();
    }

    // GET
    [HttpGet("like-author")]
    public async Task<IActionResult> LikeAuthor(Guid authorId)
    {
        await _databaseContext.LikeAuthor(authorId);

        return Ok();
    }

    // GET
    [HttpGet("like-category")]
    public async Task<IActionResult> LikeCategory(Guid categoryId)
    {
        await _databaseContext.LikeCategory(categoryId);

        return Ok();
    }

    private async Task<IEnumerable<string>> GetImages(Guid articleId)
    {
        var downloadTask = _mediaService.GetArticleMedia(articleId);

        try
        {
            var result = await Task.WhenAny(downloadTask, Task.Delay(TimeSpan.FromSeconds(2)));

            if (result == downloadTask)
            {
                return downloadTask.Result;
            }
        }
        catch (TimeoutException ex)
        {
            
        }

        return new List<string>();
    }
}