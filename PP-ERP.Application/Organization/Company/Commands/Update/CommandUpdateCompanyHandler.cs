using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.Company;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandUpdateCompanyHandler : IRequestHandler<CommandUpdateCompany, RESULT_COMPANY_DTO?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandUpdateCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_COMPANY_DTO?> Handle(CommandUpdateCompany request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Company.GetAsync(
                x => x.COMPANY_ID == request.COMPANY_ID && !x.IS_DELETE);

            if (entity == null) return null;

            var data = request.Data;

            entity.COMPANY_CODE = data.COMPANY_CODE;
            entity.COMPANY_NAME = data.COMPANY_NAME;
            entity.TAX_NO = data.TAX_NO;
            entity.PHONE = data.PHONE;
            entity.FAX = data.FAX;
            entity.EMAIL = data.EMAIL;
            entity.LINE = data.LINE;
            entity.FACEBOOK = data.FACEBOOK;
            entity.WEBSITE = data.WEBSITE;
            entity.LOGO = data.LOGO;
            entity.COMMENT = data.COMMENT;
            entity.IS_ACTIVE = data.IS_ACTIVE;
            entity.LAST_UPDATE_ID = 1; // TODO: replace with actual user id
            entity.LAST_UPDATE_DATE = DateTime.Now;

            _unitOfWork.Company.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return new RESULT_COMPANY_DTO
            {
                COMPANY_ID = entity.COMPANY_ID,
                COMPANY_CODE = entity.COMPANY_CODE,
                COMPANY_NAME = entity.COMPANY_NAME,
                TAX_NO = entity.TAX_NO,
                PHONE = entity.PHONE,
                FAX = entity.FAX,
                EMAIL = entity.EMAIL,
                LINE = entity.LINE,
                FACEBOOK = entity.FACEBOOK,
                WEBSITE = entity.WEBSITE,
                LOGO = entity.LOGO,
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
