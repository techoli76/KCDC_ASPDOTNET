using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.ViewModels
{
    public class OrderWithTotal : EntityBase
    {
        public int CustomerId { get; set; }

        [Display(Name = DisplayNames.DateOrdered)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Total)]
        public Decimal? OrderTotal { get; set; }

        [Display(Name = DisplayNames.DateShipped)]
        public DateTime ShipDate { get; set; }

        public IList<OrderDetailWithProductInfo> OrderDetails { get; set; }
    }

}