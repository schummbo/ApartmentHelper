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
        private readonly List<BaseEvaluator> _evaluators;

        public CraigslistHelperRunner(Settings settings, List<BaseEvaluator> evaluators)
        {
            _settings = settings;
            _evaluators = evaluators;
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

            var scoreEvaluator = new ScoreCalculator(_evaluators);

            foreach (HtmlNode apartment in apartments)
            {
                ApartmentListing listing = listingParser.ParseListing(apartment);

                listing.Score = scoreEvaluator.GetApartmentScore(listing);

                if (ShouldBeSkipped(_settings, listing))
                {
                    Console.WriteLine("Skipping...");
                }
                else
                {
                    listings.Add(listing);
                }
            }

            string html = new HtmlOutput().Execute(listings);

            var filePath = Path.Combine(_settings.saveFileLocation, $"Apartments-{DateTime.Now:yy_MM_dd_hh_mm}.html");

            File.WriteAllText(filePath, html);
        }

        private bool ShouldBeSkipped(Settings settings, ApartmentListing listing)
        {
            return (_settings.hideZeros && listing.Score <= 0) ||
                   settings.bannedPhrases.Any(bannedPhrase => listing.Body?.Contains(bannedPhrase) ?? false);
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
