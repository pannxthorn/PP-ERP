using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP_ERP.Application.Organization.User;
using PP_ERP.DTO.User;

namespace PP_ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RESULT_USER_DTO>>> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new QueryGetAllUser());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RESULT_USER_DTO>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new QueryGetUserById { USER_ID = id });
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RESULT_USER_DTO>> Create([FromBody] PARAM_USER_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandCreateUser { Data = data });
                return CreatedAtAction(nameof(GetById), new { id = result.USER_ID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RESULT_USER_DTO>> Update(int id, [FromBody] PARAM_USER_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandUpdateUser { USER_ID = id, Data = data });
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new CommandDeleteUser { USER_ID = id });
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
