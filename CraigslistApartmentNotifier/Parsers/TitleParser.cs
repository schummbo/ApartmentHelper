namespace CraigslistApartmentNotifier.Parsers
{
    using Entities;
    using HtmlAgilityPack;

    public class TitleParser : BaseParser<Title>
    {
        public override Title Parse(HtmlNode documentNode)
        {
            Title title = new Title();

            HtmlNode titleNode = documentNode.SelectSingleNode("//h2[@class='postingtitle']");

            var priceode = titleNode.SelectSingleNode("//span[@class='price']");
            if (priceode != null)
            {
                int price;

                if (int.TryParse(priceode.InnerText.Replace("$", ""), out price))
                {
                    title.Price = price;
                }
            }

            title.Housing = titleNode.SelectSingleNode("//span[@class='housing']")?.InnerText.Replace("/", "");
            title.Text = titleNode.SelectSingleNode("//span[@id='titletextonly']")?.InnerText;

            return title;
        }
    }
}
