using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Models;
using WarehouseApi.Service;

namespace WarehouseApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class KontrahenciController : ControllerBase
        {
        private readonly IKontrahenciService _kontrahenciService;

        public KontrahenciController(IKontrahenciService kontrahenciService)
            {
            _kontrahenciService = kontrahenciService;
            }

        // GET: api/Kontrahenci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kontrahenci>>> GetKontrahenci()
            {
            var kontrahenci = await _kontrahenciService.GetAllKontrahenciAsync();
            return Ok(kontrahenci);
            }

        // GET: api/Kontrahenci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kontrahenci>> GetKontrahent(int id)
            {
            var kontrahent = await _kontrahenciService.GetKontrahenciByIdAsync(id);

            if (kontrahent == null)
                {
                return NotFound();
                }

            return Ok(kontrahent);
            }

        // POST: api/Kontrahenci
        [HttpPost]
        public async Task<ActionResult<Kontrahenci>> PostKontrahent(Kontrahenci kontrahent)
            {
            var newKontrahent = await _kontrahenciService.CreateKontrahenciAsync(kontrahent);
            return CreatedAtAction(nameof(GetKontrahent), new { id = newKontrahent.Id }, newKontrahent);
            }

        // PUT: api/Kontrahenci/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKontrahent(int id, Kontrahenci kontrahent)
            {
            var updated = await _kontrahenciService.UpdateKontrahenciAsync(id, kontrahent);
            if (!updated)
                {
                return NotFound();
                }

            return NoContent();
            }

        // DELETE: api/Kontrahenci/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKontrahent(int id)
            {
            var deleted = await _kontrahenciService.DeleteKontrahenciAsync(id);
            if (!deleted)
                {
                return NotFound();
                }

            return NoContent();
            }
        }
    }
