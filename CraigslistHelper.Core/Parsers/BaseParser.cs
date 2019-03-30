using HtmlAgilityPack;

namespace CraigslistHelper.Core.Parsers
{
    public abstract class BaseParser<T>
    {
        public abstract T Parse(HtmlNode node);
    }
}
