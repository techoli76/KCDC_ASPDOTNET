using System.Collections.Generic;
using SpyStore.Models.Entities;
using SpyStore.Models.ViewModels;

namespace SpyStore.MVC.Models.ViewModels
{
    public class IndexPageViewModel
    {
        //This doesn't work because of the DisplayNameFor issue
        //public IEnumerable<ProductWithCategoryName> Products { get; set; }
        public IList<ProductWithCategoryName> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
