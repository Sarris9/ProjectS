using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectS.Data;
using ProjectS.Models;
using ProjectS.Models.Entities;

namespace ProjectS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ProjectDbContext dbContext;

        public AppointmentController(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Create a fix start time
        private readonly List<DateTime> availableTimes = new List<DateTime>
        {
            new DateTime(2025,1,3,9,0,0),
            new DateTime(2025,1,3,10,0,0),
            new DateTime(2025,1,3,11,0,0),
            new DateTime(2025,1,3,12,0,0),
            new DateTime(2025,1,3,13,0,0),
        };
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var allAppointments = dbContext.Appointments.ToList();
            return Ok(allAppointments);
        }
        [HttpGet]
        [Route("{AppointmentId}")] 
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = dbContext.Appointments.Find(id);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            return Ok("appointment found");
        }
        //Get the available time 
        [HttpGet("availableTimes")]
        public IActionResult GetAvailableTimes([FromQuery]DateTime startDate, [FromQuery] int days =7)
        {
            var availableTimeSlots = new List<DateTime>();

            for (var i =0;i<days;i++)
            {
                var currentDate = startDate.AddDays(i);
                foreach (var time in availableTimes)
                {

                    var dateTime = currentDate.Date.AddHours(time.Hour).AddMinutes(time.Minute);
                    availableTimeSlots.Add(dateTime);
                }
            }
            return Ok(availableTimes);
        }
        [HttpPost]
        public IActionResult AddAppointment(AddAppointmentDto addAppointmentDto)
        {
            //check if the start date is available
            if(!availableTimes.Any(slot=>slot.Hour == addAppointmentDto.StartDate.Hour && slot.Minute == addAppointmentDto.StartDate.Minute))
            {
                return BadRequest("Invalid Start Date");
            }
            //make the date last 1 hour
            addAppointmentDto.EndDate = addAppointmentDto.StartDate.AddHours(1);
            var appointmentEntity = new Appointment()
            {
                ServiceType = addAppointmentDto.ServiceType,
                CustomerId = addAppointmentDto.CustomerId,
                StartDate = addAppointmentDto.StartDate,
                //EndDate = addAppointmentDto.EndDate

            };
            dbContext.Appointments.Add(appointmentEntity);
            dbContext.SaveChanges();
            return Ok("appointment added");
        }
        [HttpPut]
        [Route("{AppointmentId}")]
        public IActionResult UpdateAppointment(int AppointmentId, UpdateAppointmentDto updateAppointmentDto)
        {
            var appointment = dbContext.Appointments.Find(AppointmentId);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            appointment.ServiceType = updateAppointmentDto.ServiceType;
            appointment.CustomerId = updateAppointmentDto.CustomerId;
            appointment.StartDate = updateAppointmentDto.StartDate;
            //appointment.EndDate = updateAppointmentDto.EndDate;
            dbContext.SaveChanges();
            return Ok("appointment updated");

        }
        [HttpDelete]
        [Route("{AppointmentId}")] 
        public IActionResult DeleteAppointment(int AppointmentId)
        {
            var appointment = dbContext.Appointments.Find(AppointmentId);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            dbContext.Appointments.Remove(appointment);
            dbContext.SaveChanges();
            return Ok("appointment removed");
        }
    }
}
