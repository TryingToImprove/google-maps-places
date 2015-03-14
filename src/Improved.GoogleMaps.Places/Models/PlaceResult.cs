using System.Collections.Generic;

namespace Improved.GoogleMaps.Places
{
    public class PlaceResult
    {
        public IEnumerable<Prediction> Predictions { get; set; }

        public string Status { get; set; }
    }
}