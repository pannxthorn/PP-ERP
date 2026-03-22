using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.Domain.Entities;
using PP_ERP.DTO.Branch;

namespace PP_ERP.Application.Organization.Branch
{
    public class CommandCreateBranchHandler : IRequestHandler<CommandCreateBranch, RESULT_BRANCH_DTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandCreateBranchHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_BRANCH_DTO> Handle(CommandCreateBranch request, CancellationToken cancellationToken)
        {
            var data = request.Data;

            var entity = new BRANCH
            {
                COMPANY_ID = data.COMPANY_ID,
                BRANCH_CODE = data.BRANCH_CODE,
                BRANCH_NAME = data.BRANCH_NAME,
                PHONE = data.PHONE,
                EMAIL = data.EMAIL,
                LINE = data.LINE,
                FACEBOOK = data.FACEBOOK,
                COMMENT = data.COMMENT,
                IS_HEADQUARTER = data.IS_HEADQUARTER,
                IS_ACTIVE = data.IS_ACTIVE,
                IS_DELETE = false,
                CREATED_BY_ID = 1, // TODO: replace with actual user id
                CREATED_DATE = DateTime.Now,
                LAST_UPDATE_ID = 1,
                LAST_UPDATE_DATE = DateTime.Now,
                ROW_UN = Guid.NewGuid()
            };

            await _unitOfWork.Branch.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return new RESULT_BRANCH_DTO
            {
                BRANCH_ID = entity.BRANCH_ID,
                COMPANY_ID = entity.COMPANY_ID,
                BRANCH_CODE = entity.BRANCH_CODE,
                BRANCH_NAME = entity.BRANCH_NAME,
                PHONE = entity.PHONE,
                EMAIL = entity.EMAIL,
                LINE = entity.LINE,
                FACEBOOK = entity.FACEBOOK,
                COMMENT = entity.COMMENT,
                IS_HEADQUARTER = entity.IS_HEADQUARTER,
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
