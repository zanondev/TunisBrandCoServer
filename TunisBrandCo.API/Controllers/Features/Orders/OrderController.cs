﻿using Microsoft.AspNetCore.Mvc;
using TunisBrandCo.Application.Features.Client;
using TunisBrandCo.Application.Features.Order;
using TunisBrandCo.Application.Features.Products;
using TunisBrandCo.Domain.Features.Clients;
using TunisBrandCo.Domain.Features.Orders;
using TunisBrandCo.Domain.Features.Products;
using TunisBrandCo.Infra.Data.Features.Clients;
using TunisBrandCo.Infra.Data.Features.Orders;
using TunisBrandCo.Infra.Data.Features.Products;

namespace TunisBrandCo.API.Controllers.Features.Orders
{
    [ApiController]
    [Route("api/orders")]

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;

        public OrderController()
        {
            _orderRepository = new OrderRepository();
            _productRepository = new ProductRepository();   
            _productService = new ProductService(_productRepository);
            _clientRepository = new ClientRepository();
            _orderService = new OrderService(_productRepository, _clientRepository, _productService, _orderRepository);
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] Order newOrder)
        {
            return Ok(_orderService.AddOrder(newOrder));
        }

        [HttpDelete]
        public IActionResult DeleteOrder(int orderId)
        {
            return Ok(_orderService.DeleteOrder(orderId));
        }

        [HttpPatch]
        public IActionResult UpdateStatus(int orderId, int status)
        {
            return Ok(_orderService.updateStatus(orderId, status));
        }

        [HttpGet("{status}")]
        public IActionResult GetStatus(int orderId)
        {
            return Ok(_orderService.GetStatus(orderId));
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }
    }
}
