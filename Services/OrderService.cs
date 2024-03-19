using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;
using OrderAPI.Services.Interfaces;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDBContext _context;
        private readonly IValidator<Order> _orderValidator;

        public OrderService(IOrderDBContext context, IValidator<Order> orderValidator)
        {
            _context = context;
            _orderValidator = orderValidator;
        }

        public void CreateOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                var validationResult = _orderValidator.Validate(order);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                if (order.BrandId != 0)
                {
                    _context.Orders.Add(order);
                }
            }
            _context.SaveChanges();
        }

        public List<Order> SearchOrders(OrderFilterModel filter)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                query = query.Where(o => o.StoreName.Contains(filter.SearchText) || o.CustomerName.Contains(filter.SearchText));
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(o => o.CreatedOn >= filter.StartDate);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(o => o.CreatedOn <= filter.EndDate);
            }

            if (filter.Statuses != null && filter.Statuses.Any())
            {
                query = query.Where(o => filter.Statuses.Contains(o.Status));
            }
            var result = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                PropertyInfo property = typeof(Order).GetProperty(filter.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    result = result.OrderBy(o => property.GetValue(o)).ToList();
                }
                else
                {
                    result = result.OrderBy(o => o.Id).ToList();
                }
            }

            return result;
        }
    }
}
