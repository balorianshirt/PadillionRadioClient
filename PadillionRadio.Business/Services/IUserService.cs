using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PadillionRadio.Business.Enums;
using PadillionRadio.Business.Models;

namespace PadillionRadio.Business.Services
{
    public interface IUserService
    {
        Task<List<UserModel>> GetList();

        Task<UserModel> Get(long id);

        Task<UserModel> Create(UserModel model);

        Task<bool> Update(UserModel model);

        Task<UserModel> Delete(long id);

        UserModel [] Search(string searchString, IQueryable<UserModel> query, SearchEnum searchEnum);
        
    }
}