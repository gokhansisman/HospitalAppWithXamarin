using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class OdalarRepository : IOdaRepository
    {
        private readonly DatabaseContext _databaseContext;

        public OdalarRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddOdaAsync(Oda oda)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Oda>(oda);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public async Task<List<Oda>> GetOdaAsync()
        {
            try
            {

                return await _databaseContext.Odalar.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Oda> GetOdaByIdAsync(int id)
        {
            try
            {

                var oda = await _databaseContext.Odalar.FindAsync(id);

                return oda;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Oda>> QueryOdaAsync(Func<Oda, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Odalar.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveOdaAsync(Oda oda)
        {
            try
            {
                var asd = await _databaseContext.Odalar.FindAsync(oda.Id);
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

        public async Task<bool> UpdateOdaAsync(Oda oda)
        {
            try
            {
                var tracking = _databaseContext.Update(oda);
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
