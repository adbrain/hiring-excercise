namespace Adbrain.WebFullStack.Models
{
    public class PostDTO
    {
        public string id { get; set; }
        public string createdDate { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string permalink { get; set; }

        public static PostDTO FromPost(Post post)
        {
            PostDTO postDto = new PostDTO();

            postDto.id = post.RedditId;
            postDto.createdDate = post.DateTime.ToString("s");
            postDto.title = post.Title;
            postDto.url = post.Url;
            postDto.permalink = post.PermanentLink;

            return postDto;
        }
    }
}