using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Android.Content;
using PersistentStorage;

namespace PersistentStorage.Android
{

    public sealed class AndroidPersistentStorageGroup : PersistentStorageGroupSynchronousBase
    {
        private readonly Context _context;
        private readonly string _name;
        private readonly ISharedPreferences _prefs;

        public AndroidPersistentStorageGroup(Context context, string name)
        {
            _context = context;
            _name = name;

            _prefs = context.GetSharedPreferences(name, FileCreationMode.Private);
        }

        public override void Delete(string key)
        {
            _prefs.Edit().PutString(key, null).Commit();
        }

        public override T Get<T>(string key, T defaultValue)
        {
            var result = _prefs.GetString(key, null);

            if (result == null)
                return defaultValue;

            try
            {
                return DeserializeObject<T>(result);
            }
            catch
            {
                return defaultValue;
            }
        }

        public override void Put<T>(string key, T value)
        {
            var serialized = SerializeObject(value);

            _prefs.Edit().PutString(key, serialized).Commit();
        }

        // taken from http://stackoverflow.com/questions/2861722/binary-serialization-and-deserialization-without-creating-files-via-strings
        internal static string SerializeObject<T>(T o)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        // taken from http://stackoverflow.com/questions/2861722/binary-serialization-and-deserialization-without-creating-files-via-strings
        internal static T DeserializeObject<T>(string str)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(str)))
            {
                Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
                return (T)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
