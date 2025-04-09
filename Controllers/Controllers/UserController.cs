using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data.Models;
using RentACar.Data.ViewModels;
using RentACar.Repositories.Interfaces;
using RentACar.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Controller responsible for managing user-related operations such as creating, editing, deleting, and viewing users.
/// </summary>
[Authorize(Roles = "Administrator")]

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="userRepository">Repository for user data operations.</param>
    /// <param name="userManager">Manager for user identity operations.</param>
    /// <param name="roleManager">Manager for role identity operations.</param>
    /// <param name="signInManager">Manager for user sign-in operations.</param>
    public UserController(
        IUserRepository userRepository,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<User> signInManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    /// <summary>
    /// Displays a list of all users with their roles.
    /// </summary>
    /// <returns>A view containing a list of users.</returns>
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllAsync();
        var userVMs = new List<UserVM>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userVM = new UserVM
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Surname = user.Surname,
                NIN = user.NIN,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Requests = user.Requests,
                Role = roles.FirstOrDefault()
            };
            userVMs.Add(userVM);
        }

        return View(userVMs);
    }

    /// <summary>
    /// Displays details of a specific user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>A view containing user details.</returns>
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var vm = new UserVM
        {
            Firstname = user.Firstname,
            Surname = user.Surname,
            NIN = user.NIN,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Requests = user.Requests,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
        };

        return View(vm);
    }

    /// <summary>
    /// Displays the user creation form.
    /// </summary>
    /// <returns>A view for creating a new user.</returns>
    [AllowAnonymous]
    public IActionResult Create()
    {
        return View(new CreateUserVM());
    }

    /// <summary>
    /// Handles the creation of a new user.
    /// </summary>
    /// <param name="vm">The view model containing user data.</param>
    /// <returns>Redirects to the home page if successful, otherwise redisplays the form with validation errors.</returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserVM vm)
    {
        if (await _userRepository.AnyAsync(u => u.NIN == vm.NIN))
        {
            ModelState.AddModelError("NIN", "This NIN is already registered");
        }

        if (await _userManager.FindByEmailAsync(vm.Email) != null)
        {
            ModelState.AddModelError("Email", "Email is already in use");
        }

        if (await _userManager.FindByNameAsync(vm.UserName) != null)
        {
            ModelState.AddModelError("UserName", "Username is taken");
        }
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = vm.Email,
                Email = vm.Email,
                Firstname = vm.Firstname,
                Surname = vm.Surname,
                NIN = vm.NIN,
                PhoneNumber = vm.PhoneNumber
            };


            var result = await _userRepository.AddAsync(user, vm.Password);

            if (result)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
        }


        return View(vm);
    }

    /// <summary>
    /// Displays the user edit form.
    /// </summary>
    /// <param name="id">The ID of the user to edit.</param>
    /// <returns>A view for editing the user.</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var roles = await _userManager.GetRolesAsync(user);

        var vm = new EditUserVM
        {
            Id = user.Id,
            Firstname = user.Firstname,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Role = roles.FirstOrDefault()
        };

        ViewBag.Roles = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
        return View(vm);
    }

    /// <summary>
    /// Handles the editing of a user.
    /// </summary>
    /// <param name="model">The view model containing updated user data.</param>
    /// <returns>Redirects to the user list if successful, otherwise redisplays the form with validation errors.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditUserVM model)
    {
        var log = new StringBuilder();
        log.AppendLine($"--- EDIT POST ---");
        log.AppendLine($"ID: {model.Id}");
        log.AppendLine($"Name: {model.Firstname} {model.Surname}");
        log.AppendLine($"Role: {model.Role}");


        foreach (var key in Request.Form.Keys)
        {
            log.AppendLine($"{key}: {Request.Form[key]}");
        }

        System.Diagnostics.Debug.WriteLine(log.ToString());
        Console.WriteLine($"--- EDIT POST STARTED ---");
        Console.WriteLine($"Model ID: {model?.Id ?? "NULL"}");
        Console.WriteLine($"Name: {model?.Firstname} {model?.Surname}");
        Console.WriteLine($"Role: {model?.Role}");
        Console.WriteLine($"Phone: {model?.PhoneNumber}");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("❌ ModelState Invalid. Errors:");
            foreach (var error in ModelState.Where(e => e.Value.Errors.Any()))
            {
                Console.WriteLine($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            ViewBag.Roles = await GetRolesSelectList();
            return View(model);
        }

        try
        {
            var user = await _userManager.Users
                .Include(u => u.Requests)
                .FirstOrDefaultAsync(u => u.Id == model.Id);

            if (user == null)
            {
                Console.WriteLine("❌ USER NOT FOUND IN DATABASE");
                return NotFound();
            }

            Console.WriteLine($"🔄 Updating user {user.Id}");
            Console.WriteLine($"FROM: {user.Firstname} {user.Surname} | {user.NIN} | {user.PhoneNumber}");
            Console.WriteLine($"TO: {model.Firstname} {model.Surname} | {model.PhoneNumber}");

            user.Firstname = model.Firstname;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;


            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                Console.WriteLine("❌ UPDATE FAILED. Errors:");
                foreach (var error in updateResult.Errors)
                {
                    Console.WriteLine($"{error.Code}: {error.Description}");
                }

                ModelState.AddModelError("", "Failed to update user");
                ViewBag.Roles = await GetRolesSelectList();
                return View(model);
            }



            var currentRoles = await _userManager.GetRolesAsync(user);
            var currentRole = currentRoles.FirstOrDefault();

            if (model.Role != currentRole)
            {
                Console.WriteLine($"🔄 CHANGING ROLE FROM {currentRole} TO {model.Role}");

                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    Console.WriteLine("❌ FAILED TO REMOVE ROLES");
                    foreach (var error in removeResult.Errors)
                    {
                        Console.WriteLine($"{error.Code}: {error.Description}");
                    }
                }

                var addResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!addResult.Succeeded)
                {
                    Console.WriteLine("❌ FAILED TO ADD ROLE");
                    foreach (var error in addResult.Errors)
                    {
                        Console.WriteLine($"{error.Code}: {error.Description}");
                    }
                }
            }

            Console.WriteLine("✅ UPDATE SUCCESSFUL");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"‼️ EXCEPTION: {ex.Message}");
            Console.WriteLine($"STACK TRACE: {ex.StackTrace}");

            ModelState.AddModelError("", "An unexpected error occurred");
            ViewBag.Roles = await GetRolesSelectList();
            return View(model);
        }
    }

    /// <summary>
    /// Retrieves a list of roles for dropdown selection.
    /// </summary>
    /// <returns>A <see cref="SelectList"/> of roles.</returns>
    private async Task<SelectList> GetRolesSelectList()
    {
        try
        {
            var roles = await _roleManager.Roles
                .AsNoTracking()
                .OrderBy(r => r.Name)
                .ToListAsync();

            Console.WriteLine($"Loaded {roles.Count} roles for dropdown");
            return new SelectList(roles, "Name", "Name");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR LOADING ROLES: {ex.Message}");
            return new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }

    /// <summary>
    /// Toggles the role of a user.
    /// </summary>
    /// <param name="id">The ID of the user whose role is to be toggled.</param>
    /// <returns>Redirects to the edit page of the user.</returns>
    [HttpPost]
    public async Task<IActionResult> ToggleRole(string id)
    {
        var result = await _userService.ChangeRoleAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Edit), new { id });
    }


    /// <summary>
    /// Displays the user deletion confirmation page.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A view for confirming user deletion.</returns>
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var vm = new UserVM
        {
            Firstname = user.Firstname,
            Surname = user.Surname,
            Email = user.Email,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
        };

        return View(vm);
    }

    /// <summary>
    /// Handles the deletion of a user.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>Redirects to the user list if successful, otherwise redisplays the confirmation page with errors.</returns>
    [HttpPost, ActionName("DeleteUser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();

        var result = await _userRepository.DeleteAsync(user);
        if (result)
        {
            TempData["SuccessMessage"] = "User deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "Failed to delete user";
        return View(await _userRepository.GetByIdAsync(id));
    }
}