using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectS.Data;
using ProjectS.Models;
using ProjectS.Models.Entities;
using System.Globalization;

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
        //private static List<DateTime> availableTimes = new List<DateTime>
        //{
        //    new DateTime(2025,1,3,9,0,0),
        //    new DateTime(2025,1,3,10,0,0),
        //    new DateTime(2025,1,3,11,0,0),
        //    new DateTime(2025,1,3,12,0,0),
        //    new DateTime(2025,1,3,13,0,0),
        //};
        [HttpGet]
        public async Task<IActionResult> GetAllAppointments()
        {
            var allAppointments =await dbContext.Appointments.ToListAsync();
            return  Ok(allAppointments);
        }
        [HttpGet]
        [Route("{AppointmentId}")] 
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await dbContext.Appointments.FindAsync(id);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            return Ok("appointment found");
        }
        //Get the available time 
        [HttpGet("availableTimes/{date}")]
        public async Task<ActionResult<IEnumerable<string>>> GetAvailableTimes(string date)
        {
            if(!DateTime.TryParseExact(date, "yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime parsedDate))
            {
                return BadRequest("Invalid date format");
            }
        var workingStartTime = parsedDate.Date.AddHours(9);
        var workingEndTime = parsedDate.Date.AddHours(14);
        var appointmentDuration = TimeSpan.FromHours(1);

        var appointments = await dbContext.Appointments.Where(a => a.StartDate.Date == parsedDate.Date).ToListAsync();
        
        var availableSLots = new List<string>();

            for(var timeSlot = workingStartTime; timeSlot < workingEndTime; timeSlot = timeSlot.Add(appointmentDuration))
            {
                var slotEndTime = timeSlot.Add(appointmentDuration);
                bool isSlotAvailable = !appointments.Any(a => timeSlot < a.EndDate && timeSlot.Add(appointmentDuration) > a.StartDate);

                if (isSlotAvailable)
                {
                    availableSLots.Add(timeSlot.ToString("HH:mm"));
                }
            }
        
            
            return Ok(availableSLots);
        }
        [HttpPost]
        public async Task<IActionResult> AddAppointment(AddAppointmentDto addAppointmentDto)
        {
            bool isSlotAvailable = !await dbContext.Appointments.AnyAsync(a=>a.StartDate < addAppointmentDto.EndDate && a.EndDate > addAppointmentDto.StartDate);

            if (!isSlotAvailable)
            {
                return BadRequest("Slot is not available");
            }
            var appointmentEntity = new Appointment()
            {
                ServiceType = addAppointmentDto.ServiceType,
                CustomerId = addAppointmentDto.CustomerId,
                StartDate = addAppointmentDto.StartDate,
                EndDate = addAppointmentDto.EndDate

            };
            
            
            dbContext.Appointments.Add(appointmentEntity);
            await dbContext.SaveChangesAsync();
            return Ok("appointment added");
        }
        [HttpPut]
        [Route("{AppointmentId}")]
        public async Task<IActionResult> UpdateAppointment(int AppointmentId, UpdateAppointmentDto updateAppointmentDto)
        {
            var appointment = dbContext.Appointments.Find(AppointmentId);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            appointment.ServiceType = updateAppointmentDto.ServiceType;
            appointment.CustomerId = updateAppointmentDto.CustomerId;
            appointment.StartDate = updateAppointmentDto.StartDate;
            appointment.EndDate = updateAppointmentDto.EndDate;
            await dbContext.SaveChangesAsync();
            return Ok("appointment updated");

        }
        [HttpDelete]
        [Route("{AppointmentId}")] 
        public async Task<IActionResult> DeleteAppointment(int AppointmentId)
        {
            var appointment = dbContext.Appointments.Find(AppointmentId);
            if (appointment is null)
            {
                return NotFound("Invalid Appointment");
            }
            dbContext.Appointments.Remove(appointment);
            await dbContext.SaveChangesAsync();
            return Ok("appointment removed");
        }
    }
}
