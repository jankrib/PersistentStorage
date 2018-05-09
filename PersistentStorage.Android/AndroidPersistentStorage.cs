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
        private static Context _context;

        public static void SetContext(Context context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
        }

        public AndroidPersistentStorage()
        {
            if(_context == null)
                throw new InvalidOperationException($"{nameof(SetContext)} must be called before instantiating {nameof(AndroidPersistentStorage)}");
            
        }

        public IPersistentStorageGroup GetGroup(string name)
        {
            return new AndroidPersistentStorageGroup(_context, name);
        }
    }
}
