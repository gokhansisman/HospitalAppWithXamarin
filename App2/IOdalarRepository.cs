using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IOdaRepository
    {
        Task<List<Oda>> GetOdaAsync();

        Task<Oda> GetOdaByIdAsync(int id);

        Task<bool> AddOdaAsync(Oda oda);

        Task<bool> UpdateOdaAsync(Oda oda);

        Task<bool> RemoveOdaAsync(Oda oda);

        Task<List<Oda>> QueryOdaAsync(Func<Oda, bool> predicate);
    }
}
