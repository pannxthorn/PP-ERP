using MediatR;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandDeleteCompany : IRequest<bool>
    {
        public int COMPANY_ID { get; set; }
    }
}
