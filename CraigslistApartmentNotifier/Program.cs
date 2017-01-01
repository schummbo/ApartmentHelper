namespace CraigslistApartmentNotifier
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Entities;
    using Helpers;
    using HtmlAgilityPack;
    using Output;
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

            HtmlDocument document = list.Load("http://seattle.craigslist.org/search/apa?hasPic=1&postedToday=1&max_price=1800&pets_dog=1");

            Console.WriteLine("Got the listing. Enumerating.");

            foreach (HtmlNode apartmentNode in document.DocumentNode.SelectNodes("//a[contains(@class, 'hdrlnk')]"))
            {
                ApartmentListing listing = new ApartmentListing
                {
                    Url = $"http://seattle.craigslist.org{apartmentNode.Attributes["href"].Value}"
                };

                try
                {
                    Console.WriteLine("Processing apartment...");
                    
                    HtmlWeb listingHw = new HtmlWeb();
                    HtmlDocument listDocument = listingHw.Load(listing.Url);

                    Title title = new TitleParser().Parse(listDocument.DocumentNode);

                    listing.Title = title.Text;
                    listing.Price = title.Price;
                    listing.Housing = title.Housing;

                    listing.Origin = new MapPointParser().Parse(listDocument.DocumentNode);

                    listing.TravelInfo = new RouteHelper().GetTravelInfo(listing);

                    listing.CityName = new CityHelper().GetCityName(listing);

                    listing.ConfidenceLevel = new ConfidenceDecider().GetConfidenceLevel(listing);

                    listings.Add(listing);
                }
                catch (Exception ex)
                {
                    listing.ConfidenceLevel = ConfidenceLevel.Failed;
                }
            }

            string html = new HtmlOutput().Execute(listings);

            File.WriteAllText($"Apartments-{DateTime.Now.ToString("yy_MM_dd_hh_mm")}.html", html);
        }
    }
}
