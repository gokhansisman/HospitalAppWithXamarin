using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IKullaniciRepository
    {
        Task<List<Kullanici>> GetKullaniciAsync();

        Task<Kullanici> GetKullaniciByIdAsync(int id);

        Task<bool> AddKullaniciAsync(Kullanici kullanici);

        Task<bool> UpdateKullaniciAsync(Kullanici kullanici);

        Task<bool> RemoveKullaniciAsync(Kullanici kullanici);

        Task<List<Kullanici>> QueryKullaniciAsync(Func<Kullanici, bool> predicate);
    }
}
