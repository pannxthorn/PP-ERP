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
    }
}
