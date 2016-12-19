namespace CraigslistApartmentNotifier
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Helpers;
    using HtmlAgilityPack;
    using Output;
    using PageEntities;
    using Parsers;

    public class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            List<ApartmentListing> listings = new List<ApartmentListing>();

            Console.WriteLine("About to go to craigslist.");

            HtmlWeb list = new HtmlWeb();

            HtmlDocument document = list.Load("http://seattle.craigslist.org/search/apa?hasPic=1&postedToday=1&max_price=2000&pets_dog=1");

            Console.WriteLine("Got the listing. Enumerating.");

            foreach (HtmlNode apartmentNode in document.DocumentNode.SelectNodes("//a[contains(@class, 'hdrlnk')]"))
            {
                Console.WriteLine("Processing apartment...");

                ApartmentListing listing = new ApartmentListing
                {
                    Url = $"http://seattle.craigslist.org{apartmentNode.Attributes["href"].Value}"
                };

                HtmlWeb listingHw = new HtmlWeb();
                HtmlDocument listDocument = listingHw.Load(listing.Url);

                Title title = new TitleParser().Parse(listDocument.DocumentNode);

                listing.Title = title.Text;
                listing.Price = title.Price;
                listing.Housing = title.Housing;

                listing.Origin = new MapPointParser().Parse(listDocument.DocumentNode);

                listing.TravelInfo = new DistanceHelper().GetTravelInfo(listing);

                listing.ConfidenceLevel = new ConfidenceDecider().GetConfidenceLevel(listing);

                listings.Add(listing);
            }

            string html = new HtmlOutput().Execute(listings);

            File.WriteAllText("Apartments.html", html);
        }
    }
}
