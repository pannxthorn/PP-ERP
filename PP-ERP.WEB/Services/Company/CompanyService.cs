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

        public async Task<RESULT_REST_RESPONSE<RESULT_COMPANY_DTO>> GetCompanyById(int id)
        {
            return await Get<RESULT_COMPANY_DTO>($"api/company/{id}");
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_COMPANY_DTO>> CreateCompany(PARAM_COMPANY_DTO data)
        {
            return await Post<RESULT_COMPANY_DTO>("api/company", data);
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_COMPANY_DTO>> UpdateCompany(int id, PARAM_COMPANY_DTO data)
        {
            return await Put<RESULT_COMPANY_DTO>($"api/company/{id}", data);
        }

        public async Task<RESULT_REST_RESPONSE<bool>> DeleteCompany(int id)
        {
            return await Delete<bool>($"api/company/{id}");
        }
    }
}
