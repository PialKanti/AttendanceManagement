using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Dtos.Request;
using AttendanceManagement.Api.Responses.Error;
using AttendanceManagement.Api.Utils;
using AttendanceManagement.Application.Dtos;
using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttendanceManagement.Api.Controllers
{
    public class AttendancesController : BaseApiController
    {
        private readonly IAttendanceRepository<Attendance> _attendanceRepository;
        private readonly IUsersRepository<ApplicationUser> _usersRepository;
        private readonly IMapper _mapper;

        public AttendancesController(IAttendanceRepository<Attendance> attendanceRepository, IUsersRepository<ApplicationUser> usersRepository, IMapper mapper)
        {
            _attendanceRepository = attendanceRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceCreateRequest dtoModel)
        {
            var user = await _usersRepository.GetByUserNameAsync(dtoModel.Username);
            if (user == null)
            {
                List<ErrorResponse> errors = new()
                    { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) };
                return NotFound(new ErrorDto(errors));
            }

            var attendance = _mapper.Map<Attendance>(dtoModel);
            attendance.UserId = user.Id;

            var attendanceDto = await _attendanceRepository.CreateAsync(attendance);

            return CreatedAtAction(nameof(Get), new {id = attendanceDto.Id}, attendanceDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> Get(string id)
        {
            var attendance = await _attendanceRepository.GetAsync(id);

            if (attendance == null)
            {
                List<ErrorResponse> errors = new()
                    { new ErrorResponse(ErrorResponseType.AttendanceNotFound, (int)HttpStatusCode.NotFound) };
                return NotFound(new ErrorDto(errors));
            }

            var attendanceDto = _mapper.Map<AttendanceDto>(attendance);

            return Ok(attendanceDto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AttendanceUpdateRequest dtoModel)
        {
            if (id != dtoModel.Id)
            {
                return BadRequest();
            }

            var attendance = await _attendanceRepository.GetAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            attendance.ExitTimestamp = CommonUtils.GetUtcTimestamp(dtoModel.ExitDateTime);
            await _attendanceRepository.UpdateAsync(id, attendance);

            return NoContent();
        }
    }
}
