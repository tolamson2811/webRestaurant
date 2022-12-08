using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class UserChangePasswordRequest
    {        
        public string Password { get; set; }        
        public string Token { get; set; }        
    }
}
