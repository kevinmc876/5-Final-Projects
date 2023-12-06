using AmberStudentSystem.Data;
using AmberStudentSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmberStudentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {   
        private readonly EnrollmentDBContext _dbContext;
        public EnrollmentController(EnrollmentDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Student Endpoint
        //Fetches All Student Models
        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                var sItems = _dbContext.Student.Include(b => b.Parish)
                        .Include(b => b.Programs)
                        .Include(b => b.Shirt)
                        .ToList();
                if(sItems == null)
                {
                    return NotFound("Student Data Not Available");
                }

                return Ok(sItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Fetches a Student Model Based on the ID
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var sItem = _dbContext.Student.Include(b => b.Parish)
                    .Include(b => b.Programs)
                    .Include(b => b.Shirt)
                    .FirstOrDefault(x => x.Id == id);

            if(sItem == null)
            {
                return NotFound();
            }

            return Ok(sItem);
        }
        //Creates a Student Model
        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student item)
        {
            _dbContext.Student.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetStudentById), new {id = item.Id}, item);
        }
        //Updates a Student Model
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student item)
        {
            if( id != item.Id)
            {
                return BadRequest();
            }

            _dbContext.Student.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetStudentById), new { id = item.Id }, item);
        }
        //Delete a Student Model
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var sItem = _dbContext.Student.Include(b => b.Parish)
                    .Include(b => b.Programs)
                    .Include(b => b.Shirt)
                    .FirstOrDefault(x => x.Id == id);
                
                if (sItem == null) return NotFound();

                _dbContext.Entry(sItem).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Parish Endpoint
        //Fetches All Parish Model
        [HttpGet]
        [Route("Parish")]
        public IActionResult GetParish()
        {
            var iParish = _dbContext.Parish.ToList();
            if(iParish == null)
            {
                return BadRequest();
            }

            return Ok(iParish);
        }
        //Fetches a Parish by ID
        [HttpGet]
        [Route("Parish/{id}")]
        public IActionResult GetParishById(int id)
        {
            var iParish = _dbContext.Parish.FirstOrDefault(x => x.Id == id);
            if(iParish == null)
            {
                return NotFound();
            }

            return Ok(iParish);
        }
        //Creates a Parish Model
        [HttpPost]
        [Route("Parish")]
        public IActionResult CreateParish([FromBody] Parish item)
        {
            _dbContext.Parish.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetParishById), new {id = item.Id }, item);
        }
        //Update a Parish Model
        [HttpPut("Parish/{id}")]
        public IActionResult UpdateParish(int id, [FromBody] Parish item)
        {
            if (id != item.Id) return NotFound();

            _dbContext.Parish.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetParishById), new { id = item.Id }, item);
        }
        //Delete a Shirt Model
        [HttpDelete("Parish/{id}")]
        public IActionResult DeleteParish(int id)
        {
            try
            {
                var iParish = _dbContext.Parish.FirstOrDefault(x => x.Id == id);
                if (iParish == null) return NotFound();

                _dbContext.Entry(iParish).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Program Endpoint
        //Fetches All Program Model
        [HttpGet]
        [Route("Programs")]
        public IActionResult GetPrograms()
        {
            var iPrograms = _dbContext.Programs.ToList();
            if (iPrograms == null)
            {
                return BadRequest();
            }

            return Ok(iPrograms);
        }
        //Fetches a Programs by ID
        [HttpGet]
        [Route("Programs/{id}")]
        public IActionResult GetProgramsById(int id)
        {
            var iPrograms = _dbContext.Programs.FirstOrDefault(x => x.Id == id);
            if (iPrograms == null)
            {
                return NotFound();
            }

            return Ok(iPrograms);
        }
        //Creates a Programs Model
        [HttpPost]
        [Route("Programs")]
        public IActionResult CreatePrograms([FromBody] Programs item)
        {
            _dbContext.Programs.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProgramsById), new { id = item.Id }, item);
        }
        //Update a Programs Model
        [HttpPut("Programs/{id}")]
        public IActionResult UpdatePrograms(int id, [FromBody] Programs item)
        {
            if (id != item.Id) return NotFound();

            _dbContext.Programs.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProgramsById), new { id = item.Id }, item);
        }
        //Delete a Programs Model
        [HttpDelete("Programs/{id}")]
        public IActionResult DetelePrograms(int id)
        {
            try
            {
                var iPrograms = _dbContext.Programs.FirstOrDefault(x => x.Id == id);
                if (iPrograms == null) return NotFound();

                _dbContext.Entry(iPrograms).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Shirt Endpoint
        //Fetches All Shirt Model
        [HttpGet]
        [Route("Shirt")]
        public IActionResult GetShirt()
        {
            var sItem = _dbContext.Shirt.ToList();
            if (sItem == null)
            {
                return BadRequest();
            }

            return Ok(sItem);
        }
        //Fetches a Shirt by ID
        [HttpGet]
        [Route("Shirt/{id}")]
        public IActionResult GetShirtById(int id)
        {
            var sItem = _dbContext.Shirt.FirstOrDefault(x => x.Id == id);
            if (sItem == null)
            {
                return NotFound();
            }

            return Ok(sItem);
        }
        //Creates a Shirt Model
        [HttpPost]
        [Route("Shirt")]
        public IActionResult CreateShirt([FromBody] Shirt item)
        {
            _dbContext.Shirt.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById), new { id = item.Id }, item);
        }
        //Update a Shirt Model
        [HttpPut("Shirt/{id}")]
        public IActionResult UpdateShirt(int id, [FromBody] Shirt item)
        {
            if (id != item.Id) return NotFound();

            _dbContext.Shirt.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetShirtById), new { id = item.Id }, item);
        }
        //Delete a Shirt Model
        [HttpDelete("Shirt/{id}")]
        public IActionResult DeleteShirt(int id)
        {
            try
            {
                var sItem = _dbContext.Shirt.FirstOrDefault(x => x.Id == id);
                if (sItem == null) return NotFound();

                _dbContext.Entry(sItem).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
