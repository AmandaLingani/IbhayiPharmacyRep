using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrescribingSystem.Models
{
    public class MedicationItem
    {
        [Key]
        public int MedicationItemId { get; set; }

        [Required]
        [ForeignKey(nameof(Prescription))]
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; } //Still the same as prescription- just named differently

        [Required]
        [ForeignKey(nameof(Medication))]
        public int MedicationId { get; set; }
        public Medication Medication { get; set; }

        public int TotalRepeats { get; set; } //same as repeatsLeft- just namedDifferntluy

        // Prescription-specific fields
        public string? Dosage { get; set; }  // Can override catalog dosage
        public int Quantity { get; set; } //Added
        public string? Instructions { get; set; } //Added
        public MedicationStatus? Status { get; set; } = MedicationStatus.Pending; //Added
    }

    public enum MedicationStatus //Added
    {
        Pending,
        Dispensed
    }
}
