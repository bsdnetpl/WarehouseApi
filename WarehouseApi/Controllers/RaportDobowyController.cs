using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Models;
using WarehouseApi.Service;

namespace WarehouseApi.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class RaportDobowyController : ControllerBase
        {
        private readonly IRaportDobowyService _raportService;

        public RaportDobowyController(IRaportDobowyService raportService)
            {
            _raportService = raportService;
            }

        // GET: api/RaportDobowy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaportDobowy>>> GetRaporty()
            {
            var raporty = await _raportService.GetAllRaportyAsync();
            return Ok(raporty);
            }

        // GET: api/RaportDobowy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RaportDobowy>> GetRaport(int id)
            {
            var raport = await _raportService.GetRaportByIdAsync(id);
            if (raport == null)
                {
                return NotFound();
                }

            return Ok(raport);
            }

        // POST: api/RaportDobowy
        [HttpPost]
        public async Task<ActionResult<RaportDobowy>> PostRaport(RaportDobowy raport)
            {
            var newRaport = await _raportService.CreateRaportAsync(raport);
            return CreatedAtAction(nameof(GetRaport), new { id = newRaport.Id }, newRaport);
            }

        // PUT: api/RaportDobowy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRaport(int id, RaportDobowy raport)
            {
            var updated = await _raportService.UpdateRaportAsync(id, raport);
            if (!updated)
                {
                return NotFound();
                }

            return NoContent();
            }

        // DELETE: api/RaportDobowy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaport(int id)
            {
            var deleted = await _raportService.DeleteRaportAsync(id);
            if (!deleted)
                {
                return NotFound();
                }

            return NoContent();
            }
        }
    }
