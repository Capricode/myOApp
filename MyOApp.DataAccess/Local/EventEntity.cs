using MyOApp.DataAccess.Local;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace MyOApp.DataAccess.Database
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

        public string Link { get; set; }

        public int? ResultsId { get; set; }

        public DateTime LastModificationDate { get; set; }

        //[OneToOne(inverseProperty: "EventId")]
        [OneToOne]
        public FavoritedEvent IsFavoritedEvent { get; set; }
    }
}
