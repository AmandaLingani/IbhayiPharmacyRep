using Microsoft.AspNetCore.Mvc.Rendering;
using PrescribingSystem.Data;

namespace PrescribingSystem.Models.ViewModels
{
    public class PrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public DateTime DatePrescribed { get; set; } = DateTime.Now;
        public int SelectedDoctorId { get; set; }
        public List<SelectListItem>? DoctorsList { get; set; }
        public string? SelectedCustomerId { get; set; }
        public List<SelectListItem>? ExistingCustomers { get; set; }
        public DoctorViewModel NewDoctor { get; set; }
        public CustomerAdditionViewModel NewCustomer { get; set; } = new();
        public CustomerType CustomerType { get; set; }
        public int SelectedMedicationId { get; set; } //might be ICollection considering that med >=1 and also must create a PrecMed table/ CustMedTable or CustPrescMed table
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public int RepeatsLeft { get; set; }
        public List<MedicationPrescriptionViewModel> SelectedMedications { get; set; } = new List<MedicationPrescriptionViewModel>();
        public List<SelectListItem>? MedicationList { get; set; } = new List<SelectListItem>();
        public IFormFile PrescriptionFile { get; set; }
        public string PrescriptionFileUrl { get; set; }
        public bool HasAllergyWarning { get; set; }
        public bool IsDispensed { get; set; }
        public bool OverrideAllergyConfirmed { get; set; }
        public string? OverrideReason { get; set; }
    }

    public class MedicationPrescriptionViewModel
    {
        public int MedicationPrescriptionId { get; set; }
        public int MedicationId { get; set; }
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public int RepeatsLeft { get; set; }
    }
}
