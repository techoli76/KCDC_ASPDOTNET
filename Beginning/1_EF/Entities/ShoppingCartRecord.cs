using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.Entities
{
    [Table("ShoppingCartRecords", Schema = "Store")]
    public class ShoppingCartRecord : EntityBase
    {

        [DataType(DataType.Date)]
        public DateTime? DateCreated { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        [InverseProperty(nameof(SpyStore.Models.Entities.Customer.ShoppingCartRecords))]
        public virtual Customer Customer { get; set; }

        [DefaultValue(1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [NotMapped, DataType(DataType.Currency)]
        public decimal LineItemTotal { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(SpyStore.Models.Entities.Product.ShoppingCartRecords))]
        public virtual Product Product { get; set; }
    }

}
