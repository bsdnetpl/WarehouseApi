using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public class KontrahenciService : IKontrahenciService
        {
        private readonly BsdnetphMatgazynMainContext _context;
        public KontrahenciService(BsdnetphMatgazynMainContext context)
            {
            _context = context;
            }

        // Pobranie wszystkich kontrahentów
        public async Task<IEnumerable<Kontrahenci>> GetAllKontrahenciAsync()
            {
            return await _context.Kontrahencis.ToListAsync();
            }

        // Pobranie kontrahenta po ID
        public async Task<Kontrahenci?> GetKontrahenciByIdAsync(int id)
            {
            return await _context.Kontrahencis.FindAsync(id);
            }

        // Tworzenie nowego kontrahenta
        public async Task<Kontrahenci> CreateKontrahenciAsync(Kontrahenci kontrahent)
            {
            _context.Kontrahencis.Add(kontrahent);
            await _context.SaveChangesAsync();
            return kontrahent;
            }

        // Aktualizacja kontrahenta
        public async Task<bool> UpdateKontrahenciAsync(int id, Kontrahenci kontrahent)
            {
            var existingKontrahent = await _context.Kontrahencis.FindAsync(id);
            if (existingKontrahent == null)
                {
                return false;
                }

            existingKontrahent.Nazwa = kontrahent.Nazwa;
            existingKontrahent.Ulica = kontrahent.Ulica;
            existingKontrahent.Miasto = kontrahent.Miasto;
            existingKontrahent.Nip = kontrahent.Nip;
            existingKontrahent.Telefon = kontrahent.Telefon;
            existingKontrahent.Email = kontrahent.Email;
            existingKontrahent.OstatniaFv = kontrahent.OstatniaFv;
            existingKontrahent.Obrot = kontrahent.Obrot;
            existingKontrahent.Reprezentant = kontrahent.Reprezentant;
            existingKontrahent.Skrot = kontrahent.Skrot;
            existingKontrahent.Odbiorca = kontrahent.Odbiorca;
            existingKontrahent.KodPocztowy = kontrahent.KodPocztowy;
            existingKontrahent.NrDomu = kontrahent.NrDomu;
            existingKontrahent.Wojewodztwo = kontrahent.Wojewodztwo;
            existingKontrahent.Powiat = kontrahent.Powiat;
            existingKontrahent.Gmina = kontrahent.Gmina;

            await _context.SaveChangesAsync();
            return true;
            }

        // Usuwanie kontrahenta
        public async Task<bool> DeleteKontrahenciAsync(int id)
            {
            var kontrahent = await _context.Kontrahencis.FindAsync(id);
            if (kontrahent == null)
                {
                return false;
                }

            _context.Kontrahencis.Remove(kontrahent);
            await _context.SaveChangesAsync();
            return true;
            }
        }
    }
