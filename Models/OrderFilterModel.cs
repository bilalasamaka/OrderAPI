using OrderAPI.Models.Enums;

namespace OrderAPI.Models
{
    public class OrderFilterModel
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public string SearchText { get; set; }

        public DateTime? StartDate { get; set; } = DateTime.MinValue;

        public DateTime? EndDate { get; set; } = DateTime.Now;

        public List<OrderStatus> Statuses { get; set; }
        public string SortBy { get; set; }
    }
}
