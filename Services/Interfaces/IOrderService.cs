using OrderAPI.Models;

namespace OrderAPI.Services.Interfaces
{
    public interface IOrderService
    {
        void CreateOrders(List<Order> orders);
        List<Order> SearchOrders(OrderFilterModel filter);
    }
}
