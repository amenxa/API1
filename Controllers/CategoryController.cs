using ApiTest.Data;
using ApiTest.Data.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        public CategoryController(AppDbContext db, ILogger<CategoryController> logger)
        {
            this.logger = logger;
            this._db = db;

        }

        private readonly AppDbContext _db;
        [HttpGet("GetALL")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> GetCatigries()
        {

            var cats = _db.Categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,

            });
            return Ok(cats);
        }
        [HttpGet("{id}/getbyID")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetCatigory(int id)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);


            if (c == null)
            {
                logger.LogError($"id {id} not found");
                return NotFound($"Not Found{id} , there is no category with id = {id} ");

            }
            var cdto = new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,
            };

            return Ok(cdto);
        }
        [HttpPost]
        [Route("{categoryNAme:alpha}/addCAtegory")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> addCategory(string cat, string des)
        {
            Category c = new Category() { Name = cat, Description = des };
            await _db.Categories.AddAsync(c);
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,
            };
            _db.SaveChanges();
            return Ok(categoryDTO);
        }

        [HttpPut("UPDATECATEGORY")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> Updatecategory(CategoryDTO cat)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == cat.Id);

            if (c == null) return NotFound($"Not Found{cat.Id} , there is no category with id = {cat.Id} ");
            c.Name = cat.Name;

            _db.SaveChanges();
            return Ok(cat);

        }

        [HttpDelete("{id:int}/DeleteById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> Removecategory(int id)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);

            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");
             _db.Categories.Remove(c);
            _db.SaveChanges();
            return Ok(true);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> pathcsomething([FromBody] JsonPatchDocument<CategoryDTO> p, [FromRoute] int id)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");
            CategoryDTO categoryDTO = new CategoryDTO() { Id=c.Id , Name = c.Name};
            p.ApplyTo(categoryDTO);
            c.Name = categoryDTO.Name;
            await _db.SaveChangesAsync();
            return Ok(categoryDTO);
        }
               
    }
}
