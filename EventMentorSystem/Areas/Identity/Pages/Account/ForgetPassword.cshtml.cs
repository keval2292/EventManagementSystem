using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using EMS.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MudBlazor;

namespace EventMentorSystem.Areas.Identity.Pages.Account
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        public ForgetPasswordModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

        }
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Your email is not found.");
            }
            else 
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, Input.Password);
                if (result.Succeeded)
                {

                    return RedirectToPage("./Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, "your password is not reset.");
                }
                
            }
            return Page();
            }
    }
}
