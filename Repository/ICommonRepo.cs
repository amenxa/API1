using ApiTest.Data.DTOS;
using ApiTest.Data;
using System.Linq.Expressions;

namespace ApiTest.Repository
{
    public interface ICommonRepo <T> 
    {
        public Task<List<T>> GetALL();
        public Task<T> GetById(Expression<Func<T, bool>> fillter, bool track = true);

        public Task<T> AddCategory(T record);

        public Task<bool> RemoveCategory(Expression<Func<T, bool>> fillter);

        public void Update(T record);
    }
}
