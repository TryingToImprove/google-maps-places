using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Raven.Client;
using Raven.Client.Embedded;

namespace Improved.GoogleMaps.Places
{
    public class PlacesService
    {
        private readonly UrlFactory _urlFactory;

        private readonly string _apiKey;
        private readonly string _language;
        private readonly ICachingProvider _cachingProvider;

        public PlacesService(string apiKey) : this(apiKey, null) { }

        public PlacesService(string apiKey, string language)
        {
            if (apiKey == null) throw new ArgumentNullException("apiKey");

            _apiKey = apiKey;
            _language = language;
            _urlFactory = new UrlFactory(_apiKey, _language);
            _cachingProvider = new StaticCachingProvider();
        }

        public Task<PlaceResult> SearchAsync(string input)
        {
            return SearchAsync(input, null);
        }

        public async Task<IEnumerable<DetailsResult>> AutocompleteAsync(string input, AutocompleteRequest request)
        {
            var results = _cachingProvider.GetAutocompleteResult(input, request);

            if (results != null)
                return results;

            var items = await SearchAsync(input, new AutocompleteRequest { Types = "address" })
                .ContinueWith(async x => await Task.WhenAll(x.Result.Predictions.Select(prediction =>
                {
                    if (prediction == null)
                        throw new ArgumentNullException("prediction");

                    return GetDetailsByPlaceId(prediction.PlaceId);
                })));

            results = (await items).Where(x => x != null);

            _cachingProvider.StoreAutocompleteResult(input, request, results);

            return results;
        }

        public Task<PlaceResult> SearchAsync(string input, AutocompleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException("input");

            var url = _urlFactory.GenerateSearchUrl(input, request);

            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

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
                webClient.Encoding = Encoding.UTF8;

                return webClient.DownloadStringTaskAsync(url)
                        .ContinueWith(x => JsonConvert.DeserializeObject<DetailsResult>(x.Result));
            }
        }
    }
}