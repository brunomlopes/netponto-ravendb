namespace Wikibird.Models
{
    public class Page
    {
        public static Page EmptyPage = new Page();

        public string Title { get; set; }
        public string Content { get; set; }
    }
}