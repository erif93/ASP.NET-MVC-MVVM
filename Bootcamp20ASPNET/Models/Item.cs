using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bootcamp20ASPNET.ViewModels;

namespace Bootcamp20ASPNET.Models
{
    public class Item:BaseModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public Supplier Suppliers { get; set; }
        public Item(ItemVM itemVM)
        {
            this.Id = itemVM.Id;
            this.Name = itemVM.Name;
            this.Price = itemVM.Price;
            this.Stock = itemVM.Stock;
            this.UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }
        public Item() { }


        public void Update(ItemVM itemVM)
        {
            this.Name = itemVM.Name;
            this.Price = itemVM.Price;
            this.Stock = itemVM.Stock;
            this.UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
    
}