using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

namespace WarehouseApi.Service
    {
    public class RaportDobowyService : IRaportDobowyService
        {
        private readonly BsdnetphMatgazynMainContext _context;

        public RaportDobowyService(BsdnetphMatgazynMainContext context)
            {
            _context = context;
            }

        // Pobranie wszystkich raportów
        public async Task<IEnumerable<RaportDobowy>> GetAllRaportyAsync()
            {
            return await _context.RaportDobowies.ToListAsync();
            }

        // Pobranie raportu po ID
        public async Task<RaportDobowy?> GetRaportByIdAsync(int id)
            {
            return await _context.RaportDobowies.FindAsync(id);
            }

        // Tworzenie nowego raportu
        public async Task<RaportDobowy> CreateRaportAsync(RaportDobowy raport)
            {
            _context.RaportDobowies.Add(raport);
            await _context.SaveChangesAsync();
            return raport;
            }

        // Aktualizacja raportu
        public async Task<bool> UpdateRaportAsync(int id, RaportDobowy raport)
            {
            var existingRaport = await _context.RaportDobowies.FindAsync(id);
            if (existingRaport == null)
                {
                return false;
                }

            existingRaport.Data = raport.Data;
            existingRaport.Zysk = raport.Zysk;
            existingRaport.UtargBrutto = raport.UtargBrutto;
            existingRaport.Sva = raport.Sva;
            existingRaport.Svb = raport.Svb;
            existingRaport.Svc = raport.Svc;
            existingRaport.Svd = raport.Svd;
            existingRaport.Sve = raport.Sve;
            existingRaport.Svf = raport.Svf;
            existingRaport.Svg = raport.Svg;
            existingRaport.Raportujacy = raport.Raportujacy;
            existingRaport.Godzina = raport.Godzina;
            existingRaport.Gotowka = raport.Gotowka;
            existingRaport.RoznicaVat = raport.RoznicaVat;

            await _context.SaveChangesAsync();
            return true;
            }

        // Usuwanie raportu
        public async Task<bool> DeleteRaportAsync(int id)
            {
            var raport = await _context.RaportDobowies.FindAsync(id);
            if (raport == null)
                {
                return false;
                }

            _context.RaportDobowies.Remove(raport);
            await _context.SaveChangesAsync();
            return true;
            }
        }
    }
