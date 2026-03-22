using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.Domain.Entities;
using PP_ERP.DTO.User;

namespace PP_ERP.Application.Organization.User
{
    public class CommandCreateUserHandler : IRequestHandler<CommandCreateUser, RESULT_USER_DTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandCreateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_USER_DTO> Handle(CommandCreateUser request, CancellationToken cancellationToken)
        {
            var data = request.Data;

            var entity = new SYS_USER
            {
                COMPANY_ID = data.COMPANY_ID,
                BRANCH_ID = data.BRANCH_ID,
                USERNAME = data.USERNAME,
                PASSWORD = data.PASSWORD ?? string.Empty,
                COMMENT = data.COMMENT,
                IS_ACTIVE = data.IS_ACTIVE,
                IS_DELETE = false,
                CREATED_BY_ID = 1, // TODO: replace with actual user id
                CREATED_DATE = DateTime.Now,
                LAST_UPDATE_ID = 1,
                LAST_UPDATE_DATE = DateTime.Now,
                ROW_UN = Guid.NewGuid()
            };

            await _unitOfWork.User.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

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
