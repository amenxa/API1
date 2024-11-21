using ApiTest.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public ItemController (AppDbContext db)
        {

            this._db = db;

        }

        private readonly AppDbContext _db;
        [HttpGet]
        public async Task<IActionResult> GetItems() => Ok();
        
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetItemswithAnother() =>Ok("something");
        
        [HttpGet()]
         [Route("{id:int}")]
        public async Task<IActionResult> GetItems(int id ) =>  Ok("Something");

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetINames(string name) => Ok("Something");

        [HttpGet]
        [Route("{name:alpha}/{id:int} /ameen" ,Name = "GetINamesAndid")]
        public async Task<IActionResult> GetINamesAndid(string name ,int id) => Ok("Something");


      


    }
}
