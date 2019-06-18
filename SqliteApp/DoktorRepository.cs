using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class DoktorRepository : IDoktorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public DoktorRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddDoktorAsync(Doktor doktor)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Doktor>(doktor);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        

        public async Task<List<Doktor>> GetDoktorAsync()
        {
            try
            {

                return await _databaseContext.Doktorlar.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Doktor> GetDoktorByIdAsync(int id)
        {
            try
            {

                var doktor = await _databaseContext.Doktorlar.FindAsync(id);

                return doktor;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Doktor>> QueryDoktorAsync(Func<Doktor, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Doktorlar.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveDoktorAsync(Doktor doktor)
        {
            try
            {
                var asd = await _databaseContext.Doktorlar.FindAsync(doktor.Id);
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

        public async Task<bool> UpdateDoktorAsync(Doktor doktor)
        {
            try
            {
                var tracking = _databaseContext.Update(doktor);
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
