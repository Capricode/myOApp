using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

{
    public class EventsClient : IEventsClient
    {
        public async Task<IEnumerable<Event>> GetEvents(DateTime? lastModificationDate = null, int? year = null)
        {
            var eventsApiUrl = Constants.EventsApi.BaseEndpointUrl;

            //var date = lastModificationDate ?? null;
            //eventsApiUrl += $"?{Constants.EventsApi.LastModificationQueryKey}={date}";

            if (year != null)
            {
                eventsApiUrl += $"&{Constants.EventsApi.YearQueryKey}={year}";
            }

                var client = new HttpClient();
                var response = await client.GetStringAsync(eventsApiUrl);

                var result = JsonConvert.DeserializeObject<List<Event>>(JObject.Parse(response)["events"].ToString());
                return result;
            }
    }
}
