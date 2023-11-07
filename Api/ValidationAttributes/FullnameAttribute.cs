using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class FullnameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string fullname = value.ToString();
            string[] parts = fullname.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                return new ValidationResult("Fullname should contain name, surname and middlename");
            }

            foreach (string part in parts)
            {
                if (!Regex.IsMatch(part, @"^[A-Za-zА-Яа-я]+$"))
                {
                    return new ValidationResult("Each part of fullname should contain only letters");
                }
            }
        }

        return ValidationResult.Success;
    }
}
