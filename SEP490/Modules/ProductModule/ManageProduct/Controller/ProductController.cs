﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490.DB;
using SEP490.Modules.ProductModule.ManageProduct.DTO;
using SEP490.Modules.ProductModule.ManageProduct.Service;

namespace SEP490.Modules.ProductModule.ManageProduct.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly SEP490DbContext _context;

        public ProductController(IProductService productService, SEP490DbContext context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGlassStructures([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query)) query = "";

            var results = await _context.GlassStructures
                .Where(gs => gs.ProductName != null && gs.ProductName.Contains(query))
                .Select(gs => new {
                    id = gs.Id,
                    productName = gs.ProductName
                })
                .Take(20)
                .ToListAsync();

            return Ok(results);
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);

            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm." });

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] UpdateProductProductDto dto)
        {
            try
            {
                var result = _productService.UpdateProduct(id, dto);
                if (!result)
                    return NotFound(new { message = "Không tìm thấy sản phẩm để cập nhật." });

                return Ok(new { message = "Cập nhật sản phẩm thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Cập nhật sản phẩm thất bại!", error = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGlassStructures()
        {
            var structures = await _context.GlassStructures
                .Select(g => new {
                    g.Id,
                    g.ProductName
                })
                .ToListAsync();

            return Ok(structures);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductProductDto dto)
        {
            try
            {
                _productService.CreateProduct(dto);
                return Ok(new { message = "Tạo sản phẩm thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Tạo sản phẩm thất bại!", error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var success = _productService.DeleteProduct(id);
                if (!success)
                    return NotFound(new { message = "Không tìm thấy sản phẩm để xoá." });

                return Ok(new { message = "Đã xoá sản phẩm thành công!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Xoá sản phẩm thất bại!", error = ex.Message });
            }
        }

        [HttpGet("top-selling")]
        public IActionResult GetTop5BestSellingProducts()
        {
            var topProducts = _productService.GetTop5BestSellingProducts();
            return Ok(topProducts);
        }

    }
}
