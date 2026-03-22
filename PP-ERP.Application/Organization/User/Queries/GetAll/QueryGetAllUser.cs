using MediatR;
using PP_ERP.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.Organization.User
{
    public class QueryGetAllUser : IRequest<IEnumerable<RESULT_USER_DTO>>
    {
    }
}
