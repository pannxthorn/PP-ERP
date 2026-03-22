using MediatR;
using PP_ERP.DTO.User;

namespace PP_ERP.Application.Organization.User
{
    public class CommandCreateUser : IRequest<RESULT_USER_DTO>
    {
        public required PARAM_USER_DTO Data { get; set; }
    }
}
