﻿using SEP490.DB.Models;
using SEP490.Modules.ProductModule.ManageProduct.DTO;

namespace SEP490.Modules.ProductModule.ManageProduct.Service
{
    public interface IProductService
    {
        List<ProductDto> GetAllProducts();
        void CreateProduct(CreateProductProductDto dto);
        bool DeleteProduct(int id);
        Product? GetProductById(int id);
        bool UpdateProduct(int id, UpdateProductProductDto dto);
        List<ProductDto> GetTop5BestSellingProducts();

    }
}
