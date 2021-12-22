using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleAssignment.Models;
using PeopleAssignment.Models.ViewModels;

namespace PeopleAssignment.Controllers
{
    public class AccountController : Controller
    {

        private SignInManager<User> SignInManager { get; }
        private UserManager<User> UserManager { get; }

        public AccountController(
            SignInManager<User> signInManager, 
            UserManager<User> userManager
            )
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                return View(entry);
            }

            Models.User user = new Models.User()
            {
                FirstName = entry.Name,
                LastName = entry.Family,
                UserName = entry.Username,
                Birthdate = entry.Birthdate
            };

            IdentityResult result = await UserManager.CreateAsync(user, entry.Password);

            if (result.Succeeded)
            {
                /*
                IdentityRole visitor =  await RoleManager.Roles
                    .SingleOrDefaultAsync(role => role.Name.Equals("visitor"));
                */

                await UserManager.AddToRoleAsync(user, "visitor");
                return RedirectToAction(nameof(SignIn));
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return View(entry);
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string returnUrl, SignInViewModel entry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View(entry);
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(
                entry.Username,
                entry.Password,
                entry.RememberMe,
                false
            );

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("NotAllowed", "You're not allowed to login.");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("LockedOut", "You're locked out from login.");
            }

            if (!result.Succeeded && !result.IsNotAllowed && !result.IsLockedOut)
            {
                ModelState.AddModelError("UsernameAndPassword", "Invalid Username or Password.");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(entry);

        }

        public async Task<IActionResult> SigningOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
