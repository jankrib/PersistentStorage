using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using PersistentStorage;
using Foundation;

namespace PersistentStorage.iOS
{

    public sealed class iOSPersistentStorageGroup : PersistentStorageGroupSynchronousBase
    {
        private readonly string _name;

        public iOSPersistentStorageGroup(string name)
        {
            _name = name;
        }

        public override void Delete(string key)
        {
            NSUserDefaults.StandardUserDefaults.RemoveObject(key);
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        public override T Get<T>(string key, T defaultValue)
        {
            var result = NSUserDefaults.StandardUserDefaults.DataForKey(_name + "_" + key);

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

            NSUserDefaults.StandardUserDefaults.SetValueForKey(serialized, new NSString(_name + "_" + key));
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        internal static NSData SerializeObject<T>(T o)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return NSData.FromArray(stream.ToArray());
            }
        }

        internal static T DeserializeObject<T>(NSData data)
        {
            using (var stream = data.AsStream())
            {
                Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
                return (T)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}
