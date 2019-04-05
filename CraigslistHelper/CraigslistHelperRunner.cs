using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CraigslistHelper.Core.Entities;
using CraigslistHelper.Core.Evaluators;
using CraigslistHelper.Core.Helpers;
using CraigslistHelper.Core.Output;
using HtmlAgilityPack;

namespace CraigslistHelper
{
    public class CraigslistHelperRunner
    {
        private readonly Settings _settings;

        public CraigslistHelperRunner(Settings settings)
        {
            _settings = settings;
        }

        public void Run()
        {
            List<ApartmentListing> listings = new List<ApartmentListing>();

            Console.WriteLine("About to go to craigslist.");

            HtmlWeb list = new HtmlWeb();

            var url = new CraigslistUrlBuilder(_settings).BuildUrl();

            HtmlDocument document = list.Load(url);

            Console.WriteLine("Got the listing. Enumerating.");

            var apartments = GetApartments(document);

            var listingParser = new ListingParser(_settings);

            var scoreEvaluator = new ScoreEvaluator(CreateEvaluators());

            foreach (HtmlNode apartment in apartments)
            {
                ApartmentListing listing = listingParser.ParseListing(apartment);

                listings.Add(listing);

                listing.Score = scoreEvaluator.GetApartmentScore(listing);
            }

            string html = new HtmlOutput().Execute(listings);

            File.WriteAllText($"Apartments-{DateTime.Now:yy_MM_dd_hh_mm}.html", html);

            System.Diagnostics.Process.Start("explorer.exe", Directory.GetCurrentDirectory());
        }

        private List<BaseEvaluator> CreateEvaluators()
        {
            return new List<BaseEvaluator>
            {
                new PriceEvaluator(new Range { Max = 1500, Min = 0 }, new Range { Max = 2000, Min = 1501 }),
                new TravelTimeEvaluator(new Range { Max = 30 * 60, Min = 0 }, new Range { Max = 60 * 60, Min = 31 * 60 }),
                new BedroomEvaluator(new Range { Max = 1, Min = 2 }, new Range { Max = 3, Min = 3 }),
                new SquareFootEvaluator(new Range { Max = 1000, Min = 700 }, new Range { Max = 1500, Min = 701 })
            };
        }

        private static HtmlNodeCollection GetApartments(HtmlDocument document)
        {
            // Check for nodes before the "nearby" row which appears if there aren't many results for the searched area
            var apartmentNodes =
                document.DocumentNode.SelectNodes(
                    "//ul[contains(@class, 'rows')]/h4/preceding-sibling::li//a[contains(@class, 'hdrlnk')]");

            // if no nodes, then that "nearby" row probably didn't show. Just get the apartments.
            if (apartmentNodes == null || !apartmentNodes.Any())
            {
                apartmentNodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'hdrlnk')]");
            }

            return apartmentNodes;
        }
    }
}
