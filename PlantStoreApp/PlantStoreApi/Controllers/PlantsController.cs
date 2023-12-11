using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantStoreApi.Data;
using PlantStoreApi.Models;

namespace PlantStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly StoreDbContext _dbContext;
        public PlantsController(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Plant Endpoint
        // Fetches All Plant Models
        [HttpGet]
        public IActionResult GetPlants()
        {
            try
            {
                var plantItems = _dbContext.Plant
                    .ToList();
                if (plantItems == null || plantItems.Count == 0)
                {
                    return NotFound("Plant Data Not Available");
                }

                return Ok(plantItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Fetches a Plant Model Based on the ID
        [HttpGet("{id}")]
        public IActionResult GetPlantById(int id)
        {
            var plantItem = _dbContext.Plant.FirstOrDefault(x => x.ID == id);

            if (plantItem == null)
            {
                return NotFound();
            }

            return Ok(plantItem);
        }

        // Creates a Plant Model
        [HttpPost]
        public IActionResult CreatePlant([FromBody] Plant item)
        {
            _dbContext.Plant.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetPlantById), new { id = item.ID }, item);
        }

        // Updates a Plant Model
        [HttpPut("{id}")]
        public IActionResult UpdatePlant(int id, [FromBody] Plant item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            _dbContext.Plant.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetPlantById), new { id = item.ID }, item);
        }

        // Deletes a Plant Model
        [HttpDelete("{id}")]
        public IActionResult DeletePlant(int id)
        {
            try
            {
                var plantItem = _dbContext.Plant.FirstOrDefault(x => x.ID == id);

                if (plantItem == null)
                    return NotFound();

                _dbContext.Entry(plantItem).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region User Endpoint
        // Fetches All User Models
        [HttpGet]

        [Route("Adopter")]
        public IActionResult GetAdopters()
        {
            try
            {
                var AdopterItems = _dbContext.Adopters.ToList();
                if (AdopterItems == null || AdopterItems.Count == 0)
                {
                    return NotFound("User Data Not Available");
                }

                return Ok(AdopterItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Fetches a User Model Based on the ID
        [HttpGet]
        [Route("Adopter/{id}")]
        public IActionResult GetUserById(int id)
        {
            var AdopterItems = _dbContext.Adopters.FirstOrDefault(x => x.ID == id);

            if (AdopterItems == null)
            {
                return NotFound();
            }

            return Ok(AdopterItems);
        }

        // Creates a User Model
        [HttpPost]
        [Route("Adopter")]
        public IActionResult CreateUser([FromBody] Adopter item)
        {
            _dbContext.Adopters.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = item.ID }, item);
        }

        // Updates a User Model
        [HttpPut]
        [Route("Adopter/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Adopter item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            _dbContext.Adopters.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = item.ID }, item);
        }

        // Deletes a User Model
        [HttpDelete]
        [Route("Adopter/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var AdopterItems = _dbContext.Adopters.FirstOrDefault(x => x.ID == id);

                if (AdopterItems == null)
                    return NotFound();

                _dbContext.Entry(AdopterItems).State = EntityState.Deleted;
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region AdoptionRequest Endpoint
        // Fetches All AdoptionRequest Models
        [HttpGet]
        [Route("Request")]
        public IActionResult GetAdoptionRequests()
        {
            try
            {
                var adoptionRequestItems = _dbContext.AdoptionRequests
                    .Include(x => x.Plant)
                    .Include(x => x.Adopter)
                    .ToList();
                if (adoptionRequestItems == null || adoptionRequestItems.Count == 0)
                {
                    return NotFound("Adoption Request Data Not Available");
                }

                return Ok(adoptionRequestItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Fetches an AdoptionRequest Model Based on the ID
        [HttpGet]
        [Route("Request/{id}")]

        public IActionResult GetAdoptionRequestById(int id)
        {
            var adoptionRequestItem = _dbContext.AdoptionRequests
                .Include(x => x.Plant)
                .Include(x => x.Adopter)
                .FirstOrDefault(x => x.ID == id);

            if (adoptionRequestItem == null)
            {
                return NotFound();
            }

            return Ok(adoptionRequestItem);
        }

        // Creates an AdoptionRequest Model
        [HttpPost]     
        [Route("Request")]


        public IActionResult CreateAdoptionRequest([FromBody] AdoptionRequest item)
        {
            _dbContext.AdoptionRequests.Add(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAdoptionRequestById), new { id = item.ID }, item);
        }

        // Updates an AdoptionRequest Model
        [HttpPut]
        [Route("Request/{id}")]
        public IActionResult UpdateAdoptionRequest(int id, [FromBody] AdoptionRequest item)
        {
            if (id != item.ID)
            {
                return BadRequest();
            }

            _dbContext.AdoptionRequests.Update(item);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetAdoptionRequestById), new { id = item.ID }, item);
        }

        // Deletes an AdoptionRequest Model
        [HttpDelete]
        [Route("Request/{id}")]

        public IActionResult DeleteAdoptionRequest(int id)
        {
            try
            {
                var adoptionRequestItem = _dbContext.AdoptionRequests.FirstOrDefault(x => x.ID == id);

                if (adoptionRequestItem == null)
                    return NotFound();

                _dbContext.Entry(adoptionRequestItem).State = EntityState.Deleted;
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
