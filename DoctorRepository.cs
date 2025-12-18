using Microsoft.EntityFrameworkCore;
using PrescribingSystem.Data;
using PrescribingSystem.Interfaces;
using PrescribingSystem.Models;
using PrescribingSystem.Models.ViewModels;

namespace PrescribingSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctor(DoctorViewModel model)
        {
            var doctor = new Doctor
            {
                DoctorId = model.DoctorId,
                Name = model.DoctorFirstName,
                Surname = model.DoctorLastName,
                Practice_Number = model.PracticeNumber,
                Email = model.Email
            };

            _context.Doctor.Add(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
