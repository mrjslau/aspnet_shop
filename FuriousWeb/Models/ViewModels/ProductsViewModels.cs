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

        [Display(Name = "Sandėlio kodas")]
        [MaxLength(50)]
        public string WarehouseCode { get; set; }

        [Display(Name = "Įkelti prekę į sandėlį")]
        public bool AddToStock { get; set; }

        [Display(Name = "Kiekis")]
        public long Quantity { get; set; }

        [Display(Name = "Kaina")]
        public double Price { get; set; }


        public List<string> WarehousesCodes { get; set; }

        public CreateDetailedProductViewModel() { }

        public CreateDetailedProductViewModel(List<string> warehousesCodes)
        {
            WarehousesCodes = warehousesCodes;
        }
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