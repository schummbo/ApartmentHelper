namespace CraigslistApartmentNotifier.Parsers
{
    using HtmlAgilityPack;
    using PageEntities;

    public class MapPointParser : BaseParser<MapPoint>
    {
        public override MapPoint Parse(HtmlNode node)
        {
            HtmlNode mapNode = node.SelectSingleNode("//div[@id='map']");

            if (mapNode == null)
            {
                return null;
            }

            return new MapPoint
            {
                Latitude = mapNode.Attributes["data-latitude"].Value,
                Longitude = mapNode.Attributes["data-longitude"].Value
            };
        }
    }
}
