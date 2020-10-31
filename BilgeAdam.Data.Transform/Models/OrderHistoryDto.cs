using System;

namespace BilgeAdam.Data.Transform.Models
{
    internal class OrderHistoryDto
    {
        public string CompanyName { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal OrderPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal Summary { get; set; }
    }
}