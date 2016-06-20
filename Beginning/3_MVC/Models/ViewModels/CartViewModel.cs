using SpyStore.Models.Entities;
using System.Collections.Generic;

namespace SpyStore.MVC.Models.ViewModels
{
    public class CartViewModel
    {
        public Customer Customer { get; set; }
        public IList<CartRecordViewModel> CartRecords { get; set; } 
    }
}