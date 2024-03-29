﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TunisBrandCo.Domain.Features.Clients;
using TunisBrandCo.Domain.Features.Orders;
using TunisBrandCo.Infra.Data.Features.Clients;

namespace TunisBrandCo.Infra.Data.Features.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO _orderDAO;

        public OrderRepository()
        {
            _orderDAO = new OrderDAO();
        }
        public void AddOrder(Order newOrder)
        {
            _orderDAO.AddOrder(newOrder);
        }

        public void DeleteOrder(int orderId)
        {
            _orderDAO.DeleteOrder(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderDAO.GetAllOrders();
        }

        public Order GetStatus(int Id)
        {
            return _orderDAO.GetStatusById(Id);
        }

        public void UpdateStatus(int orderId, int status)
        {
           _orderDAO.UpdateStatus(orderId, status);
        }

        public Order GetOrderById(int orderId)
        {
            return _orderDAO.GetOrderById(orderId);
        }

    }
}
