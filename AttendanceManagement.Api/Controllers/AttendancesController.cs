using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Entities;
using AttendanceManagement.Api.Repositories;
using AttendanceManagement.Api.Utils;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AttendancesController(IAttendanceRepository repository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> Get(string id)
        {
            var attendance = await _repository.GetAsync(id);

            if (attendance == null)
            {
                return NotFound("Attendance entry does not exist");
            }

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);

            return Ok(attendanceDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AttendanceUpdateDto dtoModel)
        {
            if (id != dtoModel.Id)
            {
                return BadRequest();
            }

            var attendance = await _repository.GetAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.ExitTimestamp = CommonUtils.GetUtcTimestamp(dtoModel.ExitDateTime);
            await _repository.UpdateAsync(id, attendance);

            return NoContent();
        }
    }
}
