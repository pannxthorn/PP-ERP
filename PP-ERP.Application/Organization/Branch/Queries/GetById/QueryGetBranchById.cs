using MediatR;
using PP_ERP.DTO.Branch;

namespace PP_ERP.Application.Organization.Branch
{
    public class QueryGetBranchById : IRequest<RESULT_BRANCH_DTO?>
    {
        public int BRANCH_ID { get; set; }
    }
}
