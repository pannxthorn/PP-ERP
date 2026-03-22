using MediatR;

namespace PP_ERP.Application.Organization.User
{
    public class CommandDeleteUser : IRequest<bool>
    {
        public int USER_ID { get; set; }
    }
}
