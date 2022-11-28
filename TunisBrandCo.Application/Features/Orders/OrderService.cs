﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TunisBrandCo.Domain.Exceptions;
using TunisBrandCo.Domain.Features.Orders;
using TunisBrandCo.Domain.Features.Clients;
using TunisBrandCo.Domain.Features.Products;
using TunisBrandCo.Application.Features.Products;
using TunisBrandCo.Application.Features.Client;
using TunisBrandCo.Infra.Data.Features.Products;
using TunisBrandCo.Infra.Data.Features.Orders;
using TunisBrandCo.Infra.Data.Features.Clients;

namespace TunisBrandCo.Application.Features.Order
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;

        public OrderService(IProductRepository productRepository, IClientRepository clientRepository, ProductService productService, IOrderRepository orderRepository)
        {
            _productRepository = new ProductRepository();
            _clientRepository = new ClientRepository();
            _productService = new ProductService(_productRepository);
            _orderRepository = new OrderRepository();
        }

        public object AddOrder(Domain.Features.Orders.Order newOrder)
        {
            if (newOrder == null)
                throw new NotFoundException($"Order is null.");

            var orderList = _orderRepository.GetAllOrders();

            foreach (var order in orderList)
            {
                if (order.Id == newOrder.Id)
                    throw new AlreadyExistsException($"Order Id: {newOrder.Id} already exists.");
            }

            var product = _productRepository.GetProductById(newOrder.Product.Id);

            if (product == null)
                throw new NotFoundException($"Product Id: {product.Id} doesn't exists.");

            newOrder.Product = product;

            if (newOrder.ProductQuantity >= newOrder.Product.StockQuantity)
                throw new NotAllowedException($"Product quantity must be less than stock.");

            if (newOrder.ProductQuantity < 1)
                throw new NotAllowedException($"Product quantity must be more than one.");

            var client = _clientRepository.GetClientById(newOrder.Client.Id);

            if (client == null)
                throw new NotFoundException($"Client Id: {client.Id} doesn't exists.");

            var totalPrice = newOrder.Product.Price * newOrder.ProductQuantity;

            newOrder.Client = client;
            
            newOrder.TotalPrice = totalPrice;

            _orderRepository.AddOrder(newOrder);

            _productService.RemoveStock(newOrder.Product.Id, newOrder.ProductQuantity);

            return newOrder;
        }

        public string DeleteOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);

            if (order == null)
                throw new NotFoundException($"Order: {order.Id} doesn't exists.");

            _orderRepository.DeleteOrder(order.Id);

            return "Pedido deletado com sucesso!";
        }
    }
}
