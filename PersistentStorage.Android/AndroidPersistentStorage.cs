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
    public sealed class AndroidPersistentStorage:IPersistentStorage
    {
        private readonly Context _context;

        public AndroidPersistentStorage(Context context)
        {
            _context = context;
        }

        public IPersistentStorageGroup GetGroup(string name)
        {
            return new AndroidPersistentStorageGroup(_context, name);
        }
    }
}
