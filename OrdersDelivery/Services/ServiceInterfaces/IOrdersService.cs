using OrdersDelivery.Models;

namespace OrdersDelivery.Services.ServiceInterfaces
{
    public interface IOrdersService
    {
        public List<Order> GetOrders();

        public List<Order> SearchOrders(string filter);

        public void AddOrder(Order order);
    }
}
