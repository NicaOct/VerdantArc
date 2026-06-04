using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerdantArc.Application.DTOs;
using VerdantArc.Application.Interfaces;
using VerdantArc.Domain.Entities;
using VerdantArc.Web.Models;

namespace VerdantArc.Web.Controllers.MVC;

[Authorize(Roles = "Admin")]
[Route("Admin")]
public class AdminController : Controller
{
    private readonly ITrailService _trailService;
    private readonly ISpeciesService _speciesService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        ITrailService trailService,
        ISpeciesService speciesService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _trailService = trailService;
        _speciesService = speciesService;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var trails = await _trailService.GetAllAsync(1, 100);
        var species = await _speciesService.GetAllAsync();
        var usersCount = await _userManager.Users.CountAsync();

        ViewBag.TrailsCount = trails.Count();
        ViewBag.SpeciesCount = species.Count();
        ViewBag.UsersCount = usersCount;

        return View();
    }

    // --- TRAILS CRUD ---

    [HttpGet("Trails")]
    public async Task<IActionResult> Trails()
    {
        var trails = await _trailService.GetAllAsync(1, 100);
        return View(trails);
    }

    [HttpGet("Trails/Create")]
    public IActionResult TrailsCreate()
    {
        return View(new TrailCreateDto());
    }

    [HttpPost("Trails/Create")]
    public async Task<IActionResult> TrailsCreate(TrailCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _trailService.CreateAsync(dto);
        return RedirectToAction(nameof(Trails));
    }

    [HttpGet("Trails/Edit/{id}")]
    public async Task<IActionResult> TrailsEdit(int id)
    {
        var trail = await _trailService.GetByIdAsync(id);
        if (trail == null) return NotFound();

        var dto = new TrailUpdateDto
        {
            Id = trail.Id,
            Name = trail.Name,
            Description = trail.Description,
            DifficultyLevel = Enum.Parse<VerdantArc.Domain.Enums.DifficultyLevel>(trail.DifficultyLevel),
            DistanceKm = trail.DistanceKm,
            EstimatedDurationMinutes = trail.EstimatedDurationMinutes,
            ElevationGainM = trail.ElevationGainM,
            ImageUrl = trail.ImageUrl
        };
        return View(dto);
    }

    [HttpPost("Trails/Edit/{id}")]
    public async Task<IActionResult> TrailsEdit(int id, TrailUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _trailService.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Trails));
    }

    [HttpPost("Trails/Delete/{id}")]
    public async Task<IActionResult> TrailsDelete(int id)
    {
        await _trailService.DeleteAsync(id);
        return RedirectToAction(nameof(Trails));
    }

    // --- SPECIES CRUD ---

    [HttpGet("Species")]
    public async Task<IActionResult> Species()
    {
        var species = await _speciesService.GetAllAsync();
        return View(species);
    }

    [HttpGet("Species/Create")]
    public IActionResult SpeciesCreate()
    {
        return View(new SpeciesCreateDto());
    }

    [HttpPost("Species/Create")]
    public async Task<IActionResult> SpeciesCreate(SpeciesCreateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _speciesService.CreateAsync(dto);
        return RedirectToAction(nameof(Species));
    }

    [HttpGet("Species/Edit/{id}")]
    public async Task<IActionResult> SpeciesEdit(int id)
    {
        var s = await _speciesService.GetByIdAsync(id);
        if (s == null) return NotFound();

        var dto = new SpeciesUpdateDto
        {
            Id = s.Id,
            CommonName = s.CommonName,
            ScientificName = s.ScientificName,
            Category = Enum.Parse<VerdantArc.Domain.Enums.Category>(s.Category),
            ConservationStatus = Enum.Parse<VerdantArc.Domain.Enums.ConservationStatus>(s.ConservationStatus),
            Description = s.Description,
            ImageUrl = s.ImageUrl,
            InatNaturalistId = s.InatNaturalistId,
            IsEndemic = s.IsEndemic
        };
        return View(dto);
    }

    [HttpPost("Species/Edit/{id}")]
    public async Task<IActionResult> SpeciesEdit(int id, SpeciesUpdateDto dto)
    {
        if (!ModelState.IsValid) return View(dto);
        await _speciesService.UpdateAsync(id, dto);
        return RedirectToAction(nameof(Species));
    }

    [HttpPost("Species/Delete/{id}")]
    public async Task<IActionResult> SpeciesDelete(int id)
    {
        await _speciesService.DeleteAsync(id);
        return RedirectToAction(nameof(Species));
    }

    // --- USER MANAGEMENT ---

    [HttpGet("Users")]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.ToListAsync();
        var userViewModels = new List<UserAdminViewModel>();

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            userViewModels.Add(new UserAdminViewModel
            {
                Id = u.Id,
                Email = u.Email ?? "",
                FullName = u.FullName,
                RegisteredAt = u.RegisteredAt,
                Roles = roles.ToList()
            });
        }

        return View(userViewModels);
    }

    [HttpPost("Users/ToggleAdmin/{id}")]
    public async Task<IActionResult> ToggleAdmin(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        if (user.Email == "admin@verdantarc.ro")
        {
            TempData["Error"] = "Cannot modify roles of the default administrator account.";
            return RedirectToAction(nameof(Users));
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        if (isAdmin)
        {
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");
        }
        else
        {
            await _userManager.RemoveFromRoleAsync(user, "User");
            await _userManager.AddToRoleAsync(user, "Admin");
        }

        return RedirectToAction(nameof(Users));
    }

    [HttpPost("Users/Delete/{id}")]
    public async Task<IActionResult> UsersDelete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();

        if (user.Email == "admin@verdantarc.ro")
        {
            TempData["Error"] = "Cannot delete the default administrator account.";
            return RedirectToAction(nameof(Users));
        }

        await _userManager.DeleteAsync(user);
        return RedirectToAction(nameof(Users));
    }
}
