using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Product : IBaseEntity
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string Brand { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
