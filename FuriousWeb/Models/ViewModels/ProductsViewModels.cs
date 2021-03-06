﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FuriousWeb.Models.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Būtina įvesti prekės kodą")]
        [Display(Name = "Kodas")]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Būtina įvesti prekės pavadinimą")]
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
        public byte[] RowVersion { get; set; }

        public List<ProductImage> SecondaryImages { get; set; }
        public ProductImage MainImage { get; set; }

        public EditProductViewModel()
        {
            SecondaryImages = new List<ProductImage>();
        }

        public EditProductViewModel(Product product, ProductImage mainImg, List<ProductImage> secondaryImages)
        {
            Id = product.Id;
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            RowVersion = product.RowVersion;

            SecondaryImages = secondaryImages;
            MainImage = mainImg;
        }
    }

    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public ProductImage ProductMainImage { get; set; }
        public List<ProductImage> ProductSecondaryImages { get; set; }
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