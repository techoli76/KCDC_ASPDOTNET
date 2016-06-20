using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.Entities
{
    [Table("OrderDetails", Schema = "Store")]
    public class OrderDetail : EntityBase
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required, DataType(DataType.Currency), Display(Name = DisplayNames.UnitCost)]
        public decimal UnitCost { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Total)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? LineItemTotal { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(SpyStore.Models.Entities.Order.OrderDetails))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(Entities.Product.OrderDetails))]
        public virtual Product Product { get; set; }

    }
}