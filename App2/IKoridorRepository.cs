using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IKoridorRepository
    {
        Task<List<Koridor>> GetKoridorAsync();

        Task<Koridor> GetKoridorByIdAsync(int id);

        Task<bool> AddKoridorAsync(Koridor koridor);

        Task<bool> UpdateKoridorAsync(Koridor koridor);

        Task<bool> RemoveKoridorAsync(Koridor koridor);

        Task<List<Koridor>> QueryKoridorAsync(Func<Koridor, bool> predicate);
    }
}
