using System.Collections.Generic;
using Newtonsoft.Json;

namespace Improved.GoogleMaps.Places
{
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