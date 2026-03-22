using PP_ERP.DTO.BaseDTO;
using PP_ERP.DTO.Branch;
using PP_ERP.WEB.Services.Base;

namespace PP_ERP.WEB.Services.Branch
{
    public class BranchService : BaseService
    {
        public BranchService(RestCommon rest, IConfiguration configuration)
            : base(rest, configuration)
        {
        }

        public async Task<RESULT_REST_RESPONSE<List<RESULT_BRANCH_DTO>>> GetAllBranch()
        {
            return await Get<List<RESULT_BRANCH_DTO>>("api/branch");
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_BRANCH_DTO>> GetBranchById(int id)
        {
            return await Get<RESULT_BRANCH_DTO>($"api/branch/{id}");
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_BRANCH_DTO>> CreateBranch(PARAM_BRANCH_DTO data)
        {
            return await Post<RESULT_BRANCH_DTO>("api/branch", data);
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_BRANCH_DTO>> UpdateBranch(int id, PARAM_BRANCH_DTO data)
        {
            return await Put<RESULT_BRANCH_DTO>($"api/branch/{id}", data);
        }

        public async Task<RESULT_REST_RESPONSE<bool>> DeleteBranch(int id)
        {
            return await Delete<bool>($"api/branch/{id}");
        }
    }
}
