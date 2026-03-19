using MediatR;
using PP_ERP.DTO.Company;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandUpdateCompany : IRequest<RESULT_COMPANY_DTO?>
    {
        public int COMPANY_ID { get; set; }
        public required PARAM_COMPANY_DTO Data { get; set; }
    }
}
