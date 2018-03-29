using BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace TaskSolution.CustomValidationAttributes
{
    public class RegonIsValidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string regon = value.ToString();
                return CompanyNumbersManagement.RegonIsValid(regon);
            }
            return false;
        }
    }
}