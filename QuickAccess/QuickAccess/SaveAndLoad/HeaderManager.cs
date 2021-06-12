using QuickAccess.SaveAndLoad;
using System;
using System.IO;
using System.Text;

namespace QuickAccess {
    internal class HeaderManager : SaveAndLoad<short> {

        internal HeaderManager(Stream stream) : base(stream) { }

        internal override short Load() {
            byte[] headerTitle = new byte[Program.FilesHeader.Length];
            byte[] fileVersion = new byte[sizeof(short)];

            stream.Read(headerTitle);

            //header == FilesHeader
            if (Encoding.UTF8.GetString(headerTitle) != Program.FilesHeader) {
                throw new ArgumentOutOfRangeException("This file is not valid to load.");
            }

            stream.Read(fileVersion);

            return BitConverter.ToInt16(fileVersion, 0);
        }

        internal override void Save(short toSave) {
            var buffer = Encoding.UTF8.GetBytes(Program.FilesHeader);
            stream.Write(buffer);

            buffer = BitConverter.GetBytes(toSave);
            stream.Write(buffer);
        }

        internal override void Skip() {
            stream.Position += Program.FilesHeader.Length + 2;
        }
    }
}
