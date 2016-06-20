using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SpyStore.MVC.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class MustNotBeGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        readonly string _otherPropertyName;
        string _propertyDisplayName;
        string _otherPropertyDisplayName;
        string _prefix;
 
        public MustNotBeGreaterThanAttribute(string otherPropertyName, string prefix = "")
            : this(otherPropertyName,"{0} must not be greater than {1}", prefix)
        {
        }
        public MustNotBeGreaterThanAttribute(
            string otherPropertyName, string errorMessage, string prefix)
            : base(errorMessage)
        {
            _otherPropertyName = otherPropertyName;
            _otherPropertyDisplayName = otherPropertyName;
            _prefix = prefix;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherPropertyDisplayName);
        }


        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var validationResult = ValidationResult.Success;
            try
            {
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);
                if (otherPropertyInfo.PropertyType == typeof(int))
                {
                    int toValidate;
                    try
                    {
                        int.TryParse(value.ToString(),out toValidate);
                    }
                    catch (Exception)
                    {
                        return new ValidationResult(ErrorMessageString);
                    }

                    var referenceProperty = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
                    if (toValidate > referenceProperty)
                    {
                        validationResult = new ValidationResult(ErrorMessageString);
                    }
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type number");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return validationResult;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            _propertyDisplayName = context.ModelMetadata.GetDisplayName();

            IEnumerable<CustomAttributeData> attributes = context.ModelMetadata.ContainerType.GetProperty(_otherPropertyName).CustomAttributes;
            foreach (CustomAttributeData itm in attributes)
            {
                if (itm.AttributeType.Name != "DisplayAttribute")
                {
                    continue;
                }
                _otherPropertyDisplayName = ((CustomAttributeNamedArgument)itm.NamedArguments[0]).TypedValue.Value.ToString();
                break;
            }
            string errorMessage = FormatErrorMessage(_propertyDisplayName);
            context.Attributes.Add("data-val-begreaterthan", errorMessage);
            context.Attributes.Add("data-val-begreaterthan-otherpropertyname", _otherPropertyName);
            context.Attributes.Add("data-val-begreaterthan-prefix", _prefix);
        }
    }
}
