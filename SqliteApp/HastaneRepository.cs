using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class HastaneRepository : IHastaneRepository
    {
        private readonly DatabaseContext _databaseContext;

        public HastaneRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddHastaneAsync(Hastane hastane)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Hastane>(hastane);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<List<Hastane>> GetHastaneAsync()
        {
            try
            {

                return await _databaseContext.Hastaneler.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Hastane> GetHastaneByIdAsync(int id)
        {
            try
            {

                var hastane = await _databaseContext.Hastaneler.FindAsync(id);

                return hastane;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Hastane>> QueryHastaneAsync(Func<Hastane, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Hastaneler.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveHastaneAsync(Hastane hastane)
        {
            try
            {
                var asd = await _databaseContext.Hastaneler.FindAsync(hastane.Id);
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

        public async Task<bool> UpdateHastaneAsync(Hastane hastane)
        {
            try
            {
                var tracking = _databaseContext.Update(hastane);
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
