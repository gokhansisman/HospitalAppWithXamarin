using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface ISorularRepository
    {
        Task<List<Sorular>> GetSorularAsync();

        Task<Sorular> GetSoruByIdAsync(int id);

        Task<bool> AddSoruAsync(Sorular soru);

        Task<bool> UpdateSoruAsync(Sorular soru);

        Task<bool> RemoveSoruAsync(Sorular soru);

        Task<List<Sorular>> QuerySoruAsync(Func<Sorular, bool> predicate);
    }
}
