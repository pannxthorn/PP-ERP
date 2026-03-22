using MediatR;
using PP_ERP.Application.UnitOfWork;

namespace PP_ERP.Application.Organization.Branch
{
    public class CommandDeleteBranchHandler : IRequestHandler<CommandDeleteBranch, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandDeleteBranchHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CommandDeleteBranch request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Branch.GetAsync(
                x => x.BRANCH_ID == request.BRANCH_ID && !x.IS_DELETE);

            if (entity == null) return false;

            entity.IS_DELETE = true;
            entity.IS_ACTIVE = false;
            entity.LAST_UPDATE_ID = 1; // TODO: replace with actual user id
            entity.LAST_UPDATE_DATE = DateTime.Now;

            _unitOfWork.Branch.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
