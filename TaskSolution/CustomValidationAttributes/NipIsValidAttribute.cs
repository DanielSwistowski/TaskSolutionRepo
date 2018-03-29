using BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace TaskSolution.CustomValidationAttributes
{
    public class NipIsValidAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string nip = value.ToString();
                return CompanyNumbersManagement.NipIsValid(nip);
            }
            return false;
        }
    }
}