using PP_ERP.Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using PP_ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_ERP.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region [Repository]

        IRepositories<BRANCH> Branch { get; }
        IRepositories<COMPANY> Company { get; }
        IRepositories<FLEX> Flex { get; }
        IRepositories<FLEX_ITEM> FlexItem { get; }
        IRepositories<SYS_USER> User { get; }

        #endregion [Repository]

        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task ExecuteInTransactionAsync(Func<Task> operation);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
    }
}
