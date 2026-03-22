using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP_ERP.Application.Organization.Branch;
using PP_ERP.DTO.Branch;

namespace PP_ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RESULT_BRANCH_DTO>>> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new QueryGetAllBranch());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RESULT_BRANCH_DTO>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new QueryGetBranchById { BRANCH_ID = id });
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RESULT_BRANCH_DTO>> Create([FromBody] PARAM_BRANCH_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandCreateBranch { Data = data });
                return CreatedAtAction(nameof(GetById), new { id = result.BRANCH_ID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RESULT_BRANCH_DTO>> Update(int id, [FromBody] PARAM_BRANCH_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandUpdateBranch { BRANCH_ID = id, Data = data });
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
                var result = await _mediator.Send(new CommandDeleteBranch { BRANCH_ID = id });
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
