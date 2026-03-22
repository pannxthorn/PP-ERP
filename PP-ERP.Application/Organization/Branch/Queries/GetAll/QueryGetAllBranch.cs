using MediatR;
using PP_ERP.DTO.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.Branch
{
    public class QueryGetAllBranch : IRequest<IEnumerable<RESULT_BRANCH_DTO>>
    {
    }
}
