using DentistDateManagement.Data;
using DentistDateManagement.Data.Entity;
using DentistDateManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DentistDateManagement.Controllers
{
    public class AppointmentController : Controller
    {
        private ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult GetAppointments()
        {
            var model = _context.Appointments.Include(x => x.User).Select(x => new AppointmentViewModel()
            {
                Id = x.Id,
                Dentist = x.User.Name + " " + x.User.Surname,
                PatientName = x.PatientName,
                PatientSurname = x.PatientSurrname,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Description = x.Description,
                Color = x.User.Color,
                UserId = x.UserId
            });
            return Json(model);
        }

        public JsonResult GetAppointmentsByDentist(string userId = "")
        {
            var model = _context.Appointments.Where(x => x.UserId == userId).Include(x => x.User).Select(x => new AppointmentViewModel()
            {
                Id = x.Id,
                Dentist = x.User.Name + " " + x.User.Surname,
                PatientName = x.PatientName,
                PatientSurname = x.PatientSurrname,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Description = x.Description,
                Color = x.User.Color,
                UserId = x.UserId
            });
            return Json(model);
        }

        [HttpPost]
        public JsonResult AddOrUpdateAppointment(AddOrUpdateAppointmentModel model)
        {
            try
            {
                //Ekleme
                if (model.Id == 0)
                {
                    Appointment entity = new Appointment()
                    {
                        CreatedDate = DateTime.Now,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        PatientName = model.PatientName,
                        PatientSurrname = model.PatientSurrname,
                        Description = model.Description,
                        UserId = model.UserId
                    };
                    _context.Add(entity);
                    _context.SaveChanges();

                }
                else //Güncelleme
                {
                    var entity = _context.Appointments.SingleOrDefault(x => x.Id == model.Id);
                    if (entity == null)
                    {
                        return Json("Güncellenecek Randevu Bulunamadı");
                    }
                    entity.UpdatedDate = DateTime.Now;
                    entity.StartDate = model.StartDate;
                    entity.EndDate = model.EndDate;
                    entity.PatientName = model.PatientName;
                    entity.PatientSurrname = model.PatientSurrname;
                    entity.Description = model.Description;
                    entity.UserId = model.UserId;

                    _context.Update(entity);
                    _context.SaveChanges();

                }
                return Json("200");
            }
            catch (Exception e) 
            { 
                return Json("400");
            }
        }

        public JsonResult DeleteAppointment(int id = 0)
        {
            var entity = _context.Appointments.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return Json("Kayıt Bulunamadı");
            }
            _context.Remove(entity);
            _context.SaveChanges();
            return Json("200");
        }
    }
}
