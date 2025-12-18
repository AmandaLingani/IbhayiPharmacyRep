using PrescribingSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace PrescribingSystem.Models
{
    public class UserAllergy
    {
        [Key]
        public int UserAllergyId { get; set; } //JustAdded
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public int ActiveIngredientId { get; set; }
        public ActiveIngredients ActiveIngredient { get; set; }
    }

}
