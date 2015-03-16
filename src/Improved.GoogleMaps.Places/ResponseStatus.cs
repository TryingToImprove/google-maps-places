using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improved.GoogleMaps.Places
{
    public abstract class ResponseStatus
    {
        private struct Shared
        {
            public const string InvalidRequest = "INVALID_REQUEST";

            public const string Ok = "OK";

            public const string RequestDenied = "REQUEST_DENIED";

            public const string OverQueryLimit = "OVER_QUERY_LIMIT";

            public const string ZeroResults = "ZERO_RESULTS";

            public const string NotFound = "NOT_FOUND";

            public const string UnknownError = "UNKNOWN_ERROR";
        }

        public struct Places
        {
            public const string UnknownError = Shared.UnknownError;

            public const string InvalidRequest = Shared.InvalidRequest;

            public const string Ok = Shared.Ok;

            public const string ZeroResults = Shared.ZeroResults;

            public const string OverQueryLimit = Shared.OverQueryLimit;

            public const string NotFound = Shared.NotFound;

            public const string RequestDenied = Shared.RequestDenied;
        }

        public struct Autocomplete
        {
            public const string Ok = Shared.Ok;

            public const string ZeroResults = Shared.ZeroResults;

            public const string OverQueryLimit = Shared.OverQueryLimit;

            public const string RequestDenied = Shared.RequestDenied;

            public const string InvalidRequest = Shared.InvalidRequest;
        }
    }
}
