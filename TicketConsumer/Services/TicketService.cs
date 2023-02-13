using MassTransit;
using System.Net.Sockets;
using System.Threading.Tasks;
using ModelLayer;

namespace TicketConsumer.Services
{
    public class TicketService:IConsumer<UserTicket>
    {
        public async Task Consume(ConsumeContext<UserTicket> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
