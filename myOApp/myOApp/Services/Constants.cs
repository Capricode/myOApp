using System;
using System.Collections.Generic;
using System.Text;

namespace myOApp.Services
{
    public class Constants
    {
        public struct Favorites
        {
            public static readonly string FavoritesToggledMessage = "FavoritesToggled";
        }

        public struct Synchronization
        {
            public static readonly string NewDataAvailableMessage = "NewDataAvailable";
        }
    }
}
