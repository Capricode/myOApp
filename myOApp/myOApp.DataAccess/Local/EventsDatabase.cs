using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace myOApp.DataAccess.Database
{
    public class EventsDatabase : IEventsDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.EventsLocal.DatabasePath, Constants.EventsLocal.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public EventsDatabase()
        {
            InitializeAsync().ConfigureAwait(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any())
                {
                    Database.Tracer = new Action<string>(q => Debug.WriteLine(q));
                    Database.Trace = true;

                    try
                    {
                        await Database.CreateTablesAsync(CreateFlags.None, typeof(EventEntity)).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                    initialized = true;
                }
            }
        }

        public Task<List<EventEntity>> GetAllEvents(Expression<Func<EventEntity, bool>> predicate = null, bool shouldSortDescending = false, DateTime? lastSynchronizationDate = null)
        {
            if (predicate == null)
            {
                // if no filter specified we just return all
                predicate = DefaultWherePredicate;
            }

            if (shouldSortDescending)
            {
                return Database.Table<EventEntity>().Where(predicate).OrderByDescending(x => x.Date).ToListAsync();
            }

            return Database.Table<EventEntity>().Where(predicate).OrderBy(x => x.Date).ToListAsync();
            //return Database.Table<EventEntity>().Where(x => x.LastModificationDate > lastSynchronizationDate).ToListAsync();
        }

        public Task<List<EventEntity>> GetNearbyEvents(IEnumerable<string> userRegions)
        {
            var now = DateTime.Now;
            return Database.Table<EventEntity>().Where(x => userRegions.Contains(x.Region)).Where(x => x.Date >= now).OrderBy(x => x.Date).ToListAsync();
        }

        public Task<List<EventEntity>> GetFavoritedEvents()
        {
            // the time span should be somehow limited
            return Database.Table<EventEntity>().Where(x => x.IsFavorite).ToListAsync();
        }

        public Task<EventEntity> GetEvent(string id)
        {
            return Database.GetAsync<EventEntity>(id);
        }

        public async Task<EventEntity> ToggleFavorite(string id)
        {
            var singleEvent = await Database.GetAsync<EventEntity>(id);
            singleEvent.IsFavorite = !singleEvent.IsFavorite;

            await Database.UpdateAsync(singleEvent);

            return singleEvent;
        }

        public Task<int> SaveEvent(EventEntity singleEvent)
        {
            return Database.InsertOrReplaceAsync(singleEvent);
        }

        public Task<int[]> SaveEvents(IEnumerable<EventEntity> events)
        {
            return Task.WhenAll(events.Select(x => Database.InsertOrReplaceAsync(x)));
        }

        public Task<int> UpdateEvents(IEnumerable<EventEntity> events)
        {
            return Database.UpdateAllAsync(events);
        }

        public Task<int> DeleteAll()
        {
            return Database.DeleteAllAsync<EventEntity>();
        }

        private Expression<Func<EventEntity, bool>> DefaultWherePredicate
        {
            get { return x => true; }
        }
    }
}
