using CraigslistHelper.Core.Entities;
using HtmlAgilityPack;

namespace CraigslistHelper.Core.Processors
{
    public abstract class BaseProcessor
    {
        protected readonly Settings Settings;

        protected BaseProcessor(Settings settings)
        {
            Settings = settings;
        }

        public abstract void Parse(HtmlNode node, ApartmentListing listing);
    }
}
