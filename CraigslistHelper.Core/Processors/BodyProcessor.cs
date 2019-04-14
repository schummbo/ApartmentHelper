using System;
using System.Collections.Generic;
using System.Text;
using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Processors
{
    public class BodyProcessor : BaseProcessor
    {
        public BodyProcessor(Settings settings) : base(settings)
        {
        }

        public override void Parse(HtmlNode node, ApartmentListing listing)
        {
            listing.Body = node.SelectSingleNode("//section[@id='postingbody']").InnerText;
        }
    }
}
