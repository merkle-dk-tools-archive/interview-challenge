using InterviewChallenge.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewChallenge.Repository
{
    public class RepositoryContext : DbContext
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
            return await Articles.FindAsync(articleId);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var firstArticleId = new Guid("{851A0AE9-CAB3-4F6B-984C-C747072F094E}");

            var firstAuthorId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0941}");
            var secondAuthorId = new Guid("{851A0AE9-CAB3-4F6B-984A-C747072F0942}");

            //Add seeding here
            modelBuilder.Entity<Author>().HasData(new List<Author>
            {
                new Author{ AuthorId = firstAuthorId, Name = "J.K. Brawling"},
                new Author{ AuthorId = secondAuthorId, Name = "William Rumblespeare"}
            });

            //Add seeding here
            modelBuilder.Entity<Article>().HasData(new List<Article>
            {
                new Article {ArticleId = firstArticleId, AuthorId = firstAuthorId, Published = new DateTime(2021,1,30), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2015,5,1), Title = "Hypertext Transfer Protocol Version 2 (HTTP/2)", Teaser = "This specification describes an optimized expression of the semantics of the Hypertext Transfer Protocol (HTTP), referred to as HTTP version 2 (HTTP/2)."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2021,1,23), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = firstAuthorId, Published = new DateTime(2021,4,14), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,3,19), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,11,20), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,10,4), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},
                new Article {ArticleId = Guid.NewGuid(), AuthorId = secondAuthorId, Published = new DateTime(2021,7,8), Title = "My favorite features in C#", Teaser = "In this article, we’ll take a look at the new features in C# 9, and how they can be used to improve your code."},

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}