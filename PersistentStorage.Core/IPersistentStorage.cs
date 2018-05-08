using System;
using System.Threading.Tasks;

namespace PersistentStorage
{

    public interface IPersistentStorage
    {
        IPersistentStorageGroup GetGroup(string name);
    }
}
