using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bootcamp20ASPNET.Models;

namespace Bootcamp20ASPNET.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Supplier { get; set; }

        public ItemVM() { }

        public ItemVM(Item item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Price = item.Price;
            this.Stock = item.Stock;
        }
    }
}