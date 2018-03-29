using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace TaskSolution.ViewModels
{
    public class SearchDetailViewModel
    {
        [Display(Name ="Wprowadzony numer")]
        public string Number { get; set; }

        [Display(Name = "Wartości nagłówka HTTP")]
        public string HeaderValues { get; set; }

        [Display(Name = "Rozpoznany typ numeru")]
        public NumberType NumberType { get; set; }
    }
}