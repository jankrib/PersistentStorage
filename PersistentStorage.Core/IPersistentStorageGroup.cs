using System;
using System.Threading.Tasks;

namespace PersistentStorage
{

    public interface IPersistentStorageGroup
    {

        T Get<T>(string key, T defaultValue);
        void Put<T>(string key, T value);
        void Delete(string key);

        Task<T> GetAsync<T>(string key, T defaultValue);
        Task PutAsync<T>(string key, T value);
        Task DeleteAsync(string key);
    }
}
