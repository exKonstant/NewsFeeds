namespace NewsFeeds.API.Models.Feeds
{
    public class FeedModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int FeedCollectionId { get; set; }
    }
}
