using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimetableAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {

        TimetableRepository repository = new TimetableRepository();

        // GET api/values
        [HttpGet("all")]
        public ActionResult<IEnumerable<Module>> GetAll()
        {
            return repository.getAllModules();
        }

        [HttpGet("moduleByModuleName/{moduleName}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<Module> GetModuleByModuleName(string moduleName)
        {
            Module module = repository.getModuleByModuleName(moduleName);
            if (module == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(repository.getModuleByModuleName(moduleName));
            }
        }

        [HttpGet("moduleByLecturerName/{lecturerName}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<Module> GetModuleByLecturerName(string lecturerName)
        {
            Module module = repository.getModuleByLecturerName(lecturerName);
            if (module == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(repository.getModuleByLecturerName(lecturerName));
            }
        }

        [HttpGet("moduleDescription/{moduleName}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<Object> getModuleDescriptionByModuleName(string moduleName)
        {
            var moduleDescription = repository.getModuleDescriptionByModuleName(moduleName);
            if (moduleDescription == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(moduleDescription);
            }
        }


        [HttpGet("moduleByTimeslot/{day},{time},{amPm}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<Object> getModuleByTimeslot(string day, string time, string amPm)
        {
            var moduleTime = repository.getModuleByTimeslot(day,time,amPm);
            if (moduleTime == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(moduleTime);
            }
        }

        [HttpGet("getTimeslotsByDay/{day}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<List<Object>> getTimeslotsByDay(string day)
        {
            var timeSlots = repository.getTimeslotsByDay(day);
            if (timeSlots == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(timeSlots);
            }
        }

        [HttpGet("timeSlotByRoomNumber/{roomNumber}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public ActionResult<List<Object>> getTimeslotsByRoomNumber(int roomNumber)
        {
            var timeSlots = repository.getTimeslotsByRoomNumber(roomNumber);
            if (timeSlots == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(timeSlots);
            }
        }
    }
}
