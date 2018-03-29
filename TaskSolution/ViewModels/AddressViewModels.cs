using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TaskSolution.ViewModels
{
    public class AddAddressViewModel
    {
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
}