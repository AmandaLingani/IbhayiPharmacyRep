using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrescribingSystem.Models.ViewModels
{
    public class PharmacistReportViewModel
    {
        public int ReportId { get; set; }
        public string CustomerId { get; set; }
        //public string PharmacistId { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;

        public List<Prescription> Prescriptions { get; set; }
        public ICollection<MedicationItem> MedicationPrescription { get; set; }

        public List<DateTime> Dates { get; set; }
        public DateTime SelectedStartDate { get; set; } = DateTime.Now;
        public DateTime SelectedEndDate { get; set; } = DateTime.Now;
        public int TotalPrescriptions { get; set; }

        public ReportGrouping Grouping { get; set; } = ReportGrouping.ByPatient;

        //Filters
        public string? SelectedPatientId { get; set; }
        public int? SelectedMedicationId { get; set; }
        public int? SelectedSchedule { get; set; }

        public List<SelectListItem> Patients { get; set; } = new();
        public List<SelectListItem> Medications { get; set; } = new();
        public List<SelectListItem> Schedules { get; set; } = new();

    }

    public enum ReportGrouping
    {
        ByPatient = 0,
        ByMedication = 1,
        BySchedule = 2
    }
}
