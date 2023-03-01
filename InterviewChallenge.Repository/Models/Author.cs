namespace InterviewChallenge.Repository.Models
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Article> Articles { get; set; }
        public int Likes { get; set; }
    }
}