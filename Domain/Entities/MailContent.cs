using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MailContent : IBaseEntity
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        public string Content { get; set; }
    }
}
