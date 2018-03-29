using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TaskSolution.CustomValidationAttributes;

namespace TaskSolution.ViewModels
{
    public class CompanyViewModel
    {
        [ScaffoldColumn(false)]
        public int ComanyId { get; set; }

        [Display(Name ="NIP")]
        public string NIP { get; set; }

        [Display(Name = "REGON")]
        public string REGON { get; set; }

        [Display(Name = "KRS")]
        public string KRS { get; set; }

        [Display(Name = "Firma")]
        public string CompanyName { get; set; }
    }

    public class AddCompanyViewModel
    {
        [Display(Name = "NIP")]
        [Required(ErrorMessage ="Pole NIP jest wymagane")]
        [NipIsValid(ErrorMessage = "Nr NIP jest nieprawidłowy")]
        public string NIP { get; set; }

        [Display(Name = "REGON")]
        [Required(ErrorMessage = "Pole REGON jest wymagane")]
        [RegonIsValid(ErrorMessage ="Nr REGON jest nieprawidłowy")]
        public string REGON { get; set; }

        [Display(Name = "KRS")]
        [Required(ErrorMessage = "Pole KRS jest wymagane")]
        [KrsIsValid(ErrorMessage = "Nr KRS jest nieprawidłowy")]
        public string KRS { get; set; }

        [Display(Name = "Firma")]
        [Required(ErrorMessage = "Pole Firma jest wymagane")]
        public string CompanyName { get; set; }

        public AddAddressViewModel CompanyAddress { get; set; }
    }

    public class EditCompanyViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ComanyId { get; set; }

        [Display(Name = "NIP")]
        [Required(ErrorMessage = "Pole NIP jest wymagane")]
        [NipIsValid(ErrorMessage = "Nr NIP jest nieprawidłowy")]
        public string NIP { get; set; }

        [Display(Name = "REGON")]
        [Required(ErrorMessage = "Pole REGON jest wymagane")]
        [RegonIsValid(ErrorMessage = "Nr REGON jest nieprawidłowy")]
        public string REGON { get; set; }

        [Display(Name = "KRS")]
        [Required(ErrorMessage = "Pole KRS jest wymagane")]
        [KrsIsValid(ErrorMessage = "Nr KRS jest nieprawidłowy")]
        public string KRS { get; set; }

        [Display(Name = "Firma")]
        [Required(ErrorMessage = "Pole Firma jest wymagane")]
        public string CompanyName { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Pole Miasto jest wymagane")]
        public string City { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Pole Ulica jest wymagane")]
        public string Street { get; set; }

        [Display(Name = "Nr domu/mieszkania")]
        [Required(ErrorMessage = "Pole Nr domu/mieszkania jest wymagane")]
        public string HouseNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Pole Kod pocztowy jest wymagane")]
        [RegularExpression(@"[0-9]{2}(?:-[0-9]{3})", ErrorMessage = "Błędny kod pocztowy. Poprawny format xx-xxx")]
        public string ZipCode { get; set; }
    }

    public class CompanyDetailsViewModel
    {
        [ScaffoldColumn(false)]
        public int ComanyId { get; set; }

        [Display(Name = "NIP")]
        public string NIP { get; set; }

        [Display(Name = "REGON")]
        public string REGON { get; set; }

        [Display(Name = "KRS")]
        public string KRS { get; set; }

        [Display(Name = "Firma")]
        public string CompanyName { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Nr domu/mieszkania")]
        public string HouseNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }
    }

    public class CompanyDetailsApiViewModel
    {
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
    }
}