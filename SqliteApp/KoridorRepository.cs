using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class KoridorRepository : IKoridorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public KoridorRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddKoridorAsync(Koridor koridor)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Koridor>(koridor);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public async Task<List<Koridor>> GetKoridorAsync()
        {
            try
            {

                return await _databaseContext.Koridorlar.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Koridor> GetKoridorByIdAsync(int id)
        {
            try
            {

                var koridor = await _databaseContext.Koridorlar.FindAsync(id);

                return koridor;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Koridor>> QueryKoridorAsync(Func<Koridor, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Koridorlar.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveKoridorAsync(Koridor koridor)
        {
            try
            {
                var asd = await _databaseContext.Koridorlar.FindAsync(koridor.Id);
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

        public async Task<bool> UpdateKoridorAsync(Koridor koridor)
        {
            try
            {
                var tracking = _databaseContext.Update(koridor);
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
