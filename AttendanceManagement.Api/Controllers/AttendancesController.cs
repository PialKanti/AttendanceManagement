using AttendanceManagement.Api.Dtos;
using AttendanceManagement.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceRepository _repository;

        public AttendancesController(IAttendanceRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceCreateDto dtoModel)
        {
            _repository.Create(dtoModel);

            return Ok();
        }
    }
}
