using MediatR;
using PP_ERP.DTO.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.Company
{
    public class QueryGetAllCompany : IRequest<IEnumerable<RESULT_COMPANY_DTO>>
    {
    }
}
