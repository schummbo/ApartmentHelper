using System.Configuration;
using System.Linq;

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
            var url =
                $"http://bellingham.craigslist.org/search/apa?hasPic=1&postedToday=1&max_price={ConfigurationManager.AppSettings["MaxPrice"]}&pets_dog=1";
            HtmlDocument document = list.Load(url);

            Console.WriteLine("Got the listing. Enumerating.");

            // Check for nodes before the "nearby" row which appears if there aren't many results for the searched area
            var apartmentNodes =
                document.DocumentNode.SelectNodes(
                    "//ul[contains(@class, 'rows')]/h4/preceding-sibling::li//a[contains(@class, 'hdrlnk')]");

            // if no nodes, then that "nearby" row probably didn't show. Just get the apartments.
            if (!apartmentNodes.Any())
            {
                apartmentNodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'hdrlnk')]");
            }

            foreach (HtmlNode apartmentNode in apartmentNodes)
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

                    Title title = new TitleParser().Parse(listDocument.DocumentNode);

                    listing.Title = title.Text;
                    listing.Price = title.Price;

                    listing.Housing = new HousingHelper().GetHousingInfo(title);

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

            System.Diagnostics.Process.Start("explorer.exe", Directory.GetCurrentDirectory());
        }
    }
}
