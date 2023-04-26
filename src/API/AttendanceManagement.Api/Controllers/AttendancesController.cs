using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Responses.Error;
using AttendanceManagement.Api.Utils;
using AttendanceManagement.Application.Dtos;
using AttendanceManagement.Application.Interfaces;
using AttendanceManagement.Domain.Entities;
using AttendanceManagement.Infrastructure.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AttendanceManagement.Api.Controllers
{
    public class AttendancesController : BaseApiController
    {
        private readonly IAttendanceRepository<Attendance> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public AttendancesController(IAttendanceRepository<Attendance> repository, UserManager<ApplicationUser> userManager, IMapper mapper)
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
                List<ErrorResponse> errors = new()
                    { new ErrorResponse(ErrorResponseType.UserNotFound, (int)HttpStatusCode.NotFound) };
                return NotFound(new ErrorDto(errors));
            }

            var attendance = _mapper.Map<Attendance>(dtoModel);

            var attendanceDto = await _repository.CreateAsync(attendance);
            //todo need to convert to AttendanceDto for sending to client

            return CreatedAtAction(nameof(Get), new {id = attendanceDto.Id}, attendanceDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> Get(string id)
        {
            var attendance = await _repository.GetAsync(id);

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
