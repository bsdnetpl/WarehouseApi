using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public class DostawcaService : IDostawcaService
        {
        private readonly BsdnetphMatgazynMainContext _context;

        public DostawcaService(BsdnetphMatgazynMainContext context)
            {
            _context = context;
            }

        // Pobranie wszystkich dostawców
        public async Task<IEnumerable<Dostawca>> GetAllDostawcyAsync()
            {
            return await _context.Dostawcas.ToListAsync();
            }

        // Pobranie dostawcy po ID
        public async Task<Dostawca?> GetDostawcaByIdAsync(int id)
            {
            return await _context.Dostawcas.FindAsync(id);
            }

        // Dodanie nowego dostawcy
        public async Task<Dostawca> CreateDostawcaAsync(Dostawca dostawca)
            {
            _context.Dostawcas.Add(dostawca);
            await _context.SaveChangesAsync();
            return dostawca;
            }

        // Aktualizacja dostawcy
        public async Task<bool> UpdateDostawcaAsync(int id, Dostawca dostawca)
            {
            var existingDostawca = await _context.Dostawcas.FindAsync(id);
            if (existingDostawca == null)
                {
                return false;
                }

            existingDostawca.NrDostawcy = dostawca.NrDostawcy;
            existingDostawca.NazwaDostawcy = dostawca.NazwaDostawcy;
            existingDostawca.DowodZakupu = dostawca.DowodZakupu;
            existingDostawca.DataZakupu = dostawca.DataZakupu;
            existingDostawca.Netto23 = dostawca.Netto23;
            existingDostawca.Podatek23 = dostawca.Podatek23;
            existingDostawca.Vat23 = dostawca.Vat23;
            existingDostawca.Netto8 = dostawca.Netto8;
            existingDostawca.Podatek8 = dostawca.Podatek8;
            existingDostawca.Vat8 = dostawca.Vat8;
            existingDostawca.Netto5 = dostawca.Netto5;
            existingDostawca.Podatek5 = dostawca.Podatek5;
            existingDostawca.Vat5 = dostawca.Vat5;
            existingDostawca.Kod1 = dostawca.Kod1;
            existingDostawca.Kod2 = dostawca.Kod2;
            existingDostawca.Kod3 = dostawca.Kod3;
            existingDostawca.Kod4 = dostawca.Kod4;
            existingDostawca.Kod5 = dostawca.Kod5;
            existingDostawca.NumerZamowienia = dostawca.NumerZamowienia;

            await _context.SaveChangesAsync();
            return true;
            }

        // Usunięcie dostawcy
        public async Task<bool> DeleteDostawcaAsync(int id)
            {
            var dostawca = await _context.Dostawcas.FindAsync(id);
            if (dostawca == null)
                {
                return false;
                }

            _context.Dostawcas.Remove(dostawca);
            await _context.SaveChangesAsync();
            return true;
            }
        }
    }
