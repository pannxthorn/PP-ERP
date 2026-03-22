using MediatR;
using PP_ERP.Application.UnitOfWork;

namespace PP_ERP.Application.Organization.User
{
    public class CommandDeleteUserHandler : IRequestHandler<CommandDeleteUser, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommandDeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CommandDeleteUser request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.User.GetAsync(
                x => x.USER_ID == request.USER_ID && !x.IS_DELETE);

            if (entity == null) return false;

            entity.IS_DELETE = true;
            entity.IS_ACTIVE = false;
            entity.LAST_UPDATE_ID = 1; // TODO: replace with actual user id
            entity.LAST_UPDATE_DATE = DateTime.Now;

            _unitOfWork.User.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
