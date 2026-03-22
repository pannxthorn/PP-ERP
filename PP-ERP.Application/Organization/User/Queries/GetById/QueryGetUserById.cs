using MediatR;
using PP_ERP.DTO.User;

namespace PP_ERP.Application.Organization.User
{
    public class QueryGetUserById : IRequest<RESULT_USER_DTO?>
    {
        public int USER_ID { get; set; }
    }
}
