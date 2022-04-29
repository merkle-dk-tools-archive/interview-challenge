namespace InterviewChallenge.Services
{
    /// <summary>
    /// Completely useless service, that only exists to demonstrate unstable behaviour, actual references to articles are not even present...
    /// </summary>
    public class UnstableMediaService
    {
        public async Task<IEnumerable<string>> GetArticleMedia(string articleId)
        {
            var images = new List<string>
            {
                "https://unsplash.com/photos/vfOtKkhHkbE",
                "https://unsplash.com/photos/g1Kr4Ozfoac",
                "https://unsplash.com/photos/Yh2Y8avvPec",
                "https://unsplash.com/photos/wS73LE0GnKs",
                "https://unsplash.com/photos/-uHVRvDr7pg",
                "https://unsplash.com/photos/rg1y72eKw6o",
            };

            var randomDelayMilliseconds = Random.Shared.Next(100, 2100);

            await Task.Delay(randomDelayMilliseconds);

            if (randomDelayMilliseconds > 2000)
            {
                throw new TimeoutException("The request timed out :/");
            }

            return images.Take(Random.Shared.Next(0, images.Count));
        }
    }
}