using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class NesneRepository : INesneRepository
    {
        private readonly DatabaseContext _databaseContext;

        public NesneRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddNesneAsync(Nesne nesne)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Nesne>(nesne);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public async Task<List<Nesne>> GetNesneAsync()
        {
            try
            {

                return await _databaseContext.Nesneler.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Nesne> GetNesneByIdAsync(int id)
        {
            try
            {

                var nesne = await _databaseContext.Nesneler.FindAsync(id);

                return nesne;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Nesne>> QueryNesneAsync(Func<Nesne, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Nesneler.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveNesneAsync(Nesne nesne)
        {
            try
            {
                var asd = await _databaseContext.Nesneler.FindAsync(nesne.Id);
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

        public async Task<bool> UpdateNesneAsync(Nesne nesne)
        {
            try
            {
                var tracking = _databaseContext.Update(nesne);
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
