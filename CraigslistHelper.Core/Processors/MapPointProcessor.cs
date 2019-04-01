using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Processors
{
    public class MapPointProcessor : BaseProcessor
    {
        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            var mapNode = node.SelectSingleNode("//div[@id='map']");

            if (mapNode == null)
            {
                listing.Origin = null;
                return;
            }

            listing.Origin = new MapPoint
            {
                Latitude = mapNode.Attributes["data-latitude"].Value,
                Longitude = mapNode.Attributes["data-longitude"].Value
            };
        }

        public MapPointProcessor(Settings settings) : base(settings)
        {
        }
    }
}
