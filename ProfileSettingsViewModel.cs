using System.ComponentModel.DataAnnotations;

namespace PrescribingSystem.Models.ViewModels
{
    public class ProfileSettingsViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string HCRNumber { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
    }
}
