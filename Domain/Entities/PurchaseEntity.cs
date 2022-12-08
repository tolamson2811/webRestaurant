using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PurchaseEntity : IBaseEntity
    {
        public int Id { get; set; }
 
        public int UserId { get; set; }

        public int Total { get; set; }


        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        
        public string Description { get; set; }
        public string PurchaseDate { get; set; }
    }
}
