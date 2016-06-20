using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.Entities
{
    [Table("Orders", Schema = "Store")]
    public class Order : EntityBase
    {
        public int CustomerId { get; set; }

        //[DataType(DataType.Currency), Display(Name = DisplayNames.Total)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public decimal? OrderTotal { get; set; }

        //[DataType(DataType.Currency)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public decimal? OrderTotalComputed { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = DisplayNames.DateOrdered)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = DisplayNames.DateShipped)]
        public DateTime ShipDate { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customer Customer { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
            = new HashSet<OrderDetail>();
    }
}