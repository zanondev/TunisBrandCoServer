﻿using Microsoft.AspNetCore.Mvc;
using TunisBrandCo.Application.Features.Client;
using TunisBrandCo.Application.Features.Products;
using TunisBrandCo.Domain.Features.Clients;
using TunisBrandCo.Domain.Features.Products;
using TunisBrandCo.Infra.Data.Features.Products;

namespace TunisBrandCo.API.Controllers.Features.Products
{
    [ApiController]
    [Route("api/products")]

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductService _productService;

        public ProductController()
        {
            _productRepository = new ProductRepository();
            _productService = new ProductService(_productRepository);
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] Product newProduct)
        {
            return Ok(_productService.AddProduct(newProduct));
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteProduct(int id)
        //{
        //    return Ok(/*_productService.DeleteClient(cpf)*/);
        //}
    }
}