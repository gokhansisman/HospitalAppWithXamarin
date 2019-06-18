using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly DatabaseContext _databaseContext;

        public KullaniciRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

       

        public async Task<bool> AddKullaniciAsync(Kullanici kulanici)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Kullanici>(kulanici);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<List<Kullanici>> GetKullaniciAsync()
        {
            try
            {

                return await _databaseContext.Kullanicilar.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

       

        public async Task<Kullanici> GetKullaniciByIdAsync(int id)
        {
            try
            {

                var kullanici = await _databaseContext.Kullanicilar.FindAsync(id);

                return kullanici;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Kullanici>> QueryKullaniciAsync(Func<Kullanici, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Kullanicilar.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveKullaniciAsync(Kullanici kulanici)
        {
            try
            {
                var asd = await _databaseContext.Products.FindAsync(kulanici.Id);
                var tracking = _databaseContext.Remove(asd);

                await _databaseContext.SaveChangesAsync();
                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;

            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateKullaniciAsync(Kullanici kulanici)
        {
            try
            {
                var tracking = _databaseContext.Update(kulanici);
                await _databaseContext.SaveChangesAsync();
                var isModified = tracking.State == EntityState.Modified;
                return isModified;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        
    }


}
