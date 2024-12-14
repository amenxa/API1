using ApiTest.Data;
using ApiTest.Data.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Repository
{
    public interface ICategoryRepo
    {
        public Task<List<Category>> GetALL();
        public Task<Category> GetById(int id , bool track= true);

        public Task<Category> AddCategory( Category category );

        public Task<bool> UpdateCategory( CategoryDTO category);

        public Task<bool> RemoveCategory( int id );

        public void patch(Category category);
    }
}
