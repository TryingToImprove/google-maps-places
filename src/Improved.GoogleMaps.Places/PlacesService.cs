using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Improved.GoogleMaps.Places
{
    public class PlacesService
    {
        private readonly UrlFactory _urlFactory;

        private readonly string _apiKey;
        private readonly string _language;

        public PlacesService(string apiKey) : this(apiKey, null) { }

        public PlacesService(string apiKey, string language)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            _apiKey = apiKey;
            _language = language;
            _urlFactory = new UrlFactory(_apiKey, _language);
        }

        public Task<PlaceResult> SearchAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException("input");

            var url = _urlFactory.GenerateSearchUrl(input);

            using (var webClient = new WebClient())
            {
                return webClient.DownloadStringTaskAsync(url)
                        .ContinueWith(x => JsonConvert.DeserializeObject<PlaceResult>(x.Result));
            }
        }

        public Task<DetailsResult> GetDetailsByPlaceId(string placeId)
        {
            if (string.IsNullOrWhiteSpace(placeId)) throw new ArgumentNullException("placeId");

            var url = _urlFactory.GenerateDetailsUrl(placeId);

            using (var webClient = new WebClient())
            {
                return webClient.DownloadStringTaskAsync(url)
                        .ContinueWith(x => JsonConvert.DeserializeObject<DetailsResult>(x.Result));
            }
        }
    }
}