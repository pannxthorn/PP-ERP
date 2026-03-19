using MediatR;
using PP_ERP.DTO.Company;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandCreateCompany : IRequest<RESULT_COMPANY_DTO>
    {
        public required PARAM_COMPANY_DTO Data { get; set; }
    }
}
