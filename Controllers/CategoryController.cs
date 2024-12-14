using ApiTest.Data;
using ApiTest.Data.DTOS;
using ApiTest.Repository;
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
        private readonly ICommonRepo<Category> repo;
        public CategoryController(ICommonRepo<Category> repo , ILogger<CategoryController> logger , IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet("GetALL")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> GetCatigries()
        {
            var DTODATA = mapper.Map<List<CategoryDTO>>(await repo.GetALL());
            return Ok(DTODATA);
        }
        [HttpGet("{id}/getbyID", Name = "GetCatigory")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> GetCatigory(int id)
        {
            Category c = await repo.GetById(x=>x.Id==id);


            if (c == null)
            {
                logger.LogError($"id {id} not found");
                return NotFound($"Not Found{id} , there is no category with id = {id} ");

            }
            var cdto = mapper.Map<CategoryDTO>(c);

            return Ok(cdto);
        }
        [HttpPost]
        [Route("{cat:alpha}/{des:alpha}/addCAtegory", Name = "addCategory")]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> addCategory(string cat, string des)
        {
            Category c = new Category() { Name = cat, Description = des };
            c = await repo.AddCategory(c);
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(c);
            return CreatedAtRoute("GetCatigory", new CategoryDTO() { Name = cat }, categoryDTO);
        }

        [HttpPut("UPDATECATEGORY")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> Updatecategory(CategoryDTO cat)
        {
            var c = await repo.GetById(x => x.Id == cat.Id, false);
            
            if (c==null)
            {
                logger.LogError($"id {cat.Id} not found");
                return NotFound($"Not Found , there is no category with id = {cat.Id} ");
            }
            c.Name = cat.Name;
            repo.Update(c);
            return Ok(cat);

        }

        [HttpDelete("{id:int}/DeleteById")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> Removecategory(int id)
        {
            bool OK = await repo.RemoveCategory(x => x.Id == id);
            if (!OK) return NotFound($"Not Found{id} , there is no category with id = {id} ");
            return Ok(true);
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> pathcsomething([FromBody] JsonPatchDocument<CategoryDTO> p, [FromRoute] int id)
        {
            Category c = await repo.GetById(x => x.Id == id , false);
            if (c == null) return NotFound($"Not Found{id} , there is no category with id = {id} ");
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(c);
            p.ApplyTo(categoryDTO);
            c =mapper.Map<Category>(categoryDTO);
            repo.Update(c);
            return Ok(categoryDTO);
        }
               
    }
}
