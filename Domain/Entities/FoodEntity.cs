using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FoodEntity : IBaseEntity
    {
        /// <summary>
        /// id của món ăn
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// tên món ăn
        /// </summary>
        public string FoodName { get; set; }
        /// <summary>
        /// giá món ăn
        /// </summary>
        public decimal FoodPrice { get; set; }
        /// <summary>
        /// link ảnh
        /// </summary>
        public string ImageLink { get; set; }


    }
}
