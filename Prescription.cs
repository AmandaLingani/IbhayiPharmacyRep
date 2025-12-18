using PrescribingSystem.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrescribingSystem.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        [Required]
        public string CustomerId { get; set; }  // Assuming Identity User Id

        [ForeignKey(nameof(CustomerId))]
        public ApplicationUser Customer { get; set; }   // Navigation property

        public int? DoctorId { get; set; } // just added
        public Doctor? Doctor { get; set; }  // just added
        public string? PharmacistId { get; set; }
        public ApplicationUser Pharmacist {get;set;}
       
        public string? DoctorName { get; set; }
        public string Source { get; set; } // Just added "WalkIn" or "CustomerUploaded/Customer portal"

        public DateTime PrescriptionDate { get; set; }
        public DateTime? LastUpdated { get; set; } = DateTime.Now; //Just added


        public decimal? TotalCost { get; set; }

        public string FilePath { get; set; } //assuming that this is the same as PrescriptionUrl just named differently

        public string? RawText { get; set; }

        // NEW FIELD: customer decides if pharmacist should process
        public bool ShouldProcess { get; set; } = false;
        public bool HasAllergyConflict { get; set; } = false; //JustAdded
        public string? OverrideReason { get; set; } //JustAdded
        public bool IsDispensed => PrescriptionStatus == PrescriptionStatus.Dispensed; //Just added
        public PrescriptionStatus PrescriptionStatus { get; set; } = PrescriptionStatus.Pending;

        // Navigation property
        public ICollection<MedicationItem> MedicationItems { get; set; } = new List<MedicationItem>();
    }
    public enum PrescriptionStatus
    {
        Pending = 0, //The new 'Uploaded" just a name change.
        Processed = 1,
        ReadyForCollection = 2,
        Collected = 3,
        Rejected = 4,
        Saved = 5,
        Dispensed = 6,
        PartiallyDispensed = 7

    }
    public enum CustomerType
    {
        NewCustomer,
        ExistingCustomer
    }
}
