

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Product
    {   [Key]
        public int ProductId{ get; set; }
        [Required]
        public  string Description { get; set; }
        public Decimal Price { get; set; }
        public  bool IsAvailable{ get; set; } //check validador de disponibilidad 
        public  DateTime PublishOn { get; set; }

        public override string ToString() //traduce al lenguaje 
        {
            return this.Description;
        }
    }
}
