using InterviewChallenge.Repository.Interfaces;
using InterviewChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace InterviewChallenge.Repository
{
    public class RepositoryContext : DbContext, IRepositoryContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Skipping actual sql db to keep focus on tasks and not connection strings
            //optionsBuilder
            //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFDataSeeding;Trusted_Connection=True");

            optionsBuilder
                .UseInMemoryDatabase("InterviewChallenge");
        }

        public async Task<Article?> GetArticleById(Guid articleId)
        {
            return await Articles.Include(a => a.Author).Include(a => a.Category).FirstOrDefaultAsync(a => a.ArticleId == articleId);
        }

        public IList<Article> GetArticlesByAuthorName(string authorName)
        {
            return Articles.Include(a => a.Author).Include(a => a.Category).Where(a => a.Author.Name == authorName).OrderByDescending(a => a.Published).ToList();
        }

        public IList<Article> GetArticlesByCategoryName(string categoryName)
        {
            return Articles.Include(a => a.Author).Include(a => a.Category).Where(a => a.Category.CategoryName == categoryName).OrderBy(a => a.Category.CategoryName).ToList();
        }

        public IList<Article> GetArticlesInTimeRange(DateTime startTime, DateTime endTime)
        {
            return Articles.Include(a => a.Author).Include(a => a.Category).Where(a => a.Published >= startTime && a.Published <= endTime).ToList();
        }

        public async Task LikeArticle(Guid articleId)
        {
            var article = await Articles.FindAsync(articleId);
            
            if (article == null)
            {
                throw new KeyNotFoundException();
            }

            article.Likes++;
            Articles.Update(article);
        }

        public async Task LikeAuthor(Guid authorId)
        {
            var author = await Authors.FindAsync(authorId);

            if (author == null)
            {
                throw new KeyNotFoundException();
            }

            author.Likes++;
            Authors.Update(author);
        }

        public async Task LikeCategory(Guid categoryId)
        {
            var category = await Categories.FindAsync(categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException();
            }

            category.Likes++;
            Categories.Update(category);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var firstArticleId = new Guid("{851A0AE9-CAB3-4F6B-984C-C747072F094E}");

            var firstAuthorId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0941}");
            var secondAuthorId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0942}");

            var firstCategoryId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0943}");
            var secondCategoryId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0944}");

            //Add seeding here
            modelBuilder.Entity<Author>().HasData(new List<Author>
            {
                new Author{ AuthorId = firstAuthorId, Name = "J.K. Brawling", Likes = 0},
                new Author{ AuthorId = secondAuthorId, Name = "William Rumblespeare", Likes = 0}
            });

            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new Category { CategoryId = firstCategoryId, CategoryName = "Category 1", Description = "This is category 1", Likes = 0 },
                new Category { CategoryId = secondCategoryId, CategoryName = "Category 2", Description = "This is category 2", Likes = 0 }
            });

            //Add seeding here
            modelBuilder.Entity<Article>().HasData(new List<Article>
            {
                new Article {ArticleId = firstArticleId, AuthorId = firstAuthorId, Published = new DateTime(2021,1,30), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = firstCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2015,5,1), Title = "Hypertext Transfer Protocol Version 2 (HTTP/2)", Teaser = "This specification describes an optimized expression of the semantics of the Hypertext Transfer Protocol (HTTP), referred to as HTTP version 2 (HTTP/2).", CategoryId = firstCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2021,1,23), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = firstCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2021,4,14), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = firstCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,3,19), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = secondCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,11,20), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = secondCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,10,4), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = secondCategoryId, Likes = 0 },
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,7,8), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code.", CategoryId = secondCategoryId, Likes = 0 },

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}