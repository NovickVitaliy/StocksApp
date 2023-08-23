using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHelpers
{
  public class ModelValidator
  {
    public static void Validate(object obj)
    {
      ValidationContext validationContext = new ValidationContext(obj);
      List<ValidationResult> validationResults = new List<ValidationResult>();
      bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

      if (!isValid)
        throw new ArgumentException(validationResults.First().ErrorMessage);
    }
  }
}
