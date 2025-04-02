using System.ComponentModel.DataAnnotations;

namespace RestaurantMVCCodeFirst.View_Model
{
    public class ResetPasswordViewModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string? UserName { get; set; }
    }
}
