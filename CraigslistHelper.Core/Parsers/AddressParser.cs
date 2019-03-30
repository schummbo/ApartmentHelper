using HtmlAgilityPack;

namespace CraigslistHelper.Core.Parsers
{
    public class AddressParser : BaseParser<string>
    {
        public override string Parse(HtmlNode node)
        {
            return node.SelectSingleNode("//div[@class='mapaddress']")?.InnerText;
        }
    }
}
