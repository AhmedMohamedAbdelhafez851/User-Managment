using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        


        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            
        }

        public async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.ToListAsync(); // Await ToListAsync()

            return View(roles); // Pass roles to the view
        }


        //public IActionResult Add()
        //{
        //    return View();
        //}  

        //[HttpPost]
        //public async Task<IActionResult> Add(string roleName)
        //{
        //    if (!string.IsNullOrWhiteSpace(roleName))
        //    {
        //        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        //        if (!roleExists)
        //        {
        //            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Role already exists.");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Role name cannot be empty.");
        //    }

        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", await _roleManager.Roles.ToListAsync());

            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("Name", "Role is exists!");
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));

            return RedirectToAction(nameof(Index));
        }

    }
}


    
