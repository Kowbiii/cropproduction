using CropProduce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CropPruduce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropPruduceController : ControllerBase
    {
        // In-memory storage for crops
        private static readonly List<CropClass> Crops = new List<CropClass>();
        private static int _nextId = 1; // For assigning unique IDs

        private readonly ILogger<CropPruduceController> _logger;

        public CropPruduceController(ILogger<CropPruduceController> logger)
        {
            _logger = logger;

            // Sample crop data for initial setup
            if (!Crops.Any())
            {
                Crops.Add(new CropClass { Id = _nextId++, Name = "Sugar", GrowthDurationDays = 90, HarvestDate = DateTime.Now.AddDays(90) });
                Crops.Add(new CropClass { Id = _nextId++, Name = "Coffee", GrowthDurationDays = 80-90, HarvestDate = DateTime.Now.AddDays(80-90) });
                Crops.Add(new CropClass { Id = _nextId++, Name = "Salt", GrowthDurationDays = 100, HarvestDate = DateTime.Now.AddDays(100) });
                Crops.Add(new CropClass { Id = _nextId++, Name = "Wheat", GrowthDurationDays = 100, HarvestDate = DateTime.Now.AddDays(100) });
            }
        }

        // GET: api/cropproduce
        [HttpGet(Name = "GetCrops")]
        public ActionResult<IEnumerable<CropClass>> Get()
        {
            return Ok(Crops);
        }

        // GET: api/cropproduce/{id}
        [HttpGet("{id}", Name = "GetCropById")]
        public ActionResult<CropClass> GetById(int id)
        {
            var crop = Crops.FirstOrDefault(c => c.Id == id);
            if (crop == null)
            {
                return NotFound();
            }
            return Ok(crop);
        }

        // POST: api/cropproduce
        [HttpPost(Name = "CreateCrop")]
        public ActionResult<CropClass> Create(CropClass crop)
        {
            crop.Id = _nextId++;
            Crops.Add(crop);
            return CreatedAtAction(nameof(GetById), new { id = crop.Id }, crop);
        }

        // PUT: api/cropproduce/{id}
        [HttpPut("{id}", Name = "UpdateCrop")]
        public ActionResult Update(int id, CropClass updatedCrop)
        {
            var crop = Crops.FirstOrDefault(c => c.Id == id);
            if (crop == null)
            {
                return NotFound();
            }
            crop.Name = updatedCrop.Name;
            crop.GrowthDurationDays = updatedCrop.GrowthDurationDays;
            crop.HarvestDate = updatedCrop.HarvestDate;
            return NoContent();
        }

        // DELETE: api/cropproduce/{id}
        [HttpDelete("{id}", Name = "DeleteCrop")]
        public ActionResult Delete(int id)
        {
            var crop = Crops.FirstOrDefault(c => c.Id == id);
            if (crop == null)
            {
                return NotFound();
            }
            Crops.Remove(crop);
            return NoContent();
        }
    }
}
