using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Parsers
{
    public class TitleParser : BaseParser<Title>
    {
        public override Title Parse(HtmlNode documentNode)
        {
            var title = new Title();

            var titleNode = documentNode.SelectSingleNode("//h2[@class='postingtitle']");

            var priceNode = titleNode.SelectSingleNode("//span[@class='price']");
            if (priceNode != null)
            {
                if (int.TryParse(priceNode.InnerText.Replace("$", ""), out var price))
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
