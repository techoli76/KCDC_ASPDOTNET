using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.Entities
{
    [Table("Customers", Schema = "Store")]
    public class Customer : EntityBase
    {
        [DataType(DataType.Text), MaxLength(50), Display(Name = DisplayNames.FullName)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress), MaxLength(50), Display(Name = DisplayNames.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password), MaxLength(50)]
        public string Password { get; set; }

        [InverseProperty(nameof(Order.Customer))]
        public virtual ICollection<Order> Orders { get; set; }
            = new HashSet<Order>();

        [InverseProperty(nameof(ShoppingCartRecord.Customer))]
        public virtual ICollection<ShoppingCartRecord> ShoppingCartRecords { get; set; }
            = new HashSet<ShoppingCartRecord>();
    }
}