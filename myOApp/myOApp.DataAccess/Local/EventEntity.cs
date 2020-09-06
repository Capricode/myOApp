using SQLite;
using System;

namespace myOApp.DataAccess.Database
{
    public class EventEntity
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Source { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Map { get; set; }

        public string Club { get; set; }

        public string Region { get; set; }

        public string EventCenter { get; set; }

        public string Link { get; set; }

        public int? ResultsId { get; set; }

        public DateTime LastModificationDate { get; set; }

        public bool IsFavorite { get; set; }
    }
}
