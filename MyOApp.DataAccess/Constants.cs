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

        public struct EventsLocal
        {
            public const string DatabaseFilename = "EventsSqlLite.db3";

            public const SQLite.SQLiteOpenFlags Flags =
                // open the database in read/write mode
                SQLite.SQLiteOpenFlags.ReadWrite |
                // create the database if it doesn't exist
                SQLite.SQLiteOpenFlags.Create |
                // enable multi-threaded database access
                SQLite.SQLiteOpenFlags.SharedCache;

            public static string DatabasePath
            {
                get
                {
                    var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    return Path.Combine(basePath, DatabaseFilename);
                }
            }
        }   
    }
}
