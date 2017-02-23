using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTracker.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName ="Money")]
        public decimal Price { get;  set; }

        public int Quantity { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public Item(string name, string description, decimal price, int quantity, int itemId = 0)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            ItemId = itemId;
        }

        public Item() { }
    }
}
