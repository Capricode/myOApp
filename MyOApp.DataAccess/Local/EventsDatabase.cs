using MyOApp.DataAccess.Local;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyOApp.DataAccess.Database
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
                        await Database.CreateTablesAsync(CreateFlags.None, typeof(EventEntity), typeof(FavoritedEvent)).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                    }

                    initialized = true;
                }
            }
        }


        public Task<List<EventEntity>> GetAllEvents(DateTime? lastSynchronizationDate = null)
        {
            return Database.Table<EventEntity>().ToListAsync();
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
            // return Database.GetAllWithChildrenAsync<EventEntity>(x => x.IsFavoritedEvent != null);
            // return Database.Table<EventEntity>().Where(x => x.IsFavoritedEvent != null).ToListAsync();
            return Database.Table<EventEntity>().Where(x => x.IsFavorite).ToListAsync();
        }

        public Task<EventEntity> GetEvent(string id)
        {
            return Database.GetAsync<EventEntity>(id);
        }

        public async Task<EventEntity> ToggleFavorite(string id)
        {
            var singleEvent = await Database.GetAsync<EventEntity>(id);

            //var favoritedEvent = await Database.Table<FavoritedEvent>().Where(x => x.EventId == id).FirstOrDefaultAsync();
            //if (favoritedEvent == null)
            //{
            //    favoritedEvent = new FavoritedEvent { Event = singleEvent };
            //    await Database.InsertAsync(favoritedEvent);
            //    singleEvent.IsFavoritedEvent = favoritedEvent;
            //    await Database.UpdateAsync(singleEvent);
            //}
            //else
            //{
            //    singleEvent.IsFavoritedEvent = null;
            //    await Database.UpdateAsync(singleEvent);

            //    await Database.DeleteAsync(favoritedEvent);
            //}
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

        //public async Task<List<EventEntity>> GetAllEvents()
        //{
        //    var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //    return await AttemptAndRetry(() => databaseConnection.Table<EventEntity>().ToListAsync()).ConfigureAwait(false);
        //}

        //public async Task<EventEntity> GetEvent(string id)
        //{
        //    var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //    return await AttemptAndRetry(() => databaseConnection.GetAsync<EventEntity>(id)).ConfigureAwait(false);
        //}

        //public async Task<int> UpdateEvents(IEnumerable<EventEntity> events)
        //{
        //    var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //    // or InsertOrReplace?
        //    // return await AttemptAndRetry(() => databaseConnection.UpdateAllAsync(events)).ConfigureAwait(false);
        //    return await databaseConnection.UpdateAllAsync(events);
        //}

        //public async Task<int> SaveEvents(IEnumerable<EventEntity> events)
        //{
        //    try
        //    {
        //        var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //        //var repositoryDatabaseModels = events.Select(x => (RepositoryDatabaseModel)x);

        //        return await AttemptAndRetry(() => databaseConnection.InsertAllAsync(events)).ConfigureAwait(false);
        //    }
        //    catch (SQLiteException e) when (e.Result is SQLite3.Result.Constraint)
        //    {
        //        int count = 0;

        //        foreach (var singleEvent in events)
        //        {
        //            count += await SaveEvent(singleEvent).ConfigureAwait(false);
        //        }

        //        return count;
        //    }
        //}

        //public async Task<int> SaveEvent(EventEntity singleEvent)
        //{
        //    var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //    return await AttemptAndRetry(() => databaseConnection.InsertOrReplaceAsync(singleEvent)).ConfigureAwait(false);
        //}

        //public async Task<int> DeleteAll()
        //{
        //    var databaseConnection = await GetDatabaseConnection<EventEntity>().ConfigureAwait(false);

        //    return await AttemptAndRetry(() => databaseConnection.DeleteAllAsync<EventEntity>()).ConfigureAwait(false);
        //}
    }
}
