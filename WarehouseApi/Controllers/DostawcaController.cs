using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Models;
using WarehouseApi.Service;

namespace WarehouseApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class DostawcaController : ControllerBase
        {
        private readonly IDostawcaService _dostawcaService;

        public DostawcaController(IDostawcaService dostawcaService)
            {
            _dostawcaService = dostawcaService;
            }

        // GET: api/Dostawca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dostawca>>> GetDostawcy()
            {
            var dostawcy = await _dostawcaService.GetAllDostawcyAsync();
            return Ok(dostawcy);
            }

        // GET: api/Dostawca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dostawca>> GetDostawca(int id)
            {
            var dostawca = await _dostawcaService.GetDostawcaByIdAsync(id);

            if (dostawca == null)
                {
                return NotFound();
                }

            return Ok(dostawca);
            }

        // POST: api/Dostawca
        [HttpPost]
        public async Task<ActionResult<Dostawca>> PostDostawca(Dostawca dostawca)
            {
            var newDostawca = await _dostawcaService.CreateDostawcaAsync(dostawca);
            return CreatedAtAction(nameof(GetDostawca), new { id = newDostawca.Id }, newDostawca);
            }

        // PUT: api/Dostawca/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDostawca(int id, Dostawca dostawca)
            {
            var updated = await _dostawcaService.UpdateDostawcaAsync(id, dostawca);
            if (!updated)
                {
                return NotFound();
                }

            return NoContent();
            }

        // DELETE: api/Dostawca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDostawca(int id)
            {
            var deleted = await _dostawcaService.DeleteDostawcaAsync(id);
            if (!deleted)
                {
                return NotFound();
                }

            return NoContent();
            }
        }
    }
