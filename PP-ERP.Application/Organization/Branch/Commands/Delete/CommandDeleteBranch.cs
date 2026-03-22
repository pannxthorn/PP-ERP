using MediatR;

namespace PP_ERP.Application.Organization.Branch
{
    public class CommandDeleteBranch : IRequest<bool>
    {
        public int BRANCH_ID { get; set; }
    }
}
