using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IHastaneRepository
    {
        Task<List<Hastane>> GetHastaneAsync();

        Task<Hastane> GetHastaneByIdAsync(int id);

        Task<bool> AddHastaneAsync(Hastane hastane);

        Task<bool> UpdateHastaneAsync(Hastane hastane);

        Task<bool> RemoveHastaneAsync(Hastane hastane);

        Task<List<Hastane>> QueryHastaneAsync(Func<Hastane, bool> predicate);
    }
}
