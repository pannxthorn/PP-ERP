using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.Branch;

namespace PP_ERP.Application.Organization.Branch
{
    public class CommandUpdateBranchHandler : IRequestHandler<CommandUpdateBranch, RESULT_BRANCH_DTO?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandUpdateBranchHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_BRANCH_DTO?> Handle(CommandUpdateBranch request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Branch.GetAsync(
                x => x.BRANCH_ID == request.BRANCH_ID && !x.IS_DELETE);

            if (entity == null) return null;

            var data = request.Data;

            entity.COMPANY_ID = data.COMPANY_ID;
            entity.BRANCH_CODE = data.BRANCH_CODE;
            entity.BRANCH_NAME = data.BRANCH_NAME;
            entity.PHONE = data.PHONE;
            entity.EMAIL = data.EMAIL;
            entity.LINE = data.LINE;
            entity.FACEBOOK = data.FACEBOOK;
            entity.COMMENT = data.COMMENT;
            entity.IS_HEADQUARTER = data.IS_HEADQUARTER;
            entity.IS_ACTIVE = data.IS_ACTIVE;
            entity.LAST_UPDATE_ID = 1; // TODO: replace with actual user id
            entity.LAST_UPDATE_DATE = DateTime.Now;

            _unitOfWork.Branch.Update(entity);
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
