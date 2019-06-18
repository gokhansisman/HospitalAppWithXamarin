using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface INesneRepository
    {
        Task<List<Nesne>> GetNesneAsync();

        Task<Nesne> GetNesneByIdAsync(int id);

        Task<bool> AddNesneAsync(Nesne nesne);

        Task<bool> UpdateNesneAsync(Nesne nesne);

        Task<bool> RemoveNesneAsync(Nesne nesne);

        Task<List<Nesne>> QueryNesneAsync(Func<Nesne, bool> predicate);
    }
}
