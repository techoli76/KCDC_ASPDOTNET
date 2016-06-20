using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace SpyStore.MVC.Models.Validations
{
    public class MustBeGreaterThanZeroAttribute : ValidationAttribute, IClientModelValidator
    {
        private string _propertyDisplayName;
        public MustBeGreaterThanZeroAttribute()
            : this("{0} must be greater than 0")
        {
        }
        public MustBeGreaterThanZeroAttribute(
            string errorMessage)
            : base(errorMessage)
        {
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }

        public override bool IsValid(object value)
        {
            int result;
            try
            {
                int.TryParse(value.ToString(),out result);
            }
            catch (Exception)
            {
                return false;
            }
            return result != 0;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            _propertyDisplayName = context.ModelMetadata.DisplayName ?? context.ModelMetadata.PropertyName;
            string errorMessage = FormatErrorMessage(_propertyDisplayName);
            context.Attributes.Add("data-val-greaterthanzero", errorMessage);
        }
    }
}