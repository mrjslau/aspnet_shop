using FuriousWeb.Models;

namespace FuriousWeb.Factories
{
    public static class ProductImageFactory
    {
        public static ProductImage Create(int productId, string relativePathToImage, bool isMainImage)
        {
            var productImage = new ProductImage();
            productImage.ProductId = productId;
            productImage.IsMainImage = isMainImage;
            productImage.RelativePath = relativePathToImage;

            return productImage;
        }
    }
}