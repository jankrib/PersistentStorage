using System;
using System.Threading.Tasks;

namespace PersistentStorage
{
    public abstract class PersistentStorageGroupSynchronousBase : IPersistentStorageGroup
    {
        public abstract void Delete(string key);

        public async Task DeleteAsync(string key)
        {
            await Task.Run(() => Delete(key)).ConfigureAwait(false);
        }

        public abstract T Get<T>(string key, T defaultValue);

        public async Task<T> GetAsync<T>(string key, T defaultValue)
        {
            return await Task.Run(() => Get<T>(key, defaultValue)).ConfigureAwait(false);
        }

        public abstract void Put<T>(string key, T value);

        public async Task PutAsync<T>(string key, T value)
        {
            await Task.Run(() => Put(key, value)).ConfigureAwait(false);
        }
    }
}
