using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;

namespace Improved.GoogleMaps.Places
{

    public class RavenCachingProvider : ICachingProvider
    {
        private readonly IDocumentStore _documentStore;

        public RavenCachingProvider(string cachingLocation)
        {
            _documentStore = new EmbeddableDocumentStore()
            {
                RunInMemory = true
            };
            _documentStore.Initialize();
        }

        public void StoreAutocompleteResult(string input, AutocompleteRequest request, IEnumerable<DetailsResult> results)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(new CachingDetailsResult
                {
                    Input = input,
                    Request = request,
                    Results = results
                });

                session.SaveChanges();
            }
        }

        public IEnumerable<DetailsResult> GetAutocompleteResult(string input, AutocompleteRequest request)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<CachingDetailsResult>()
                    .Where(x => x.Input.Equals(input, StringComparison.InvariantCultureIgnoreCase)
                                && x.Request.Components.Equals(request.Components, StringComparison.InvariantCultureIgnoreCase)
                                && x.Request.Types.Equals(request.Types, StringComparison.InvariantCultureIgnoreCase))
                    .SelectMany(x => x.Results);
            }
        }
    }

    internal class CachingDetailsResult
    {
        public string Input { get; set; }

        public AutocompleteRequest Request { get; set; }

        public IEnumerable<DetailsResult> Results { get; set; }
    }
}
