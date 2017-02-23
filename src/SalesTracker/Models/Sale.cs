using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTracker.Models
{   [Table("Sales")]
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Sale(int itemId, int saleId = 0)
        {
            ItemId = itemId;
            SaleId = saleId;
        }

        public Sale() { }

    }
}
