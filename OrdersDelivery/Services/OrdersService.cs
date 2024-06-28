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

        public OrdersService(AppDbContext context) => _context = context;

        /*
         * In real systems all dates are stored in the database in UTC format
         * and turns into a local time on frontend, so i model it by explicit conversion at line 27.
         */
        public List<Order> GetOrders()
        {
            List<Order> orders = _context.Orders.OrderByDescending(o => o.PickupDate).ToList();

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
                orders = orders.Where(o => o.Number.Contains(filter)).ToList();
            }

            return orders;
        }

        /*
         * In real systems all dates are stored in the database in UTC format
         * and turns to a local time on frontend, so i model it by explicit conversion at line 51.
         */
        public void AddOrder(Order order)
        {
            order.PickupDate = order.PickupDate.ToUniversalTime();
            order.Number = GenerateOrderNumber(order);
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        /*
         * Generating of Order Number is based on object hash & others but not only on hash, 
         * because GetHashCode() can return not unique hash.
         * Using of other parameters not give guarantees that Order Number will be unique, 
         * but it rases chances of it, so i decided that it will be enough.
         */
        public string GenerateOrderNumber(Order order) => $"{order.GetHashCode()}-{order.SenderCity[0]}{order.RecipientCity[0]}-{order.Weight}";
    }
}
