using Domain.Interfaces;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BookFoodEntity : IBaseEntity
    {
        /// <summary>
        /// id của đặt món
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// id của người dùng
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// id của món ăn
        /// </summary>
        public int FoodId { get; set; }
        /// <summary>
        /// số lượng đặt
        /// </summary>
        public int Quantity { get; set; }
        public long Total { get; set; }
    }
}
