using System;
using System.IO;

namespace MyOApp.DataAccess
{
    public struct Constants
    {
        public struct EventsApi
        {
            public static readonly string BaseEndpointUrl = "https://o-api.azurewebsites.net/api/extendedEvents";

            public static readonly string LastModificationQueryKey = "lastmodification={0}";
            public static readonly string YearQueryKey = "year={0}";
        }

    }
}
