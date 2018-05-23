using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{
    public class CreateProductViewModel
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

    public class EditProductViewModel : CreateProductViewModel
    {
        public int Id { get; set; } //product Id, will be hidden

        public EditProductViewModel(Product product)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
        }

        public EditProductViewModel() { }
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