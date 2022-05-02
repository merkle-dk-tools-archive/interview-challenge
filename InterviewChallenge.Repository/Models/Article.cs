namespace InterviewChallenge.Repository.Models
{
    public class Article
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Teaser { get; set; }

        public DateTime Published { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
