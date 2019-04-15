using System;
using System.Text.RegularExpressions;
using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Processors
{
    public class HousingProcessor : BaseProcessor
    {
        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            var housingInfo = new HousingInfo();

            var titleNode = node.SelectSingleNode("//h2[@class='postingtitle']");
            var housing = titleNode.SelectSingleNode("//span[@class='housing']")?.InnerText.Replace("/", "");

            if (!string.IsNullOrWhiteSpace(housing))
            {
                var regex = new Regex("(([0-9]*)br)?\\s*-*\\s*(([0-9]*)ft2)?");

                var match = regex.Match(housing.Trim());

                if (int.TryParse(match.Groups[2].Value, out int bedrooms))
                {
                    housingInfo.Bedrooms = bedrooms;
                }

                if (int.TryParse(match.Groups[4].Value, out int sqft))
                {
                    housingInfo.SqFt = sqft;
                }

            }

            var attributeNodes = node.SelectNodes("//p[contains(@class,'attrgroup')]/span");

            foreach (var attributeNode in attributeNodes)
            {
                var sanitizedText = attributeNode.InnerText.Replace("/", "")
                                                            .Replace("-", "")
                                                            .Replace(" ", "");

                if (Enum.TryParse(sanitizedText, true, out HousingType result))
                {
                    housingInfo.HousingType = result;
                    break;
                }
            }

            listing.Housing = housingInfo;
        }

        public HousingProcessor(Settings settings) : base(settings)
        {
        }
    }
}
