using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Output
{
    public class HtmlOutput
    {
        public string Execute(List<ApartmentListing> apartments)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html><body>");

            foreach (string confidenceLevelName in Enum.GetNames(typeof(ConfidenceLevel)))
            {
                ConfidenceLevel confidenceLevel = (ConfidenceLevel)Enum.Parse(typeof(ConfidenceLevel), confidenceLevelName);

                WriteTableHeader(sb, confidenceLevel);

                foreach (ApartmentListing apartmentListing in apartments.Where(x => x.ConfidenceLevel == confidenceLevel)
                                                                        .OrderBy(x => x.Housing.Bedrooms)
                                                                        .ThenBy(x => x.Housing.SqFt)
                                                                        .ThenBy(x => x.Price))
                {
                    WriteRow(apartmentListing, sb);
                }

                sb.AppendLine("</table>");
            }

            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        private static void WriteTableHeader(StringBuilder sb, ConfidenceLevel confidenceLevel)
        {
            sb.AppendLine($"<h3>{confidenceLevel} Confidence</h3>");
            sb.AppendLine("<table border='1'>");

            sb.AppendLine("<tr>");
            sb.AppendLine($"<td>Title</td>");
            sb.AppendLine($"<td>Price</td>");
            sb.AppendLine($"<td>Bedrooms</td>");
            sb.AppendLine("<td>SqFt</td>");
            sb.AppendLine($"<td>Buses</td>");
            sb.AppendLine($"<td>Walking</td>");
            sb.AppendLine($"<td>Locality</td>");
            sb.AppendLine("</tr>");
        }

        private void WriteRow(ApartmentListing listing, StringBuilder sb)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine($"<td><a href='{listing.Url}'>{listing.Title}</a></td>");
            sb.AppendLine($"<td>{listing.Price?.ToString("C")}</td>");
            sb.AppendLine($"<td>{listing.Housing.Bedrooms}</td>");
            sb.AppendLine($"<td>{listing.Housing.SqFt}</td>");

            sb.AppendLine($"<td>{listing.TravelInfo?.NumberOfBuses}</td>");

            var walkingTimes = listing.TravelInfo?.WalkingTimes?.Select(x => TimeSpan.FromSeconds(x).Minutes.ToString());

            sb.AppendLine($"<td>{string.Join(" &#47; ", walkingTimes ?? new string[0])}</td>");
            sb.AppendLine($"<td>{listing.CityName}</td>");

            sb.AppendLine("</tr>");
        }
    }
}
