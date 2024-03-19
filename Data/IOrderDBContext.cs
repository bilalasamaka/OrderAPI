using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;

namespace OrderAPI.Data
{
    public interface IOrderDBContext
    {
        public DbSet<Order> Orders { get; set; }

        public int SaveChanges();

    }
}
