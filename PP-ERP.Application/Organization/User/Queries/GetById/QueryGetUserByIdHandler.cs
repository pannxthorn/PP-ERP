using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.User;

namespace PP_ERP.Application.Organization.User
{
    public class QueryGetUserByIdHandler : IRequestHandler<QueryGetUserById, RESULT_USER_DTO?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public QueryGetUserByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_USER_DTO?> Handle(QueryGetUserById request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.User.GetAsync(
                x => x.USER_ID == request.USER_ID && x.IS_ACTIVE && !x.IS_DELETE);

            if (entity == null) return null;

            return new RESULT_USER_DTO
            {
                USER_ID = entity.USER_ID,
                COMPANY_ID = entity.COMPANY_ID,
                BRANCH_ID = entity.BRANCH_ID,
                USERNAME = entity.USERNAME,
                COMMENT = entity.COMMENT,
                IS_ACTIVE = entity.IS_ACTIVE,
                IS_DELETE = entity.IS_DELETE,
                CREATED_BY_ID = entity.CREATED_BY_ID,
                CREATED_DATE = entity.CREATED_DATE,
                LAST_UPDATE_ID = entity.LAST_UPDATE_ID,
                LAST_UPDATE_DATE = entity.LAST_UPDATE_DATE,
                ROW_UN = entity.ROW_UN
            };
        }
    }
}
