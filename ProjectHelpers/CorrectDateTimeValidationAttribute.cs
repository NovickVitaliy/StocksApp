using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProjectHelpers
{
  public class CorrectDateTimeValidationAttribute : ValidationAttribute
  {
    private DateTime _minimumDateTime;

    public CorrectDateTimeValidationAttribute(int year, int month, int day)
    {
      _minimumDateTime = new DateTime(year, month, day);
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      if (value != null)
      {
        DateTime date = (DateTime)value;

        if (date > _minimumDateTime)
        {
          return ValidationResult.Success;
        }

        return new ValidationResult($"DateTime should be older than {_minimumDateTime}");

      }

      return new ValidationResult("DateTime was not supplied");
    }
  }
}