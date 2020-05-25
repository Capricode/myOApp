using myOApp.DataAccess.Database;
using System;
using System.Linq.Expressions;

namespace myOApp.Models
{
    public class EventsFilter
    {
        public Expression<Func<EventEntity, bool>> Predicate { get; set; }

        public bool ShouldSortDescending { get; set; }
    }
}
