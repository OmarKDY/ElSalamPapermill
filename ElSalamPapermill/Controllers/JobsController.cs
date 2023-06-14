using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElSalamPapermill.Data;
using ElSalamPapermill.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ElSalamPapermill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public JobsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: Jobs
        [HttpGet("getCandidateList")]
        public async Task<IActionResult> Index()
        {
              return _context.Jobs != null ? 
                          Ok(await _context.Jobs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
        }

        // GET: Jobs/Details/5
        [HttpGet("getCandidate/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jobs == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .FirstOrDefaultAsync(m => m.CandidateId == id);
            if (jobs == null)
            {
                return NotFound();
            }
            return Ok(jobs);
        }

        //[HttpPost("addCandidate")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> Create([FromForm] Jobs jobs)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = jobs.CandidateEmail,
        //            Email = jobs.CandidateEmail,
        //            PhoneNumber = jobs.CandidateMobileNo
        //        };

        //        var result = await _userManager.CreateAsync(user, "candidate");
        //        if (result.Succeeded)
        //        {
        //            jobs.User = user;

        //            if (jobs.CandidateCV != null && jobs.CandidateCV.Length > 0)
        //            {
        //                if (!Path.GetExtension(jobs.CandidateCV.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        //                {
        //                    ModelState.AddModelError("CandidateCV", "The CV file must be in PDF format.");
        //                    return BadRequest(ModelState);
        //                }

        //                var uniqueFileName = Guid.NewGuid().ToString() + "_" + jobs.CandidateCV.FileName;
        //                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "CVs", uniqueFileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    await jobs.CandidateCV.CopyToAsync(stream);
        //                }

        //                jobs.CandidateCVPath = filePath;
        //            }
        //            else
        //            {
        //                return BadRequest("Candidate CV is required.");
        //            }

        //            _context.Add(jobs);
        //            await _context.SaveChangesAsync();

        //            return Ok("Candidate created successfully");
        //        }
        //        else
        //        {
        //            var errors = result.Errors.Select(e => e.Description);
        //            return BadRequest(errors);
        //        }
        //    }

        //    return BadRequest(ModelState);
        //}


        // POST: Jobs/Create
        [HttpPost("addCandidate")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                if (jobs.CandidateCV != null && jobs.CandidateCV.Length > 0)
                {
                    if (!Path.GetExtension(jobs.CandidateCV.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("CandidateCV", "The CV file must be in PDF format.");
                        return BadRequest(ModelState);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + jobs.CandidateCV.FileName;
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "CVs", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await jobs.CandidateCV.CopyToAsync(stream);
                    }

                    jobs.CandidateCVPath = filePath;
                    _context.Add(jobs);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest("Candidate Cv Is Required");
                }
            }
            return Ok("Candidate Request Sent Successfully");
        }



        // GET: Jobs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Jobs == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobs = await _context.Jobs.FindAsync(id);
        //    if (jobs == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(jobs);
        //}

        // POST: Jobs/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CandidateId,CandidateFN,CandidateLN,CandidateEmail,CandidateMobileNo,CandidateAddress,Country,CandidateJobTitle,CandidateCV,CandidateMsg")] Jobs jobs)
        //{
        //    if (id != jobs.CandidateId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(jobs);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!JobsExists(jobs.CandidateId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(jobs);
        //}

        // GET: Jobs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Jobs == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobs = await _context.Jobs
        //        .FirstOrDefaultAsync(m => m.CandidateId == id);
        //    if (jobs == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(jobs);
        //}

        // POST: Jobs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Jobs == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Jobs'  is null.");
        //    }
        //    var jobs = await _context.Jobs.FindAsync(id);
        //    if (jobs != null)
        //    {
        //        _context.Jobs.Remove(jobs);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool JobsExists(int id)
        {
          return (_context.Jobs?.Any(e => e.CandidateId == id)).GetValueOrDefault();
        }
    }
}
