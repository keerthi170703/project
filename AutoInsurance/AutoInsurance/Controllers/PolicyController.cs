using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System;
using AutoInsurance.Models;

namespace AutoInsuranceWebApp.Controllers
{
    public class PolicyController : Controller
    {
        private readonly InsuranceDbContext _context;

        public PolicyController(InsuranceDbContext context)
        {
            _context = context;
        }

        // GET: /Policy
        public async Task<IActionResult> Index()
        {
            var policies = await _context.Policies.ToListAsync();
            return View(policies);
        }

        // GET: /Policy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Policy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Policy policy)
        {
            if (ModelState.IsValid)
            {
                _context.Policies.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        // GET: /Policy/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        // POST: /Policy/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Policy policy)
        {
            if (id != policy.PolicyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.PolicyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        // GET: /Policy/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        // POST: /Policy/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: /Policy/GetPolicyById
        public IActionResult GetPolicyById()
        {
            return View();
        }



        // POST: /Policy/GetPolicyById
        [HttpPost]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy = await _context.Policies.FirstOrDefaultAsync(p => p.PolicyId == id);

            if (policy == null)
            {
                ViewBag.Message = $"No policy found with ID = {id}.";
                return View();
            }

            return View(policy);
        }


        private bool PolicyExists(int id)
        {
            return _context.Policies.Any(e => e.PolicyId == id);
        }
    }
}