using SpyStore.MVC.Models.Validations;
using SpyStore.MVC.Models.ViewModels.Base;

namespace SpyStore.MVC.Models.ViewModels
{
    public class CartRecordViewModel : CartViewModelBase
    {

        [MustNotBeGreaterThan(nameof(UnitsInStock), "item_")]
        public int Quantity { get; set; }

    }

}
