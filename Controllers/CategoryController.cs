using ApiTest.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public CategoryController(AppDbContext db)
        {

            this._db = db;

        }

        private readonly AppDbContext _db;
        [HttpGet]
        public async Task<IActionResult> GetCatigries()
        {
            var cats = await _db.Categories.ToListAsync(); 
            return Ok(cats);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatigory(int id )
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");

            return Ok(c);
        }
        [HttpPost]
        public async Task<IActionResult> addCategory(string cat , string des)
        {
            Category c = new Category() { Name = cat, Description = des };
            await _db.Categories.AddAsync(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut]
        [ProducesResponseType(200,Type = typeof(Category)) ]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> Updatecategory(Category cat)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x=>x.Id==cat.Id);

            if (c == null) return NotFound($"Not Found{cat.Id} , there is no category with id = {cat.Id} ");
            c.Name= cat.Name;
            c.Description= cat.Description;
            _db.SaveChanges();
            return Ok(c);

        }

        [HttpDelete]
        public async Task<IActionResult> Removecategory(int id)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");
             _db.Categories.Remove(c);
            _db.SaveChanges();
            return Ok(c);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> pathcsomething([FromBody] JsonPatchDocument<Category> p, [FromRoute] int id)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");

            p.ApplyTo(c);
            await _db.SaveChangesAsync();
            return Ok(c);
        }

    }
}
