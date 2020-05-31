using myOApp.DataAccess.Database;
using SQLiteNetExtensions.Attributes;

namespace myOApp.DataAccess.Local
{
    public class FavoritedEvent
    {
        [ForeignKey(typeof(EventEntity))]
        public string EventId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public EventEntity Event { get; set; }
    }
}
