﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improved.GoogleMaps.Places
{
    internal class StaticCachingProvider : ICachingProvider
    {
        private static readonly ConcurrentDictionary<string, IEnumerable<DetailsResult>> _cache = new ConcurrentDictionary<string, IEnumerable<DetailsResult>>();

        public void StoreAutocompleteResult(string input, AutocompleteRequest request, IEnumerable<DetailsResult> results)
        {
            _cache.TryAdd(input, results);
        }

        public IEnumerable<DetailsResult> GetAutocompleteResult(string input, AutocompleteRequest request)
        {
            IEnumerable<DetailsResult> results;
            _cache.TryGetValue(input, out results);

            return results;
        }
    }
}
