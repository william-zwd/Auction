using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Auction.Hubs
{
    public class AuctionHub: Hub
    {
        public async Task SendBidding(string user, decimal price)
        {
            await Clients.All.SendAsync("ReceiveBidding", user, price);
        }
    }
}
