using DoctorOffice.Data;
using DoctorsOffice.Models;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Appointment = DoctorOffice.Models.Appointment;
using Doctor = DoctorOffice.Models.Doctor;
using Patient = DoctorOffice.Models.Patient;
using Userlog = DoctorsOffice.Models.Userlog;
using VisitMethod = DoctorOffice.Models.VisitMethod;

namespace DoctorOffice.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly OfficeDb _ctx;

        //Constructor for Dependancy Injection to connect to DB
        public RecordController(Data.OfficeDb context) => _ctx = context;



        #region Appointments
        [HttpGet]
        [Route("Appointment")]
        public IActionResult GetAppointment()
        {
            // Fetches a record where the Id matches the results of the query
            var appointments = _ctx.Appointments
                .Include(b => b.Doctor)
                .Include(b => b.Patient)
                .Include(b => b.VisitMethod)
                .ToList();

            if (appointments.Count == 0)
            {
                // Return a 200 (OK) response with an empty array
                return Ok(new List<Appointment>());
            }

            return Ok(appointments);
        }


        //Get BreakfastOrder by ID
        [HttpGet]
        [Route("Appointment/{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            // Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Appointments.Include(b => b.Doctor)
          .Include(b => b.Patient)
          .Include(b => b.VisitMethod)
          .FirstOrDefault(x => x.Id == id);
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }

        // creates New Apointment

        [HttpPost]
        [Route("Appointment")]
        public IActionResult CreateAppointment([FromBody] Appointment item)
        {
            _ctx.Appointments.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = item.Id }, item);
        }

        [HttpPut("Appointment/{Id}")]
        public IActionResult UpdateAppointment(int id, [FromBody] Appointment item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Appointments.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = item.Id }, item);
        }

        // deletes record from database
        [HttpDelete]
        [Route("Appointment/{id}")]
        public IActionResult DeleteAppointmentById(int id)
        {
            var pVeges = _ctx.Appointments.FirstOrDefault(x => x.Id == id);
            if (pVeges == null)
            {
                return NotFound();
            }

            _ctx.Appointments.Remove(pVeges);
            _ctx.SaveChanges();

            return Ok();
        }

        #endregion

        // The Patient Api
        #region Patient

        [HttpGet]
        [Route("Patient")]
        public IActionResult GetPatient()
        {
            // Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Patients.ToList();
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }


        //Get BreakfastOrder by ID
        [HttpGet]
        [Route("Patient/{id}")]
        public IActionResult GetPatientById(int id)
        {
            // Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Patients
          .FirstOrDefault(x => x.Id == id);
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }

        // creates New Patient Record

        [HttpPost]
        [Route("Patient")]
        public IActionResult CreatePatient([FromBody] Patient item)
        {
            _ctx.Patients.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetPatient), new { id = item.Id }, item);
        }

        // Update Patient by Id

        [HttpPut("Patient/{Id}")]
        public IActionResult UpdatePatient(int id, [FromBody] Patient item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Patients.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = item.Id }, item);
        }

        // deletes record from database
        [HttpDelete]
        [Route("Patient/{id}")]
        public IActionResult DeletePatientById(int id)
        {
            var patient = _ctx.Patients.FirstOrDefault(x => x.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            _ctx.Patients.Remove(patient);
            _ctx.SaveChanges();

            return Ok();
        }
        #endregion

        // The Doctor Api
        #region Doctor

        [HttpGet]
        [Route("Doctor")]
        public IActionResult GetDoctor()
        {
            // Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Doctors.ToList();
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }


        //Get BreakfastOrder by ID
        [HttpGet]
        [Route("Doctor/{id}")]
        public IActionResult GetDoctorById(int id)
        {
            // Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Doctors
          .FirstOrDefault(x => x.Id == id);
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }

        // creates New Patient Record

        [HttpPost]
        [Route("Doctor")]
        public IActionResult CreateDoctor([FromBody] Doctor item)
        {
            _ctx.Doctors.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetDoctor), new { id = item.Id }, item);
        }

        // Update Doctor by Id

        [HttpPut("Doctor/{Id}")]
        public IActionResult UpdateDoctor(int id, [FromBody] Doctor item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Doctors.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetDoctorById), new { id = item.Id }, item);
        }

        // deletes record from database
        [HttpDelete]
        [Route("Doctor/{id}")]
        public IActionResult DeleteDoctorById(int id)
        {
            var doctor = _ctx.Doctors.FirstOrDefault(x => x.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _ctx.Doctors.Remove(doctor);
            _ctx.SaveChanges();

            return Ok();
        }
        #endregion


        // the Visit Method Reason
        #region VisitMethod


        [HttpGet]
        [Route("VisitMethod")]
        public IActionResult GetVisitMethod()
        {
            var vMethods = _ctx.VisitMethods.ToList();
            if (vMethods == null)
            {
                return NotFound();
            }

            return Ok(vMethods);
        }

        [HttpGet]
        [Route("VisitMethod/{id}")]
        public IActionResult GetVisitMethodById(int id)
        {
            var vMethod = _ctx.VisitMethods.FirstOrDefault(x => x.Id == id);
            if (vMethod == null)
            {
                return NotFound();
            }

            return Ok(vMethod);
        }

        [HttpPost]
        [Route("VisitMethod")]
        public IActionResult CreateVisitMethod([FromBody] VisitMethod item)
        {
            _ctx.VisitMethods.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetVisitMethod), new { id = item.Id }, item);
        }

        [HttpPut("VisitMethod/{Id}")]
        public IActionResult UpdateVisitMethod(int id, [FromBody] VisitMethod item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.VisitMethods.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetVisitMethodById), new { id = item.Id }, item);
        }

        [HttpDelete]
        [Route("VisitMethod/{id}")]
        public IActionResult DeleteVisitMethodById(int id)
        {
            var vMethod = _ctx.VisitMethods.FirstOrDefault(x => x.Id == id);
            if (vMethod == null)
            {
                return NotFound();
            }

            _ctx.VisitMethods.Remove(vMethod);
            _ctx.SaveChanges();

            return Ok();
        }

        #endregion

        #region User

        [HttpGet]
        [Route("Userlog")]
        public IActionResult GetUserLogs()
        {
            var userLogs = _ctx.users.ToList();
            return Ok(userLogs);
        }

        [Route("{userId}")]
        [HttpGet]
        public IActionResult GetUserLogById(int userId)
        {
            var user = _ctx.users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("")]
        [HttpPost]
        public IActionResult CreateUserLog(Userlog user)
        {
            _ctx.users.Add(user);
            _ctx.SaveChanges();
            return Ok("User log created successfully");
        }

        [Route("{userId}")]
        [HttpPut]
        public IActionResult UpdateUserLog(int userId, User updatedUser)
        {
            var user = _ctx.users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = updatedUser.Username; // Assuming 'Name' is a property to update
                                                  // Update other properties as needed

            _ctx.SaveChanges();
            return Ok("User log updated successfully");
        }

        [Route("{userId}")]
        [HttpDelete]
        public IActionResult DeleteUserLog(int userId)
        {
            var user = _ctx.users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            _ctx.users.Remove(user);
            _ctx.SaveChanges();
            return Ok("User log deleted successfully");
        }



        #endregion
    }
}
