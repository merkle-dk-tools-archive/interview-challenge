using InterviewChallenge.Services;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int Likes { get; set; }
        [NotMapped]
        public IEnumerable<string> Images { get; set; }
    }
}
