using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace UserManagementWithIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles
                });
            }

            return View(userViewModels);
        }


        //public async Task<IActionResult> Add()
        //{
        //    var roles = await _roleManager.Roles.Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToListAsync();
        //    var viewModel = new AddUserViewModel
        //    {
        //        Roles = roles
        //    };
        //    return View(viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(AddUserViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    if (!model.Roles.Any(r => r.IsSelected))
        //    {
        //        ModelState.AddModelError("Roles", "Please select at least one role");
        //        return View(model);
        //    }
        //    if (await _userManager.FindByEmailAsync(model.Email) != null)
        //    {
        //        ModelState.AddModelError("Email", "Email is alreasy exists");
        //        return View(model);  

        //    }
        //    if (await _userManager.FindByNameAsync(model.UserName) != null)
        //    {
        //        ModelState.AddModelError("UserName", "Username is alreasy exists");
        //        return View(model);

        //    }
        //    var user = new ApplicationUser()
        //    {
        //        UserName = model.UserName,
        //        Email = model.Email,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //    };
        //    var result = await _userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("Roles", error.Description);
        //        }
        //        return View(model);
        //    }
        //    await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

        //    return RedirectToAction("Index");
        //}

        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);

                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();


            var viewModel = new ProfileFormViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName , 
                LastName = user.LastName ,  
                UserName = user.UserName,
                Email = user.Email
                
            };

            return View(viewModel);
        }

        [HttpPost]        // To Post On View of Index
        [ValidateAntiForgeryToken]     
        // Keep in your mind the shape of form Edit.cshtml and the operation of Model of this View and Ddata 
        // I Want To Save The Data of Model To Index ()<=
        public async Task<IActionResult> Edit(ProfileFormViewModel model)
        {
            if(!ModelState.IsValid)
              return View(model); 
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if(userWithSameEmail!=null && userWithSameEmail.Id != model.Id)   
            {
                ModelState.AddModelError("Email", "This Email Is Already Assigned To Another User");
                return View(model);
            }


            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            if (userWithSameUserName != null && userWithSameUserName.Id != model.Id)
            {
                ModelState.AddModelError("UserName", "This UserName Is Already Assigned To Another User");
                return View(model);
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
         
            await _userManager.UpdateAsync(user);
             

            return RedirectToAction(nameof(Index));
        }






































        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new AddUserViewModel
            {
                Roles = roles.Select(r => new RoleViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsSelected = false
                }).ToList()      
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList();
                    await _userManager.AddToRolesAsync(user, selectedRoles);

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If we got this far, something failed; redisplay the form
            var roles = await _roleManager.Roles.ToListAsync();
            model.Roles = roles.Select(r => new RoleViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
                IsSelected = false
            }).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> IsUserNameAvailable(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return Json(user == null);
        }

        [HttpPost]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Json(user == null);
        }









    }

}
