using System.ComponentModel.DataAnnotations;
using YES.Infrastructure;

namespace YES.CustomValidation
{
    public class EmailUniqueAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                var dbContext = (AppDbContext)validationContext.GetService(typeof(AppDbContext));

                if (dbContext.Users.Any(u => u.Email == email))
                {
                    return new ValidationResult("Email already exists.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
