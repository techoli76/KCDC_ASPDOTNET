using System;
using System.ComponentModel.DataAnnotations;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.ViewModels
{
    public class CartRecordWithProductInfo : EntityBase
    {

        [DataType(DataType.Date), Display(Name = DisplayNames.DateCreated)]
        public DateTime? DateCreated { get; set; }

        public int? CustomerId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }

        [Display(Name = DisplayNames.ModelName)]
        public string ModelName { get; set; }

        [Display(Name = DisplayNames.ModelNumber)]
        public string ModelNumber { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Price)]
        public decimal CurrentPrice { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Total)]
        public decimal LineItemTotal { get; set; }

        [Display(Name = DisplayNames.Stock)]
        public int UnitsInStock { get; set; }

        [Display(Name = DisplayNames.Category)]
        public string CategoryName { get; set; }
        public string ProductImage { get; set; }
        public string ProductImageLarge { get; set; }
        public string ProductImageThumb { get; set; }
    }
}
