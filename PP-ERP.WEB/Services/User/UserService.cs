using PP_ERP.DTO.BaseDTO;
using PP_ERP.DTO.User;
using PP_ERP.WEB.Services.Base;

namespace PP_ERP.WEB.Services.User
{
    public class UserService : BaseService
    {
        public UserService(RestCommon rest, IConfiguration configuration)
            : base(rest, configuration)
        {
        }

        public async Task<RESULT_REST_RESPONSE<List<RESULT_USER_DTO>>> GetAllUser()
        {
            return await Get<List<RESULT_USER_DTO>>("api/user");
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_USER_DTO>> GetUserById(int id)
        {
            return await Get<RESULT_USER_DTO>($"api/user/{id}");
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_USER_DTO>> CreateUser(PARAM_USER_DTO data)
        {
            return await Post<RESULT_USER_DTO>("api/user", data);
        }

        public async Task<RESULT_REST_RESPONSE<RESULT_USER_DTO>> UpdateUser(int id, PARAM_USER_DTO data)
        {
            return await Put<RESULT_USER_DTO>($"api/user/{id}", data);
        }

        public async Task<RESULT_REST_RESPONSE<bool>> DeleteUser(int id)
        {
            return await Delete<bool>($"api/user/{id}");
        }
    }
}
