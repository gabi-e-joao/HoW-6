using Materias.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materias.Services
{
    public interface IMateriaStore
        <T>
    {
        Task<bool> AddItemAsync(T materia);
        Task<bool> UpdateItemAsync(T materia);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(Status status);
        Task<int> GetNewId();

    }
}
