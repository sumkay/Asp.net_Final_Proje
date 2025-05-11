using Microsoft.AspNetCore.Mvc;
using Dis.Models;
using Dis.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

namespace Dis.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Review/
        [HttpGet]
        public IActionResult Index()
        {
            var reviews = _context.Reviews
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            return View(reviews);
        }

        // POST: /Review/
        [HttpPost]
        public IActionResult Index(Review model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                _context.Reviews.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var reviews = _context.Reviews
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            return View(reviews);
        }
    }
}
