using MediatR;
using PP_ERP.DTO.Branch;

namespace PP_ERP.Application.Organization.Branch
{
    public class CommandUpdateBranch : IRequest<RESULT_BRANCH_DTO?>
    {
        public int BRANCH_ID { get; set; }
        public required PARAM_BRANCH_DTO Data { get; set; }
    }
}
