namespace Wikibird.Core.Implementations
{
    public static class PageIdGenerator
    {
        public static string AsPageId(this string name)
        {
            return "page/" + name;
        }
    }
}