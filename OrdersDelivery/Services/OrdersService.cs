using OrdersDelivery.Services.ServiceInterfaces;
using OrdersDelivery.Data;
using OrdersDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace OrdersDelivery.Services
{
    public class OrdersService : IOrdersService
    {
        readonly AppDbContext _context;

        public OrdersService(AppDbContext context) 
        { 
            _context = context;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            orders = _context.Orders.ToList();

            foreach (Order order in orders)
            {
                order.PickupDate = order.PickupDate.ToLocalTime();
            }

            return orders;
        }

        public List<Order> SearchOrders(string filter)
        {
            List<Order> orders = GetOrders();
            
            if (!String.IsNullOrEmpty(filter))
            {
                orders = orders.AsQueryable().Where(o => o.Number.Contains(filter)).ToList();
            }

            return orders;
        }

        public void AddOrder(Order order)
        {
            order.PickupDate = order.PickupDate.ToUniversalTime();
            order.Number = GenerateOrderNumber(order);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public string GenerateOrderNumber(Order order)
        {
            StringBuilder stringBuilder = new StringBuilder(order.GetHashCode().ToString());
            stringBuilder.Append(order.SenderCity[0]);
            stringBuilder.Append(order.RecipientCity[0]);

            return stringBuilder.ToString();
        }
    }
}
