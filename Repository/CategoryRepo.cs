using ApiTest.Data;
using ApiTest.Data.DTOS;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApiTest.Repository
{
    public class CategoryRepo : ICategoryRepo 
    {
        private readonly AppDbContext db;
        public CategoryRepo(AppDbContext db)  
        { 
         this.db = db;
        }
        public async Task<List<Category>> GetALL()
        { return await db.Categories.ToListAsync(); }
        public async Task<Category> GetById(int id , bool track = true ) 
        {
           if (track) return await db.Categories.FirstOrDefaultAsync(x => x.Id == id);
           else return await db.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> AddCategory(Category category) // without tracking 
        {
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategory(CategoryDTO categoryDTO) // without tracking 
        {

            Category c = await  GetById(categoryDTO.Id);
            if (c== null) return false;
            c.Name = categoryDTO.Name;
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            Category c = await GetById(id);
            if (c== null) return false;
            db.Categories.Remove(c);
            await db.SaveChangesAsync();
            return true;
        }

        public async void patch(Category c)
        {
            db.Categories.Update(c);
            await db.SaveChangesAsync();
        }
    }
}
