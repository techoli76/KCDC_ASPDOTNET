using SpyStore.MVC.Models.Validations;

using SpyStore.MVC.Models.ViewModels.Base;

namespace SpyStore.MVC.Models.ViewModels
{
    public class AddToCartViewModel :CartViewModelBase
    {
        [MustNotBeGreaterThan(nameof(UnitsInStock))]
        [MustBeGreaterThanZero]
        public int Quantity { get; set; }

    }
}