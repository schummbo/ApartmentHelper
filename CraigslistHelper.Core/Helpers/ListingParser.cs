using System;
using System.Collections.Generic;
using CraigslistHelper.Core.Entities;
using CraigslistHelper.Core.Processors;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Helpers
{
    public class ListingParser
    {
        private readonly List<BaseProcessor> _parsers;

        public ListingParser(Settings settings)
        {
            _parsers = new List<BaseProcessor>
            {
                new TitleProcessor(settings),
                new HousingProcessor(settings),
                new MapPointProcessor(settings),
                new RouteProcessor(settings),
                new CityProcessor(settings),
                new BodyProcessor(settings)
            };
        }

        public ApartmentListing ParseListing(HtmlNode apartmentNode)
        {
            ApartmentListing listing = new ApartmentListing
            {
                Url = apartmentNode.Attributes["href"].Value
            };

            try
            {
                Console.WriteLine("Processing apartment...");

                HtmlWeb listingHw = new HtmlWeb();
                HtmlDocument listDocument = listingHw.Load(listing.Url);

                var node = listDocument.DocumentNode;

                foreach (var parser in _parsers)
                {
                    parser.Parse(node, listing);
                }
            }
            catch (Exception)
            {
                //listing.ConfidenceLevel = ConfidenceLevel.Failed;
            }

            return listing;
        }
    }
}
