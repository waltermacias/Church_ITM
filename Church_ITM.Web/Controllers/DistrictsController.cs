using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Church_ITM.Common.Entities;
using Church_ITM.Web.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Church_ITM.Web.Controllers
{
     [Authorize(Roles = "Admin")]
     public class DistrictsController : Controller
     {
          private readonly DataContext _context;

          public DistrictsController(DataContext context)
          {
               _context = context;
          }

          public async Task<IActionResult> Index()
          {
               return View(await _context.Districts
                   .Include(c => c.Campuses)
                   .ToListAsync());
          }

          public async Task<IActionResult> Details(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts
                   .Include(c => c.Campuses)
                   .ThenInclude(d => d.Churchs)
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (district == null)
               {
                    return NotFound();
               }

               return View(district);
          }

          public IActionResult Create()
          {
               return View(new District());
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create(District district)
          {
               if (ModelState.IsValid)
               {
                    try
                    {
                         _context.Add(district);
                         await _context.SaveChangesAsync();
                         return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }

               return View(district);
          }

          public async Task<IActionResult> Edit(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts.FindAsync(id);
               if (district == null)
               {
                    return NotFound();
               }

               return View(district);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(int id, District district)
          {
               if (id != district.Id)
               {
                    return NotFound();
               }

               if (ModelState.IsValid)
               {
                    try
                    {
                         _context.Update(district);
                         await _context.SaveChangesAsync();
                         return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }

               return View(district);
          }

          public async Task<IActionResult> Delete(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts
                   .Include(c => c.Campuses)
                   .ThenInclude(d => d.Churchs)
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (district == null)
               {
                    return NotFound();
               }

               _context.Districts.Remove(district);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
          }

          public async Task<IActionResult> AddCampus(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts.FindAsync(id);
               if (district == null)
               {
                    return NotFound();
               }

               Campus model = new Campus { IdDistrict = district.Id };
               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> AddCampus(Campus campus)
          {
               if (ModelState.IsValid)
               {
                    District district = await _context.Districts
                        .Include(c => c.Campuses)
                        .FirstOrDefaultAsync(c => c.Id == campus.IdDistrict);
                    if (district == null)
                    {
                         return NotFound();
                    }

                    try
                    {
                         campus.Id = 0;
                         district.Campuses.Add(campus);
                         _context.Update(district);
                         await _context.SaveChangesAsync();
                         return RedirectToAction($"{nameof(Details)}/{district.Id}");
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }

               return View(campus);
          }

          public async Task<IActionResult> EditCampus(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses.FindAsync(id);
               if (campus == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts.FirstOrDefaultAsync(c => c.Campuses.FirstOrDefault(d => d.Id == department.Id) != null);
               campus.IdDistrict = district.Id;
               return View(campus);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditCampus(Campus campus)
          {
               if (ModelState.IsValid)
               {
                    try
                    {
                         _context.Update(campus);
                         await _context.SaveChangesAsync();
                         return RedirectToAction($"{nameof(Details)}/{campus.IdDistrict}");
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }
               return View(campus);
          }

          public async Task<IActionResult> DetailsCampus(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses
                   .Include(d => d.Churchs)
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (campus == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts.FirstOrDefaultAsync(c => c.Campuses.FirstOrDefault(d => d.Id == department.Id) != null);
               campus.IdDistrict = district.Id;
               return View(campus);
          }

          public async Task<IActionResult> DeleteCampus(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses
                   .Include(d => d.Churchs)
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (campus == null)
               {
                    return NotFound();
               }

               District district = await _context.Districts.FirstOrDefaultAsync(c => c.Campuses.FirstOrDefault(d => d.Id == department.Id) != null);
               _context.Campuses.Remove(campus);
               await _context.SaveChangesAsync();
               return RedirectToAction($"{nameof(Details)}/{district.Id}");
          }

          public async Task<IActionResult> AddChurch(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses.FindAsync(id);
               if (campus == null)
               {
                    return NotFound();
               }

               Church model = new Church { IdCampus = campus.Id };
               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> AddChurch(Church church)
          {
               if (ModelState.IsValid)
               {
                    Campus campus = await _context.Campuses
                        .Include(d => d.Churchs)
                        .FirstOrDefaultAsync(c => c.Id == church.IdCampus);
                    if (campus == null)
                    {
                         return NotFound();
                    }

                    try
                    {
                         church.Id = 0;
                         campus.Churchs.Add(church);
                         _context.Update(campus);
                         await _context.SaveChangesAsync();
                         return RedirectToAction($"{nameof(DetailsCampus)}/{campus.Id}");

                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }

               return View(church);
          }

          public async Task<IActionResult> EditChurch(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Church church = await _context.Churchs.FindAsync(id);
               if (church == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses.FirstOrDefaultAsync(d => d.Churchs.FirstOrDefault(c => c.Id == church.Id) != null);
               church.IdCampus = campus.Id;
               return View(church);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> EditChurch(Church church)
          {
               if (ModelState.IsValid)
               {
                    try
                    {
                         _context.Update(church);
                         await _context.SaveChangesAsync();
                         return RedirectToAction($"{nameof(DetailsCampus)}/{church.IdCampus}");

                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                         if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                         {
                              ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                         }
                         else
                         {
                              ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                         }
                    }
                    catch (Exception exception)
                    {
                         ModelState.AddModelError(string.Empty, exception.Message);
                    }
               }
               return View(church);
          }

          public async Task<IActionResult> DeleteChurch(int? id)
          {
               if (id == null)
               {
                    return NotFound();
               }

               Church church = await _context.Churchs
                   .FirstOrDefaultAsync(m => m.Id == id);
               if (church == null)
               {
                    return NotFound();
               }

               Campus campus = await _context.Campuses.FirstOrDefaultAsync(d => d.Churchs.FirstOrDefault(c => c.Id == church.Id) != null);
               _context.Churchs.Remove(church);
               await _context.SaveChangesAsync();
               return RedirectToAction($"{nameof(DetailsCampus)}/{campus.Id}");
          }
     }
}