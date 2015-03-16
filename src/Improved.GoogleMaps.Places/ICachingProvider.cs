using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improved.GoogleMaps.Places
{
    public interface ICachingProvider
    {
        void StoreAutocompleteResult(string input, AutocompleteRequest request, IEnumerable<DetailsResult> results);

        IEnumerable<DetailsResult> GetAutocompleteResult(string input, AutocompleteRequest request);
    }
}
