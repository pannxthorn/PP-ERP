using MediatR;
using PP_ERP.DTO.User;

namespace PP_ERP.Application.Organization.User
{
    public class CommandUpdateUser : IRequest<RESULT_USER_DTO?>
    {
        public int USER_ID { get; set; }
        public required PARAM_USER_DTO Data { get; set; }
    }
}
