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
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SuppliersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Suppliers
        [HttpGet("getSupplierList")]
        public async Task<IActionResult> Index()
        {
              return _context.Suppliers != null ? 
                          Ok(await _context.Suppliers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Suppliers'  is null.");
        }

        // GET: Suppliers/Details/5
        [HttpGet("getSupplier/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var suppliers = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.SupplierId == id);
            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        [HttpPost]
        [HttpPost("addSupplier")]
        public async Task<IActionResult> Create([Bind("SupplierId,SupplierFN,SupplierLN,SupplierEmail,SupplierMobileNo,SupplierAddress,Country,Material,SupplierMsg")] Suppliers suppliers)
        {
            _context.Add(suppliers);
            await _context.SaveChangesAsync();
            return Ok("Supplier Created Successfully");
        }
        //[HttpPost]
        //[HttpPost("addSupplier")]
        //public async Task<IActionResult> Create([Bind("SupplierId,SupplierFN,SupplierLN,SupplierEmail,SupplierMobileNo,SupplierAddress,Country,Material,SupplierMsg")] Suppliers suppliers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = suppliers.SupplierEmail,
        //            Email = suppliers.SupplierEmail,
        //            PhoneNumber = suppliers.SupplierMobileNo
        //        };

        //        var result = await _userManager.CreateAsync(user, "supplier");
        //        if (result.Succeeded)
        //        {
        //            suppliers.User = user;

        //            _context.Add(suppliers);
        //            await _context.SaveChangesAsync();

        //            return Ok("Supplier created successfully");
        //        }
        //        else
        //        {
        //            var errors = result.Errors.Select(e => e.Description);
        //            return BadRequest(errors);
        //        }
        //    }
        //    return BadRequest(ModelState);
        //}



        //// GET: Suppliers/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Suppliers == null)
        //    {
        //        return NotFound();
        //    }

        //    var suppliers = await _context.Suppliers.FindAsync(id);
        //    if (suppliers == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(suppliers);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("SupplierId,SupplierFN,SupplierLN,SupplierEmail,SupplierMobileNo,SupplierAddress,Country,Material,SupplierMsg")] Suppliers suppliers)
        //{
        //    if (id != suppliers.SupplierId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(suppliers);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SuppliersExists(suppliers.SupplierId))
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
        //    return Ok(suppliers);
        //}

        //// GET: Suppliers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Suppliers == null)
        //    {
        //        return NotFound();
        //    }

        //    var suppliers = await _context.Suppliers
        //        .FirstOrDefaultAsync(m => m.SupplierId == id);
        //    if (suppliers == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(suppliers);
        //}

        //// POST: Suppliers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Suppliers == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Suppliers'  is null.");
        //    }
        //    var suppliers = await _context.Suppliers.FindAsync(id);
        //    if (suppliers != null)
        //    {
        //        _context.Suppliers.Remove(suppliers);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool SuppliersExists(int id)
        {
          return (_context.Suppliers?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}
