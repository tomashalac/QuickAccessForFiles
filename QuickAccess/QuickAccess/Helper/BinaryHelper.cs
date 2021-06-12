using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuickAccess.Helper {
    internal class BinaryHelper {

        internal static byte[] ObjectToByteArray<T>(T obj) {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        internal static T ByteArrayToObject<T>(byte[] arrBytes) {
            using MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Position = 0;
            return (T)binForm.Deserialize(memStream);
        }
    }
}