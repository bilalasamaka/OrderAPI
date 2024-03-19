using FluentValidation;
using OrderAPI.Models;

namespace OrderAPI.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Price).GreaterThan(0);
            RuleFor(order => order.StoreName).NotEmpty();
            RuleFor(order => order.CustomerName).NotEmpty();
            RuleFor(order => order.CreatedOn).NotEmpty();
        }
    }
}
