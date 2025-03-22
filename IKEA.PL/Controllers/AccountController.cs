using IKEA.DAL.Models.Identity;
using IKEA.PL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Register
		#region Get
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		#endregion
		#region Post
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var ExistingUser = await _userManager.FindByNameAsync(signUpViewModel.UserName);
			if (ExistingUser != null)
			{
				ModelState.AddModelError(nameof(signUpViewModel.UserName), "This User Name is already taken");
				return View(signUpViewModel);
			}
			var User = new ApplicationUser()
			{
				FName = signUpViewModel.FirstName,
				LName = signUpViewModel.LastName,
				UserName = signUpViewModel.UserName,
				Email = signUpViewModel.Email,
				IsAgree = signUpViewModel.IsAgree,
			};
			var result = await _userManager.CreateAsync(User,signUpViewModel.Password);
			if (result.Succeeded)
			{
				return RedirectToAction(nameof(SignIn));
			}
            foreach (var error in result.Errors)
            {
				ModelState.AddModelError(string.Empty, error.Description);
            }
			return View(signUpViewModel);
        }
		#endregion
		#endregion
		#region SignIn
		public IActionResult SignIn()
		{
			return View();
		}
		#endregion

	}
}
