using System;

namespace Repo.WebApi.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }     
        public int UserId { get; set; }     
        public string Role { get; set; }     
    }
}
