namespace CraigslistApartmentNotifier.Parsers
{
    using HtmlAgilityPack;

    public abstract class BaseParser<T>
    {
        public abstract T Parse(HtmlNode node);
    }
}
