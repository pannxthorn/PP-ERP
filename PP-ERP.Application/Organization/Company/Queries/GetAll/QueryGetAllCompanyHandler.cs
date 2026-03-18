using MediatR;
using PP_ERP.Application.UnitOfWork;
using PP_ERP.DTO.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.Company
{
    public class QueryGetAllCompanyHandler : IRequestHandler<QueryGetAllCompany, IEnumerable<RESULT_COMPANY_DTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public QueryGetAllCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RESULT_COMPANY_DTO>> Handle(QueryGetAllCompany request, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await _unitOfWork.Company.GetManyAsync<RESULT_COMPANY_DTO>(
                    x => x.IS_ACTIVE && !x.IS_DELETE,
                    x => new RESULT_COMPANY_DTO
                    {
                        COMPANY_ID = x.COMPANY_ID,
                        COMPANY_CODE = x.COMPANY_CODE,
                        COMPANY_NAME = x.COMPANY_NAME,
                        TAX_NO = x.TAX_NO,
                        PHONE = x.PHONE,
                        FAX = x.FAX,
                        EMAIL = x.EMAIL,
                        LINE = x.LINE,
                        FACEBOOK = x.FACEBOOK,
                        WEBSITE = x.WEBSITE,
                        LOGO = x.LOGO,
                        COMMENT = x.COMMENT,
                        IS_ACTIVE = x.IS_ACTIVE,
                        IS_DELETE = x.IS_DELETE,
                        CREATED_BY_ID = x.CREATED_BY_ID,
                        CREATED_DATE = x.CREATED_DATE,
                        LAST_UPDATE_ID = x.LAST_UPDATE_ID,
                        LAST_UPDATE_DATE = x.LAST_UPDATE_DATE,
                        ROW_UN = x.ROW_UN
                    });

                return companies;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving company data.", ex);
            }
        }
    }
}
