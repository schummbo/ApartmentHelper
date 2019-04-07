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



            WriteTableHeader(sb);

            foreach (ApartmentListing apartmentListing in apartments.OrderByDescending(x => x.Score))
            {
                WriteRow(apartmentListing, sb);
            }

            sb.AppendLine("</table>");


            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        private static void WriteTableHeader(StringBuilder sb)
        {
            sb.AppendLine("<table border='1'>");

            sb.AppendLine("<tr>");
            sb.AppendLine($"<td>Title</td>");
            sb.AppendLine($"<td>Price</td>");
            sb.AppendLine($"<td>Bedrooms</td>");
            sb.AppendLine("<td>SqFt</td>");
            sb.AppendLine($"<td>Score</td>");
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

            sb.AppendLine($"<td>{listing.Score}</td>");
            sb.AppendLine($"<td>{listing.CityName}</td>");

            sb.AppendLine("</tr>");
        }
    }
}
