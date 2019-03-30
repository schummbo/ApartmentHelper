using System;
using System.Collections.Generic;
using System.Linq;
using CraigslistHelper.Core.Entities;

namespace CraigslistHelper.Core.Helpers
{
    public class CraigslistUrlBuilder
    {
        private readonly Settings _settings;

        public CraigslistUrlBuilder(Settings settings)
        {
            _settings = settings;
        }

        public string BuildUrl()
        {
            List<string> qaParams = new List<string>();
            
            if (_settings.craigslistOptions.hasImage)
            {
                qaParams.Add("hasPic=1");
            }

            if (_settings.craigslistOptions.postedToday)
            {
                qaParams.Add("postedToday=1");
            }

            if (_settings.craigslistOptions.bundleDuplicates)
            {
                qaParams.Add("bundleDuplicates=1");
            }

            if (_settings.craigslistOptions.includeNearby)
            {
                qaParams.Add("searchNearby=1");
            }

            if (_settings.craigslistOptions.milesFromZip != null)
            {
                qaParams.Add($"search_distance={_settings.craigslistOptions.milesFromZip.miles}");
                qaParams.Add($"miles={_settings.craigslistOptions.milesFromZip.zip}");
            }

            if (_settings.craigslistOptions?.price?.min != null)
            {
                qaParams.Add($"min_price={_settings.craigslistOptions.price.min}");
            }

            if (_settings.craigslistOptions?.price?.max != null)
            {
                qaParams.Add($"max_price={_settings.craigslistOptions.price.max}");
            }

            if (_settings.craigslistOptions?.bedrooms?.min != null)
            {
                qaParams.Add($"min_bedrooms={_settings.craigslistOptions.bedrooms.min}");
            }

            if (_settings.craigslistOptions?.bedrooms?.max != null)
            {
                qaParams.Add($"max_bedrooms={_settings.craigslistOptions.bedrooms.max}");
            }

            if (_settings.craigslistOptions?.sqFt?.min != null)
            {
                qaParams.Add($"minSqft={_settings.craigslistOptions.sqFt.min}");
            }

            if (_settings.craigslistOptions?.sqFt?.max != null)
            {
                qaParams.Add($"maxSqft={_settings.craigslistOptions.sqFt.max}");
            }

            if (_settings.craigslistOptions.catsOk)
            {
                qaParams.Add("pets_cat=1");
            }

            if (_settings.craigslistOptions.dogsOk)
            {
                qaParams.Add("pets_dog=1");
            }

            if (_settings.craigslistOptions.furnished)
            {
                qaParams.Add("is_furnished=1");
            }

            if (_settings.craigslistOptions.noSmoking)
            {
                qaParams.Add("no_smoking=1");
            }

            if (_settings.craigslistOptions.wheelchairAccess)
            {
                qaParams.Add("wheelchaccess=1");
            }

            foreach (var type in _settings.craigslistOptions.housingTypes ?? Enumerable.Empty<HousingType>())
            {
                qaParams.Add($"housing_type={Convert.ToInt32(type)}");
            }

            var pars = string.Join("&", qaParams.ToArray());

            return $"https://{_settings.craigslistOptions.city}.craigslist.org/search/apa?{pars}";
        }
    }
}
