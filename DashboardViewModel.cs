namespace PrescribingSystem.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int PrescriptionsToday { get; set; }
        public int PendingPrescriptions { get; set; }
        public int TotalCustomers { get; set; }
        public int LowStockAlerts { get; set; }

        public List<string> WeeklyLabels { get; set; } = new();
        public List<int> WeeklyCounts { get; set; } = new();

        public List<string> TopMedicineNames { get; set; } = new();
        public List<int> TopMedicineCounts { get; set; } = new();
    }
}
