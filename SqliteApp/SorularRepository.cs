using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class SorularRepository : ISorularRepository
    {
        private readonly DatabaseContext _databaseContext;

        public SorularRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddSoruAsync(Sorular soru)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Sorular>(soru);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<List<Sorular>> GetSorularAsync()
        {
            try
            {

                return await _databaseContext.Sorularim.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Sorular> GetSoruByIdAsync(int id)
        {
            try
            {

                var soru = await _databaseContext.Sorularim.FindAsync(id);

                return soru;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Sorular>> QuerySoruAsync(Func<Sorular, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Sorularim.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveSoruAsync(Sorular soru)
        {
            try
            {
                var asd = await _databaseContext.Sorularim.FindAsync(soru.Id);
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

        public async Task<bool> UpdateSoruAsync(Sorular soru)
        {
            try
            {
                var tracking = _databaseContext.Update(soru);
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
