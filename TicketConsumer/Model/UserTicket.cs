﻿using System;

namespace TicketConsumer.Model
{
    public class UserTicket
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string token { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
