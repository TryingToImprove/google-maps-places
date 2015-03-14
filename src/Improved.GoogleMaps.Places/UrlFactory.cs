using System;
using System.Text;

namespace Improved.GoogleMaps.Places
{
    internal class UrlFactory
    {
        private readonly string _apiKey;
        private readonly string _language;

        internal UrlFactory(string apiKey, string language)
        {
            _apiKey = apiKey;
            _language = language;
        }

        public Uri GenerateSearchUrl(string input)
        {
            var url = new StringBuilder();
            url.AppendFormat("https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}", _apiKey);
            url.AppendFormat("&input={0}", input);

            if (!string.IsNullOrWhiteSpace(_language))
            {
                url.AppendFormat("&language={0}", _language);
            }

            return new Uri(url.ToString());
        }

        public Uri GenerateDetailsUrl(string placeId)
        {
            var url = new StringBuilder();
            url.AppendFormat("https://maps.googleapis.com/maps/api/place/details/json?key={0}", _apiKey);
            url.AppendFormat("&placeId={0}", placeId);

            if (!string.IsNullOrWhiteSpace(_language))
            {
                url.AppendFormat("&language={0}", _language);
            }

            return new Uri(url.ToString());
        }
    }
}