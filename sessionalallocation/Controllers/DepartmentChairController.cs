using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SessionalAllocation.Data;
using SessionalAllocation.Models;
using SessionalAllocation.ViewModels;

namespace SessionalAllocation.Controllers
{
    [Authorize(Roles = "DepartmentChair")]
    public class DepartmentChairController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DepartmentChairController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //get the logged in users id
            var ident = User.Identity as ClaimsIdentity;
            var userId = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var departments = _context.Department.Include(u => u.DepartmentOwnerNavigation).Where(u => u.DepartmentOwner == userId);
            return View(await departments.ToListAsync());
        }

        public async Task<IActionResult> Units(int? id)
        {
            var units = _context.Unit.Include(u => u.DepartmentNavigation).Include(u => u.UnitOwnerNavigation).Where(u => u.Department == id);
            return View(await units.ToListAsync());
        }

        // Retrieve Classes for specified unit
        public async Task<IActionResult> Classes(int? id, string filter)
        {
            var unitDbContext = _context.Class.Include(AspNetUsers => AspNetUsers.TutorAllocatedNavigation).Include(Unit => Unit.Unit).Where(Class => Class.UnitId == id);
            if (filter == "Allocated")
            {
                unitDbContext = _context.Class.Include(AspNetUsers => AspNetUsers.TutorAllocatedNavigation).Include(Unit => Unit.Unit).Where(Class => Class.UnitId == id && Class.Allocated);
            }
            else if (filter == "NotAllocated")
            {
                unitDbContext = _context.Class.Include(AspNetUsers => AspNetUsers.TutorAllocatedNavigation).Include(Unit => Unit.Unit).Where(Class => Class.UnitId == id && !Class.Allocated);
            }
            else if (filter == "NotApproved")
            {
                unitDbContext = _context.Class.Include(AspNetUsers => AspNetUsers.TutorAllocatedNavigation).Include(Unit => Unit.Unit).Where(Class => Class.UnitId == id && Class.Allocated && !Class.Approved);
            }
            return View(await unitDbContext.OrderBy(x => x.StartDate).ThenBy(x => x.StartTimeScheduled).ToListAsync());
        }

        // Convener nomination of applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nominate(int id)
        {
            // get the application details, class details
            Applications app = await _context.Applications.FindAsync(id);
            Class aClass = await _context.Class.Include(u => u.TutorAllocatedNavigation).FirstOrDefaultAsync(c => c.Id == app.AppliedClass);

            // setting previous applicant details if exists
            Applications prevApp = null;
            if (aClass.Allocated)
            {
                prevApp = _context.Applications.Where(a => a.AppliedClass == aClass.Id && a.Applicant == aClass.TutorAllocatedNavigation.Id).First();
                prevApp.ProvisionallyAllocated = false;
                prevApp.Approved = false;
            }

            aClass.TutorAllocated = app.Applicant;
            app.ProvisionallyAllocated = true;
            aClass.Allocated = true;

            if (ModelState.IsValid)
            {
                _context.Update(aClass);
                _context.Update(app);
                if (prevApp != null)
                {
                    _context.Update(prevApp);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Classes", new { Id = aClass.UnitId });
        }

        // Reject nominated applicant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            // get the application details, class details
            Applications app = await _context.Applications.FindAsync(id);
            Class aClass = await _context.Class.FindAsync(app.AppliedClass);

            aClass.TutorAllocated = null;
            app.ProvisionallyAllocated = false;
            app.Approved = false;
            aClass.Allocated = false;
            aClass.Approved = false;

            if (ModelState.IsValid)
            {
                _context.Update(aClass);
                _context.Update(app);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Classes", new { Id = aClass.UnitId });
        }

        // Retrieve Applicants for specified class
        public async Task<IActionResult> Applicants(int? id)
        {
            //var unitDbContext = _context.Applications.Include(a => a.ApplicantNavigation).Include(a => a.AppliedClassNavigation).Where(Applications => Applications.AppliedClass == id);
            //return View(await unitDbContext.ToListAsync());
            var applicants = _context.Applications.Include(a => a.ApplicantNavigation).Include(a => a.AppliedClassNavigation).Where(Applications => Applications.AppliedClass == id);
            //var ident = User.Identity as ClaimsIdentity;
            string convenorId = null;
            var unit = _context.Unit.Where(u => u.Id == applicants.FirstOrDefault().AppliedClassNavigation.UnitId);//.FirstOrDefault().UnitOwner;

            if (unit.Any())
            {
                convenorId = unit.FirstOrDefault().UnitOwner;
            }
            
            //var convenorId = ident.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            List<ApplicantsViewModel> model = new List<ApplicantsViewModel>();
            foreach (var a in applicants)
            {
                ApplicantsViewModel temp = new ApplicantsViewModel();
                temp.Application = a;
                if (convenorId != null)
                {
                    var rating = _context.TutorPreference.Where(r => r.TutorId == a.Applicant && r.ConvenorId == convenorId).FirstOrDefault();
                    temp.rating = rating.rating;
                }
                model.Add(temp);
            }
            // sort by rating and preference
            model.Sort((x, y) => y.Application.Preference.CompareTo(x.Application.Preference));
            model.Sort((x, y) => y.rating.CompareTo(x.rating));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            // get the application details, class details
            Class aClass = await _context.Class.Include(u => u.TutorAllocatedNavigation).FirstOrDefaultAsync(c => c.Id == id);
            var application = _context.Applications.Where(a => a.AppliedClass == aClass.Id && a.Applicant == aClass.TutorAllocatedNavigation.Id).First();
            application.Approved = true;

            aClass.Approved = true;

            if (ModelState.IsValid)
            {
                _context.Update(aClass);
                _context.Update(application);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Classes", new { Id = aClass.UnitId });
        }

        // For approving tutors for all allocated classes
        public async Task<IActionResult> ApproveAll(Dictionary<string, string> idParms)
        {
            int? unitId = null;
            foreach (var item in idParms)
            {
                Class aClass = await _context.Class.Include(u => u.TutorAllocatedNavigation).FirstOrDefaultAsync(c => c.Id == Convert.ToInt32(item.Value));
                aClass.Approved = true;
                var application = _context.Applications.Where(a => a.AppliedClass == aClass.Id && a.Applicant == aClass.TutorAllocatedNavigation.Id).First();
                application.Approved = true;

                if (ModelState.IsValid)
                {
                    _context.Update(aClass);
                    //_context.Update(application);
                    await _context.SaveChangesAsync();
                }
                unitId = aClass.UnitId; // need to set unitId for redirection
            }
            return RedirectToAction("Classes", new { Id = unitId });
        }

        // edit unit is used to only set convenors for the unit
        // GET: Units/Edit/5
        public async Task<IActionResult> EditUnit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            ViewData["Department"] = new SelectList(_context.Department, "Id", "Name", unit.Department);
            ViewData["UnitOwner"] = new SelectList(_context.Users, "Id", "FullName", unit.UnitOwner).Prepend(new SelectListItem("None", null));
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUnit(int id, [Bind("Id,UnitCode,UnitName,Department,UnitOwner")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (unit.UnitOwner == "None")
                    {
                        unit.UnitOwner = null;
                    }
                    else
                    {   // if UnitOwner is not in convenor role, give convenor role
                        var user = await _context.Users.FindAsync(unit.UnitOwner);
                        if (!(await _userManager.IsInRoleAsync(user, "Convenor")))
                        {
                            await _userManager.AddToRoleAsync(user, "Convenor");
                        }
                    }
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Units", new { Id = unit.Department });
            }
            ViewData["Department"] = new SelectList(_context.Department, "Id", "Id", unit.Department);
            ViewData["UnitOwner"] = new SelectList(_context.Users, "Id", "Id", unit.UnitOwner).Prepend(new SelectListItem("None", null));
            return RedirectToAction("Units", new { Id = unit.Department });
        }

        private bool UnitExists(int id)
        {
            return _context.Unit.Any(e => e.Id == id);
        }
    }
}