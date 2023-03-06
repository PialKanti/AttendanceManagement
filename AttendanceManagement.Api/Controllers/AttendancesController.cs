using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendancesController(IAttendanceRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceCreateDto dtoModel)
        {
            var user = await _userManager.FindByNameAsync(dtoModel.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var attendanceDto = await _repository.CreateAsync(dtoModel, user);

            return CreatedAtAction(nameof(Get), new {id = attendanceDto.Id}, attendanceDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> Get(string id)
        {
            var attendanceDto = await _repository.GetAsync(id);

            if (attendanceDto == null)
            {
                return NotFound("Attendance entry does not exist");
            }

            return Ok(attendanceDto);
        }
    }
}
