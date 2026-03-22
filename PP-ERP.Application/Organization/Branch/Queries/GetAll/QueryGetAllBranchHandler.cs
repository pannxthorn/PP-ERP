using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.Branch
{
    public class QueryGetAllBranchHandler : IRequestHandler<QueryGetAllBranch, IEnumerable<RESULT_BRANCH_DTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public QueryGetAllBranchHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RESULT_BRANCH_DTO>> Handle(QueryGetAllBranch request, CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _unitOfWork.Branch.GetManyAsync<RESULT_BRANCH_DTO>(
                    x => x.IS_ACTIVE && !x.IS_DELETE,
                    x => new RESULT_BRANCH_DTO
                    {
                        BRANCH_ID = x.BRANCH_ID,
                        COMPANY_ID = x.COMPANY_ID,
                        BRANCH_CODE = x.BRANCH_CODE,
                        BRANCH_NAME = x.BRANCH_NAME,
                        PHONE = x.PHONE,
                        EMAIL = x.EMAIL,
                        LINE = x.LINE,
                        FACEBOOK = x.FACEBOOK,
                        COMMENT = x.COMMENT,
                        IS_HEADQUARTER = x.IS_HEADQUARTER,
                        IS_ACTIVE = x.IS_ACTIVE,
                        IS_DELETE = x.IS_DELETE,
                        CREATED_BY_ID = x.CREATED_BY_ID,
                        CREATED_DATE = x.CREATED_DATE,
                        LAST_UPDATE_ID = x.LAST_UPDATE_ID,
                        LAST_UPDATE_DATE = x.LAST_UPDATE_DATE,
                        ROW_UN = x.ROW_UN
                    });

                return branches;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving branch data.", ex);
            }
        }
    }
}
