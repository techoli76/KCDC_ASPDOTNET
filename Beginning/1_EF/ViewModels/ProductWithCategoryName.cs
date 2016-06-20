using System.ComponentModel.DataAnnotations;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.ViewModels
{
    public class ProductWithCategoryName : EntityBase
    {
        public string Description { get; set; }

        [Display(Name = DisplayNames.ModelName)]
        public string ModelName { get; set; }

        [Display(Name = DisplayNames.ModelNumber)]
        public string ModelNumber { get; set; }
        public string ProductImage { get; set; }
        public string ProductImageLarge { get; set; }
        public string ProductImageThumb { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.UnitCost)]
        public decimal UnitCost { get; set; }

        [DataType(DataType.Currency), Display(Name = DisplayNames.Price)]
        public decimal CurrentPrice { get; set; }

        [Display(Name = DisplayNames.Stock)]
        public int UnitsInStock { get; set; }

        [Display(Name = DisplayNames.Category)]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = DisplayNames.Featured)]
        public bool IsFeatured { get; set; }
    }

}