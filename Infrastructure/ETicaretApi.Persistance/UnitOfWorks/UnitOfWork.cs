using ETicaretApi.Application.Interfaces.Repositories;
using ETicaretApi.Application.Interfaces.UnitOfWorks;
using ETicaretApi.Domain.Common;
using ETicaretApi.Persistance.Context;
using ETicaretApi.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Persistance.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext )
        {
            this.dbContext = dbContext;
        }

        //Tek Satırlık return işlemlerinde aşağıdaki gibi kullanım yapılabilir.
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();

        public  int Save() =>  dbContext.SaveChanges();

        public async Task<int> SaveAsync() => await dbContext.SaveChangesAsync();

        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);

    }
}
