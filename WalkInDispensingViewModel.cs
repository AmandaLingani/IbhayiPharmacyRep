using Microsoft.AspNetCore.Mvc.Rendering;

namespace PrescribingSystem.Models.ViewModels
{
    public class WalkInDispensingViewModel
    {
        public string SelectedCustomerId { get; set; }
        public string CustomerIdentityNumber { get; set; }
        public List<SelectListItem> Customers { get; set; } = new();
        public List<MedicationItemViewModel> Medications { get; set; } = new();
        public bool HasAllergyWarning { get; set; }
        public string OverrideReason { get; set; }
        public bool OverrideAllergyConfirmed { get; set; }
    }

    public class MedicationItemViewModel
    {
        public int MedicationItemId { get; set; }
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public int RepeatsLeft { get; set; }
        public string Status { get; set; }
        public bool IsSelected { get; set; }
        public string DoctorName { get; set; }
    }
}
