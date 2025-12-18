using Microsoft.AspNetCore.Mvc.Rendering;
using PrescribingSystem.Data;

namespace PrescribingSystem.Models.ViewModels
{
    public class ProcessPrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        //public string CustomerLastName { get; set; }
        public string IdentityNumber { get; set; }
        public int? SelectedDoctorId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DoctorViewModel NewDoctor { get; set; } = new();
        public IEnumerable<Doctor> Doctors { get; set; } = Enumerable.Empty<Doctor>();
        //public List<SelectListItem> DoctorSelectList { get; set; } 
        public string PrescriptionFileUrl { get; set; }
        public List<Medication> MedicationsList { get; set; } = new();
        //public IEnumerable<Medication> MedicationsList { get; set; } = Enumerable.Empty<Medication>();
        public int MedicationId { get; set; }
        public List<MedicationInputViewModel> Medications { get; set; } = new ();
        public MedicationInputViewModel NewMedications { get; set; }
        public bool HasAllergyWarning { get; set; }
        public bool OverrideAllergyConfirmed { get; set; }
        public string OverrideReason { get; set; }
        public bool WantsDispense { get; set; }
        //public bool WantsRepeatsPacked { get; set; }
        public string Status { get; set; }
    }
}

public class MedicationInputViewModel
{
    public int MedicationId { get; set; }
    public string MedicationName { get; set; } 
    public int Quantity { get; set; }
    public string Instructions { get; set; }
    public int RepeatsLeft { get; set; }
}
