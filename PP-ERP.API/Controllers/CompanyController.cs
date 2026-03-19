using MediatR;
using Microsoft.AspNetCore.Mvc;
using PP_ERP.Application.Organization.Company;
using PP_ERP.DTO.Company;

namespace PP_ERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RESULT_COMPANY_DTO>>> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new QueryGetAllCompany());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RESULT_COMPANY_DTO>> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new QueryGetCompanyById { COMPANY_ID = id });
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RESULT_COMPANY_DTO>> Create([FromBody] PARAM_COMPANY_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandCreateCompany { Data = data });
                return CreatedAtAction(nameof(GetById), new { id = result.COMPANY_ID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RESULT_COMPANY_DTO>> Update(int id, [FromBody] PARAM_COMPANY_DTO data)
        {
            try
            {
                var result = await _mediator.Send(new CommandUpdateCompany { COMPANY_ID = id, Data = data });
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
                var result = await _mediator.Send(new CommandDeleteCompany { COMPANY_ID = id });
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
