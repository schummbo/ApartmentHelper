using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Processors
{
    public class TitleProcessor : BaseProcessor
    {
        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            var title = new Title();

            var titleNode = node.SelectSingleNode("//h2[@class='postingtitle']");

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
            listing.Title = title.Text;
            listing.Price = title.Price;
        }

        public TitleProcessor(Settings settings) : base(settings)
        {
        }
    }
}