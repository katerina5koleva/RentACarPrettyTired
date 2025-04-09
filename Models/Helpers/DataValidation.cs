using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Data.Helpers
{
    /// <summary>
    /// Validation attribute to ensure a date is in the future.
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates whether the provided value is a future date.
        /// </summary>
        /// <param name="value">The value to validate, expected to be a DateTime.</param>
        /// <param name="validationContext">The context in which the validation is performed.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating success if the date is in the future,
        /// or an error message if the date is in the past.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date < DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be in the future");
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Validation attribute to ensure a date is after another specified date property.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class DateAfterAttribute : ValidationAttribute
        {
            private readonly string _comparisonProperty;

            /// <summary>
            /// Initializes a new instance of the <see cref="DateAfterAttribute"/> class.
            /// </summary>
            /// <param name="comparisonProperty">The name of the property to compare against.</param>
            public DateAfterAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            /// <summary>
            /// Validates whether the provided value is after the specified comparison property's value.
            /// </summary>
            /// <param name="value">The value to validate, expected to be a DateTime.</param>
            /// <param name="validationContext">The context in which the validation is performed.</param>
            /// <returns>
            /// A <see cref="ValidationResult"/> indicating success if the date is after the comparison property's value,
            /// or an error message if it is not.
            /// </returns>
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);
                if (propertyInfo == null)
                {
                    return new ValidationResult($"Unknown property: {_comparisonProperty}");
                }

                var comparisonValue = (DateTime?)propertyInfo.GetValue(validationContext.ObjectInstance);

                if (value is DateTime dateValue && comparisonValue.HasValue)
                {
                    if (dateValue.Date <= comparisonValue.Value.Date)
                    {
                        return new ValidationResult(
                            ErrorMessage ?? $"{validationContext.DisplayName} must be after {_comparisonProperty}");
                    }
                }

                return ValidationResult.Success;
            }
        }
    }
}