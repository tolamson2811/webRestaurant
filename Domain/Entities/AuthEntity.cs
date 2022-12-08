using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AuthEntity : IBaseEntity
    {
        public int Id { get; set; }
        public Guid? UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string HashedKey { get; set; }
        public string SaltKey { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birth { get; set; }
        public string Avatar { get; set; }
    }
}
