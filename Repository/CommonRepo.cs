using ApiTest.Data;
using ApiTest.Data.DTOS;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace ApiTest.Repository
{
    public class CommonRepo <T> : ICommonRepo <T> where T : class
    {
        private readonly AppDbContext db;
        private readonly DbSet<T> dbset;
        public CommonRepo(AppDbContext db)
        {
            this.db = db;
            this.dbset = db.Set<T>();
        }

        public async Task<List<T>> GetALL()
        { return await dbset.ToListAsync(); }

        public async Task<T> GetById(Expression<Func<T, bool>> fillter, bool track = true)
        {
            if (track) return await dbset.FirstOrDefaultAsync(fillter);
            else return await dbset.AsNoTracking().FirstOrDefaultAsync(fillter);
        }

        public async Task<T> AddCategory(T record) // without tracking 
        {
            await dbset.AddAsync(record);
            await db.SaveChangesAsync();
            return record;
        }

        public async Task<bool> RemoveCategory(Expression<Func<T, bool>> fillter)
        {
            T c = await GetById(fillter);
            if (c == null) return false;
            dbset.Remove(c);
            await db.SaveChangesAsync();
            return true;
        }

        public  void Update(T record)
        {
            dbset.Update(record);
             db.SaveChanges();
        }
    }
}
