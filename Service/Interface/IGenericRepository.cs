using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<string> AddUpdate(T entity, string userName);
        public Task Delete(int id);
        public T GetById(int Id, string user);
        public Task<List<T>> GetAll(int PageNo, int Perpage, string SearchKeyword);
    }
}
 