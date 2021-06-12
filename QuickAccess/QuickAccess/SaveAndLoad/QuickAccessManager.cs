using QuickAccess.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuickAccess.SaveAndLoad {
    internal class QuickAccessManager : SaveAndLoad<Dictionary<string, object>> {

        internal QuickAccessManager(Stream stream) : base(stream) { }

        internal override Dictionary<string, object> Load() {
            var sizeOfBlockBuffer = new byte[4];
            stream.Read(sizeOfBlockBuffer);

            var sizeOfBlock = BitConverter.ToUInt16(sizeOfBlockBuffer);

            var buffer = new byte[sizeOfBlock];
            stream.Read(buffer);
            return BinaryHelper.ByteArrayToObject<Dictionary<string, object>>(buffer);
        }

        internal override void Save(Dictionary<string, object> toSave) {
            var buffer = BinaryHelper.ObjectToByteArray(toSave);

            var sizeOfBlock = BitConverter.GetBytes((uint)buffer.Length);

            //write size of block
            stream.Write(sizeOfBlock);

            stream.Write(buffer);
        }

        internal override void Skip() {
            byte[] headerSize = new byte[sizeof(uint)];
            stream.Read(headerSize);
            stream.Position += BitConverter.ToUInt32(headerSize, 0);
        }
    }
}
