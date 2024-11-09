using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Shop.Models;
using Web_Shop.Models.ViewModels;

namespace Web_Shop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUseModel> _userManager;
        private SignInManager<AppUseModel> _signInManager;

        public AccountController(SignInManager<AppUseModel> signInManager, UserManager<AppUseModel> userManager)
        {
            _signInManager = signInManager; 
            _userManager = userManager;
        }
        //Đây là trả về trang
        public IActionResult Login(string returnUrl)
        {
            return View( new LoginViewModel { ReturnUrl = returnUrl});
        }

        //Đây là hàm
        [HttpPost]        
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", " Username hoặc Password bị sai");
            }
            return View(loginVM);
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserModel user )
		{
            if(ModelState.IsValid)
            {
                AppUseModel newUser = new AppUseModel { UserName = user.UserName, Email = user.Email };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if(result.Succeeded)
                {
                    TempData["success"] = "Tạo user thành công";
                    return Redirect("/Account/login");

                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
			return View(user);
		}
        
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

	}
}
