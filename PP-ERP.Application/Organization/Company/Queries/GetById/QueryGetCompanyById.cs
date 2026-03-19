using MediatR;
using PP_ERP.DTO.Company;

namespace PP_ERP.Application.Organization.Company
{
    public class QueryGetCompanyById : IRequest<RESULT_COMPANY_DTO?>
    {
        public int COMPANY_ID { get; set; }
    }
}
