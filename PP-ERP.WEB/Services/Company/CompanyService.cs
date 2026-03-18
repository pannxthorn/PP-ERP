using PP_ERP.DTO.BaseDTO;
using PP_ERP.DTO.Company;
using PP_ERP.WEB.Services.Base;

namespace PP_ERP.WEB.Services.Company
{
    public class CompanyService : BaseService
    {
        public CompanyService(RestCommon rest, IConfiguration configuration)
            : base(rest, configuration)
        {
        }

        public async Task<RESULT_REST_RESPONSE<List<RESULT_COMPANY_DTO>>> GetAllCompany()
        {
            return await Get<List<RESULT_COMPANY_DTO>>("api/company");
        }
    }
}
