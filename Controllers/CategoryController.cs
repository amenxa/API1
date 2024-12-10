using ApiTest.Data;
using ApiTest.Data.DTOS;
using AutoMapper;
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
        private readonly IMapper mapper;
        public CategoryController(AppDbContext db, ILogger<CategoryController> logger , IMapper mapper)
        {
            this.logger = logger;
            this._db = db;
            this.mapper = mapper;

        }

        private readonly AppDbContext _db;
        [HttpGet("GetALL")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> GetCatigries()
        {
            var DTODATA = mapper.Map<List<CategoryDTO>>(_db.Categories.ToList());
            return Ok(DTODATA);
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
            var cdto = mapper.Map<CategoryDTO>(c);

            return Ok(cdto);
        }
        [HttpPost]
        [Route("{cat:alpha}/{des:alpha}/addCAtegory")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> addCategory(string cat, string des)
        {
            Category c = new Category() { Name = cat, Description = des };
            await _db.Categories.AddAsync(c);
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(c);
            _db.SaveChanges();
            return Ok(categoryDTO);
        }

        [HttpPut("UPDATECATEGORY")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(402, Type = typeof(string))]
        public async Task<IActionResult> Updatecategory(CategoryDTO cat)
        {
            Category c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == cat.Id);

            if (c == null) return NotFound($"Not Found{cat.Id} , there is no category with id = {cat.Id} ");
            c.Name = cat.Name;
            var c1 = new Category() { Id = 3, Name = "c.Name" };
            _db.Categories.Update(c1);
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
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(c);
            p.ApplyTo(categoryDTO);
            c.Name = categoryDTO.Name;
            await _db.SaveChangesAsync();
            return Ok(categoryDTO);
        }
               
    }
}
