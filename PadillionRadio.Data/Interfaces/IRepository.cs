using System.Linq;
using System.Threading.Tasks;

namespace PadillionRadio.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T[]> GetItems();

        Task<T> GetItem(long id);

        Task Create(T item);

        void Update(T item);

        void Delete(T item);

        void Detatch(T item);

    }
}