using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class WelcomeRequest
    {
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Link { get; set; }
    }
}
