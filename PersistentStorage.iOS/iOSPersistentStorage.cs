using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentStorage.iOS
{
    public sealed class iOSPersistentStorage : IPersistentStorage
    {
        public iOSPersistentStorage()
        {
        }

        public IPersistentStorageGroup GetGroup(string name)
        {
            return new iOSPersistentStorageGroup(name);
        }
    }
}
