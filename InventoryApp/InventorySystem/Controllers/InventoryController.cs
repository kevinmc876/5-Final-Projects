using InventorySystem.Data;
using InventorySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace InventorySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDBContext _ctx;

        public InventoryController(InventoryDBContext context)
        {
            _ctx = context;
        }

        #region Products

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _ctx.Product
                .Include(b => b.Category).Include(b => b.Size).
                ToListAsync();
        }


        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var pItem = _ctx.Product.Include(b => b.Category).Include(b => b.Size).FirstOrDefault(x => x.Id == id);
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Products item)
        {
            _ctx.Product.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetProductById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Products item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Product.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetProductById), new { id = item.Id }, item);
        }

        #endregion

        #region Category 

        [HttpGet]
        [Route("Category")]
        public IActionResult GetCategory()
        {
            var pCategory = _ctx.Category.ToList();
            if (pCategory == null)
            {
                return NotFound();
            }

            return Ok(pCategory);
        }

        [HttpGet]
        [Route("Category/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var pCategory = _ctx.Category.FirstOrDefault(x => x.Id == id);
            if (pCategory == null)
            {
                return NotFound();
            }

            return Ok(pCategory);
        }

        [HttpPost]
        [Route("Category")]
        public IActionResult CreateCategory([FromBody] Categories item)
        {
            _ctx.Category.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = item.Id }, item);
        }

        [HttpPut]
        [Route("Category/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Categories item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            _ctx.Category.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = item.Id }, item);
        }

        #endregion


        #region Sizes


        [HttpGet]
        [Route("Size")]
        public IActionResult GetSize()
        {
            var Size = _ctx.Size.ToList();
            if (Size == null)
            {
                return NotFound();
            }

            return Ok(Size);
        }

        [HttpGet]
        [Route("Size/{id}")]
        public IActionResult GetSizeById(int id)
        {
            var size = _ctx.Size.FirstOrDefault(x => x.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return Ok(size);
        }

        [HttpPost]
        [Route("Size")]
        public IActionResult CreateSize([FromBody] Sizes item)
        {
            _ctx.Size.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetSizeById), new { id = item.Id }, item);
        }

        [HttpPut]
        [Route("Size/{id}")]
        public IActionResult UpdateSize(int id, [FromBody] Sizes item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            _ctx.Size.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetSizeById), new { id = item.Id }, item);
        }

        #endregion

    }
}

