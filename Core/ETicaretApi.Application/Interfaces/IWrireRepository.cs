using ETicaretApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApi.Application.Interfaces
{
    public interface IWrireRepository<T> where T : class, IEntityBase, new()
    {
    }
}
