using BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace TaskSolution.CustomValidationAttributes
{
    public class KrsIsValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string krs = value.ToString();
                return CompanyNumbersManagement.KrsIsValid(krs);
            }
            return false;
        }
    }
}