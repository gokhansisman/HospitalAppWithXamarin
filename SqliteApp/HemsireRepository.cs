using App2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteApp
{
    public class HemsireRepository : IHemsireRepository
    {
        private readonly DatabaseContext _databaseContext;

        public HemsireRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }



        public async Task<bool> AddHemsireAsync(Hemsire hemsire)
        {
            try
            {
                var tracking = await _databaseContext.AddAsync<Hemsire>(hemsire);
                await _databaseContext.SaveChangesAsync();

                var isAdded = tracking.State == EntityState.Added;
                return isAdded;
            }
            catch (Exception e)
            {
                return false;
            }

        }



        public async Task<List<Hemsire>> GetHemsireAsync()
        {
            try
            {

                return await _databaseContext.Hemsireler.ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<Hemsire> GetHemsireByIdAsync(int id)
        {
            try
            {

                var hemsire = await _databaseContext.Hemsireler.FindAsync(id);

                return hemsire;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Hemsire>> QueryHemsireAsync(Func<Hemsire, bool> predicate)
        {
            try
            {
                return await Task.FromResult(_databaseContext.Hemsireler.Where(predicate).ToList());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> RemoveHemsireAsync(Hemsire hemsire)
        {
            try
            {
                var asd = await _databaseContext.Hemsireler.FindAsync(hemsire.Id);
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

        public async Task<bool> UpdateHemsireAsync(Hemsire hemsire)
        {
            try
            {
                var tracking = _databaseContext.Update(hemsire);
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
