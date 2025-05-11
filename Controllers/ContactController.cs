using Microsoft.AspNetCore.Mvc;
using Dis.Data;
using Dis.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Dis.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.CreatedAt = DateTime.Now;

                
                bool isSameTimeExists = _context.Appointments
                    .Any(a => a.AppointmentDate == appointment.AppointmentDate);
                if (isSameTimeExists)
                {
                    ModelState.AddModelError("", "Bu saat için zaten bir randevu mevcut. Lütfen başka bir saat seçin.");
                    return View();
                }

                
                string phoneToCheck = appointment.Patient.Phone.Trim();
                string emailToCheck = appointment.Patient.Email.Trim().ToLower();

                bool isPhoneExists = _context.Patients.Any(p => p.Phone == phoneToCheck);
                bool isEmailExists = _context.Patients.Any(p => p.Email.ToLower() == emailToCheck);

                if (isPhoneExists)
                {
                    ModelState.AddModelError("", "Bu telefon numarası ile zaten bir kayıt var.");
                    return View();
                }
                if (isEmailExists)
                {
                    ModelState.AddModelError("", "Bu e-posta ile zaten bir kayıt var.");
                    return View();
                }

                
                var existingPatient = _context.Patients
                    .FirstOrDefault(p => p.Email.ToLower() == emailToCheck);

                if (existingPatient == null)
                {
                    var newPatient = new Patient
                    {
                        Name = appointment.Patient.Name,
                        Email = emailToCheck,
                        Phone = phoneToCheck
                    };
                    _context.Patients.Add(newPatient);
                    _context.SaveChanges();

                    appointment.PatientId = newPatient.Id;
                }
                else
                {
                    appointment.PatientId = existingPatient.Id;
                }

                appointment.Patient = null;

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                ViewBag.Message = "Randevunuz Başarıyla Alındı";
            }
            return View();
        }

    }
}
