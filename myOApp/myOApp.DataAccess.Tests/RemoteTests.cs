using myOApp.DataAccess.Client;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace myOApp.DataAccess.Tests
{
    [TestFixture]
    public class RemoteTests
    {
        private IEventsClient eventsClient;

        [SetUp]
        public void SetUp()
        {
            eventsClient = new EventsClient();
        }

        [Test]
        public async Task ShouldReturnEventsFromRemoteAsync()
        {
            var events = (await this.eventsClient.GetEvents()).ToList();

            Assert.IsNotNull(events);
            Assert.AreEqual(events.Any(), true);
        }
    }
}
