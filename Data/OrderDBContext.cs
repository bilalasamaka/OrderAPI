using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public class OrderDBContext : DbContext, IOrderDBContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        int IOrderDBContext.SaveChanges() 
        {
            return base.SaveChanges();
        }
    }
}
