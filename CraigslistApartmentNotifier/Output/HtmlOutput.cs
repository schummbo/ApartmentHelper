namespace CraigslistApartmentNotifier.Output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entities;

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
                                                                        .OrderBy(x => x.TravelInfo.TravelTimeSeconds))
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
            sb.AppendLine($"<td>Housing</td>");
            sb.AppendLine($"<td>Travel Time (mins)</td>");
            sb.AppendLine($"<td>Busses</td>");
            sb.AppendLine($"<td>Locality</td>");
            sb.AppendLine("</tr>");
        }

        private void WriteRow(ApartmentListing listing, StringBuilder sb)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine($"<td><a href='{listing.Url}'>{listing.Title}</a></td>");
            sb.AppendLine($"<td>{listing.Price.ToString("C")}</td>");
            sb.AppendLine($"<td>{listing.Housing}</td>");

            if (listing.TravelInfo.TravelTimeSeconds.HasValue)
            {
                TimeSpan t = TimeSpan.FromSeconds(listing.TravelInfo.TravelTimeSeconds.Value);

                sb.AppendLine($"<td>{t.Hours:D2}h:{t.Minutes:D2}m</td>");
            }
            else
            {
                sb.AppendLine($"<td>?</td>");
            }

            sb.AppendLine($"<td>{listing.TravelInfo.NumberOfBuses}</td>");
            sb.AppendLine($"<td>{listing.CityName}</td>");

            sb.AppendLine("</tr>");
        }
    }
}
