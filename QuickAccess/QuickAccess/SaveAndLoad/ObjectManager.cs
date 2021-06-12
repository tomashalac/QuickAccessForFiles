using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace QuickAccess.SaveAndLoad {
    internal class ObjectManager<T> : SaveAndLoad<T> {

        internal ObjectManager(Stream stream) : base(stream) { }

        internal override T Load() {
            BinaryFormatter bf = new BinaryFormatter();
            return (T)bf.Deserialize(stream);
        }

        internal override void Save(T toSave) {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, toSave);
        }

        internal override void Skip() => throw new NotImplementedException();
    }
}
