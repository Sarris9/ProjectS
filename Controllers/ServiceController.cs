using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectS.Data;
using ProjectS.Models;
using ProjectS.Models.Entities;

namespace ProjectS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ProjectDbContext dbContext;

        public ServiceController(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllServices()
        {
            var allServices = dbContext.Services.ToList();
            return Ok(allServices);
        }
        [HttpGet]
        [Route("{ServiceId}")]
        public IActionResult GetServiceById(int ServiceId)
        {
            var service =dbContext.Services.Find(ServiceId);
            if (service is null)
            {
                return NotFound("Invalid Service");
            }

            return Ok("service found");
        }
        [HttpPost]
        public IActionResult AddService(AddServiceDto addServiceDto)
        {
            var serviceEntity = new Service()
            {
                
                Mark = addServiceDto.Mark,
                Type = addServiceDto.Type,
                PlateNumber = addServiceDto.PlateNumber,
                Kw = addServiceDto.Kw,
                DateOfService = addServiceDto.DateOfService,
                ServiceType = addServiceDto.ServiceType
            };
            dbContext.Services.Add(serviceEntity);
            dbContext.SaveChanges();
            return Ok("service added");
        }
        [HttpPut]
        [Route("{ServiceId}")]
        public IActionResult UpdateService(int ServiceId, UpdateServiceDto updateServiceDto)
        {
            var service = dbContext.Services.Find(ServiceId);
            if (service is null)
            {
                return NotFound("Invalid Service");
            }
            service.Mark = updateServiceDto.Mark;
            service.Type = updateServiceDto.Type;
            service.PlateNumber = updateServiceDto.PlateNumber;
            service.Kw = updateServiceDto.Kw;
            service.DateOfService = updateServiceDto.DateOfService;
            service.ServiceType = updateServiceDto.ServiceType;
            dbContext.SaveChanges();
            return Ok("service updated");
        }
        [HttpDelete]
        [Route("{ServiceId}")]
        public IActionResult DeleteService(int ServiceId)
        {
            var service = dbContext.Services.Find(ServiceId);
            if (service is null)
            {
                return NotFound("Invalid Service");
            }
            dbContext.Services.Remove(service);
            dbContext.SaveChanges();
            return Ok("service removed");
        }
    }
}
