using Dis.Data;
using Dis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Authorize]
public class ProfileController : Controller
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    public ProfileController(DbContextOptions<ApplicationDbContext> options)
    {
        _options = options;
    }
    public IActionResult Index()
    {
       
        using (var ctx = new ApplicationDbContext(_options))
        {

            var lst = ctx.Patients.ToList();


            return View(lst);
        }
    }

    [HttpGet]
    public IActionResult AddPatient()
    {      
        return View();
    }
    [HttpPost]
    public IActionResult AddPatient(Patient pt)
    {
        if (pt!=null)
        {
            using (var ctx = new ApplicationDbContext(_options))
            {
                ctx.Patients.Add(pt);
                ctx.SaveChanges();
            }
        }
            return RedirectToAction("Index");
       
    }
    public IActionResult EditPatient(int id)
    {

        using (var ctx = new ApplicationDbContext(_options))
        {
            var s = ctx.Patients.Find(id);
            return View(s);
        }            
    }
    [HttpPost]
    public IActionResult EditPatient(Patient pt)
    {
        if (pt != null)
        {
            using (var ctx = new ApplicationDbContext(_options))
            {
                ctx.Entry(pt).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        return RedirectToAction("Index");
    }
    public IActionResult DeletePatient(int id)
    {
        using (var ctx = new ApplicationDbContext(_options))
        {
            ctx.Appointments.RemoveRange(ctx.Appointments.Where(x => x.PatientId == id));
            ctx.Patients.Remove(ctx.Patients.Find(id));
            ctx.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
