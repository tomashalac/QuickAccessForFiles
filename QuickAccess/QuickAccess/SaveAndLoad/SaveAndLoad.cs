using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuickAccess.SaveAndLoad {
    internal abstract class SaveAndLoad<T> {

        protected readonly Stream stream;

        public SaveAndLoad(Stream stream) {
            this.stream = stream;
        }

        internal abstract void Skip();
        internal abstract void Save(T toSave);
        internal abstract T Load();

    }
}
