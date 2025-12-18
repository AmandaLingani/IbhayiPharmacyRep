namespace PrescribingSystem.Models.ViewModels
{
    public class ContinuePrescriptionViewModel
    {
        public int PrescriptionId { get; set; }
        public string CustomerName { get; set; }
        public string DoctorName { get; set; }
        public string PrescriptionFileUrl { get; set; }
        public List<int> SelectedMedicationIds { get; set; } = new();
        public List<ContinueMedicationViewModel> Medications { get; set; } = new();
        public bool HasAllergyWarning { get; set; }
        public bool OverrideAllergyConfirmed { get; set; }
        public string OverrideReason { get; set; }
        public string Status { get; set; }
        public DateTime? LastUpdated { get; set; }

    }
    public class ContinueMedicationViewModel
    {
        public int MedicationPrescriptionId { get; set; }
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public int RepeatsLeft { get; set; }
        public MedicationStatus? Status { get; set; }
        public bool IsSelected { get; set; }
        public RepeatRequestViewModel? RepeatRequest{get;set;}
    }
}
