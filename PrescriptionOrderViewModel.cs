namespace PrescribingSystem.Models.ViewModels
{
    public class PrescriptionOrderViewModel
    {
        public int PrescriptionOrdersId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime? PackedDate { get; set; }
        public DateTime? ReadyDate { get; set; }
        public DateTime? CollectedDate { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }

    public class OrderItemViewModel
    {
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public decimal Price { get; set; }

    }
}
