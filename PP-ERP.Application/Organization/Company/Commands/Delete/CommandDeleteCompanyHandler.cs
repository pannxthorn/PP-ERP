using MediatR;
using PP_ERP.Application.UnitOfWork;

namespace PP_ERP.Application.Organization.Company
{
    public class CommandDeleteCompanyHandler : IRequestHandler<CommandDeleteCompany, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandDeleteCompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CommandDeleteCompany request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Company.GetAsync(
                x => x.COMPANY_ID == request.COMPANY_ID && !x.IS_DELETE);

            if (entity == null) return false;

            entity.IS_DELETE = true;
            entity.IS_ACTIVE = false;
            entity.LAST_UPDATE_ID = 1; // TODO: replace with actual user id
            entity.LAST_UPDATE_DATE = DateTime.Now;

            _unitOfWork.Company.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
