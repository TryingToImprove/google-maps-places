using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Improved.GoogleMaps.Places
{
    public class PlacesService
    {
        private readonly string _apiKey;
        private readonly string _language;

        public PlacesService(string apiKey) : this(apiKey, null) { }

        public PlacesService(string apiKey, string language)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            _apiKey = apiKey;
            _language = language;
        }

        public Task<PlaceResult> SearchAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(input);

            var url = GenerateSearchUrl(input);

            using (var webClient = new WebClient())
            {
                return webClient.DownloadStringTaskAsync(url)
                        .ContinueWith(x => JsonConvert.DeserializeObject<PlaceResult>(x.Result));
            }
        }

        public Task<object> GetDetailsByPlaceId(string placeId)
        {
            
        }

        private Uri GenerateSearchUrl(string input)
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
    }

    public class PlaceResult
    {
        public IEnumerable<Prediction> Predictions { get; set; }

        public string Status { get; set; }
    }

    public class Prediction
    {
        public string Description { get; set; }

        public string Id { get; set; }

        [JsonProperty("matched_substrings")]
        public IEnumerable<MatchedSubstrings> MatchedSubstrings { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        public string Reference { get; set; }

        public IEnumerable<Term> Terms { get; set; }

        public IEnumerable<string> Types { get; set; }
    }

    public class MatchedSubstrings
    {
        public int Length { get; set; }

        public int Offset { get; set; }
    }

    public class Term
    {
        public int Offset { get; set; }

        public string Value { get; set; }
    }
}