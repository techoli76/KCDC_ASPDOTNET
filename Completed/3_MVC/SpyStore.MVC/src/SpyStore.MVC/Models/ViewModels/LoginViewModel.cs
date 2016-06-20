using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SpyStore.Models.Entities;
using SpyStore.MVC.Models.Validations;

namespace SpyStore.MVC.Models.ViewModels
{
    public class LoginViewModel
    {
        //[Range(1, int.MaxValue, ErrorMessage = "Please select a valid user")]
        [MustBeGreaterThanZero(ErrorMessage = "Please select a valid user")]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [DataType(DataType.Text), Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50), EmailAddress, Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
