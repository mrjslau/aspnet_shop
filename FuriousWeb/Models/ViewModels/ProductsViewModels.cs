using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{
    public class CreateDetailedProductViewModel
    {
        [Required]
        [Display(Name = "Kodas")]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Pavadinimas")]
        public string Name { get; set; }

        [Display(Name = "Aprašymas")]
        public string Description { get; set; }

        [Display(Name = "Kaina")]
        public double Price { get; set; }
    }

    public class DeleteConfirmProductViewModel
    {
        [Display(Name = "Kodas")]
        public string Code { get; set; }

        [Display(Name = "Pavadinimas")]
        public string Name { get; set; }

        [Display(Name = "Aprašymas")]
        public string Description { get; set; }
    }
}