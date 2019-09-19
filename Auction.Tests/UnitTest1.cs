using Auction.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class AuctionBiddingTests
    {
        [Test]
        public async Task SignalR_BiddingTest()
        {
            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();

            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

            AuctionHub hub = new AuctionHub()
            {
                Clients = mockClients.Object
            };

            await hub.SendBidding("User1", 10.0M);

            // assert
            mockClients.Verify(clients => clients.All, Times.Once);
            mockClientProxy.Verify(clientProxy => clientProxy.SendAsync("ReceiveBidding", default(CancellationToken)), Times.Once);
        }
    }
}