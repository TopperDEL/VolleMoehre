using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VolleMoehre.Contracts.Interfaces
{
    public interface IDBAdapter
    {
        Task<bool> SaveAsync<T>(T objectToSave);
        Task<bool> DeleteAsync<T>(string Id);
        Task<T> GetAsync<T>(string Id);
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> whereClause);

        [Obsolete("Nicht mehr notwendig - entfernen")]
        object GetUnderlyingSession();
    }
}
