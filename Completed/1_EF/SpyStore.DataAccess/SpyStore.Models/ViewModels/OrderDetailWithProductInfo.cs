using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.ViewModels
{
    public class OrderDetailWithProductInfo : EntityBase
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Display(Name = DisplayNames.UnitCost)]
        public decimal UnitCost { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Total)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? LineItemTotal { get; set; }

        public string Description { get; set; }

        [Display(Name = DisplayNames.ModelName)]
        public string ModelName { get; set; }

        [Display(Name = DisplayNames.ModelNumber)]
        public string ModelNumber { get; set; }

        [Display(Name = DisplayNames.Category)]
        public string CategoryName { get; set; }

        public string ProductImage { get; set; }
        public string ProductImageLarge { get; set; }
        public string ProductImageThumb { get; set; }
    }
}