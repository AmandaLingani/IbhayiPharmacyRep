using Microsoft.EntityFrameworkCore;
using PrescribingSystem.Data;
using PrescribingSystem.Interfaces;
using PrescribingSystem.Models;
using PrescribingSystem.Models.ViewModels;

namespace PrescribingSystem.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<IGrouping<DateTime, Prescription>>> GetCustomerUploadedPrescriptionsAsync()
        {
            return (await _context.Prescriptions
                 .Include(p => p.Customer)
                 .Where(p => p.Source == "CustomerUploaded" && p.PrescriptionStatus == PrescriptionStatus.Pending)
                 .OrderByDescending(p => p.PrescriptionDate.Date)
                 .ToListAsync())
                 .GroupBy(p => p.PrescriptionDate.Date);
        }
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByCustomerAsync(string customerId)
        {
            return await _context.Prescriptions
                .Include(p => p.Customer)
                .Include(p => p.Doctor)
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }
        public async Task<Prescription?> GetPrescriptionByIdAsync(int prescriptionId)
        {
            return await _context.Prescriptions
                .Include(p => p.Customer)
                .Include(p => p.Doctor)
                .Include(p => p.MedicationItems)
                .ThenInclude(mp => mp.Medication)
                .FirstOrDefaultAsync(p => p.PrescriptionId == prescriptionId);
        }
        public async Task AddPrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            return await _context.Doctor
                .ToListAsync();
        }
        public async Task<IEnumerable<Medication>> GetMedicationsAsync()
        {
            return await _context.Medication
                .ToListAsync();
        }

        public async Task<IEnumerable<MedicationItem>> GetMedicationsForProcessingAsync(int prescriptionId)
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.MedicationItems)
                .ThenInclude(mp => mp.Medication)
                .FirstOrDefaultAsync(p => p.PrescriptionId == prescriptionId);

            if (prescriptions == null)
                return Enumerable.Empty<MedicationItem>();

            if (prescriptions.MedicationItems.All(m => m.TotalRepeats == null || m.TotalRepeats == 5))
                return prescriptions.MedicationItems;

            return prescriptions.MedicationItems
                .Where(m => m.TotalRepeats > 0)
                .ToList();
        }
      

    }

}
