using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models;
using OrderAPI.Services.Interfaces;
using OrderAPI.Validators;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PostOrder([FromBody] List<Order> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                return BadRequest("No orders provided.");
            }

            try
            {
                _orderService.CreateOrders(orders);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding orders: {ex.Message}");
            }
        }

        [HttpPost("search")]
        public IActionResult SearchOrders([FromBody] OrderFilterModel filter)
        {
            if (filter == null)
            {
                return BadRequest("No filter provided.");
            }

            try
            {
                var searchResult = _orderService.SearchOrders(filter);
                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching orders: {ex.Message}");
            }
        }
    }
}
