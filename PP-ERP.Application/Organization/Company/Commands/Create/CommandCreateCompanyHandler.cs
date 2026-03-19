using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.Domain.Entities;
using PP_ERP.DTO.Company;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandCreateCompanyHandler : IRequestHandler<CommandCreateCompany, RESULT_COMPANY_DTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandCreateCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RESULT_COMPANY_DTO> Handle(CommandCreateCompany request, CancellationToken cancellationToken)
        {
            var data = request.Data;

            var entity = new COMPANY
            {
                COMPANY_CODE = data.COMPANY_CODE,
                COMPANY_NAME = data.COMPANY_NAME,
                TAX_NO = data.TAX_NO,
                PHONE = data.PHONE,
                FAX = data.FAX,
                EMAIL = data.EMAIL,
                LINE = data.LINE,
                FACEBOOK = data.FACEBOOK,
                WEBSITE = data.WEBSITE,
                LOGO = data.LOGO,
                COMMENT = data.COMMENT,
                IS_ACTIVE = data.IS_ACTIVE,
                IS_DELETE = false,
                CREATED_BY_ID = 1, // TODO: replace with actual user id
                CREATED_DATE = DateTime.Now,
                LAST_UPDATE_ID = 1,
                LAST_UPDATE_DATE = DateTime.Now,
                ROW_UN = Guid.NewGuid()
            };

            await _unitOfWork.Company.AddAsync(entity);
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
