using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IDoktorRepository
    {
        Task<List<Doktor>> GetDoktorAsync();

        Task<Doktor> GetDoktorByIdAsync(int id);

        Task<bool> AddDoktorAsync(Doktor doktor);

        Task<bool> UpdateDoktorAsync(Doktor doktor);

        Task<bool> RemoveDoktorAsync(Doktor doktor);

        Task<List<Doktor>> QueryDoktorAsync(Func<Doktor, bool> predicate);
    }
}
