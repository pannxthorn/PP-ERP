using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.User
{
    public class QueryGetAllUserHandler : IRequestHandler<QueryGetAllUser, IEnumerable<RESULT_USER_DTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public QueryGetAllUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RESULT_USER_DTO>> Handle(QueryGetAllUser request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.User.GetManyAsync<RESULT_USER_DTO>(
                    x => x.IS_ACTIVE && !x.IS_DELETE,
                    x => new RESULT_USER_DTO
                    {
                        USER_ID = x.USER_ID,
                        COMPANY_ID = x.COMPANY_ID,
                        COMPANY_NAME = x.COMPANY != null ? x.COMPANY.COMPANY_NAME : null,
                        BRANCH_ID = x.BRANCH_ID,
                        BRANCH_NAME = x.BRANCH != null ? x.BRANCH.BRANCH_NAME : null,
                        USERNAME = x.USERNAME,
                        COMMENT = x.COMMENT,
                        IS_ACTIVE = x.IS_ACTIVE,
                        IS_DELETE = x.IS_DELETE,
                        CREATED_BY_ID = x.CREATED_BY_ID,
                        CREATED_DATE = x.CREATED_DATE,
                        LAST_UPDATE_ID = x.LAST_UPDATE_ID,
                        LAST_UPDATE_DATE = x.LAST_UPDATE_DATE,
                        ROW_UN = x.ROW_UN
                    });

                return users;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving user data.", ex);
            }
        }
    }
}
