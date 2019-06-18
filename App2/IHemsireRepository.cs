using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public interface IHemsireRepository
    {
        Task<List<Hemsire>> GetHemsireAsync();

        Task<Hemsire> GetHemsireByIdAsync(int id);

        Task<bool> AddHemsireAsync(Hemsire hemsire);

        Task<bool> UpdateHemsireAsync(Hemsire hemsire);

        Task<bool> RemoveHemsireAsync(Hemsire hemsire);

        Task<List<Hemsire>> QueryHemsireAsync(Func<Hemsire, bool> predicate);
    }
}
