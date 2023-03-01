using InterviewChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewChallenge.Repository.Interfaces
{
    public interface IRepositoryContext
    {
        DatabaseFacade Database { get; }

        Task<Article?> GetArticleById(Guid articleId);
        IList<Article> GetArticlesByAuthorName(string authorName);
        IList<Article> GetArticlesByCategoryName(string categoryName);
        IList<Article> GetArticlesInTimeRange(DateTime startTime, DateTime endTime);
        Task LikeArticle(Guid articleId);
        Task LikeAuthor(Guid authorId);
        Task LikeCategory(Guid categoryId);
    }
}
