using BusinessLayer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IUserBusiness userBusiness;
        public TicketController(IBus bus, IUserBusiness userBusiness)
        {
            _bus = bus;
            this.userBusiness = userBusiness;
        }
        [HttpPost("ticket")]
        public async Task<IActionResult> CreateTicketForPassword(string email)
        {
            var token = userBusiness.ForgetPassword(email);
            if(token != null) 
            {
                var ticket=userBusiness.CreateTicketForPassword(email,token);
                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                return Ok(new {status=true,message="mail sent successfully" });
            }
            else
            {
                return BadRequest(new { status = false, message = "mail not sent" });
            }
        }
    }
}
