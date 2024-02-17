using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Persistence
{
    public class Orders
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public String Name { get; set; }

        public String Address { get; set; }
        
        public string PhoneNumber { get; set; }
        public string OrderedItems { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OutDate { get; set; }

        public int FullPrice { get; set;  }

    }
}
