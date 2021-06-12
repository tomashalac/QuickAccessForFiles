using System;

namespace QuickAccess {

    /// <summary>
    /// [HEADER("title for file type", build verions: short(2bytes) )]
    /// [QUICK ACCESS(dynamic key-value structure, string-object type)]
    /// [DATA]
    /// </summary>
    class Program {

        //No modificar estas const, sino se rompe la carga de archivos viejos!
        internal const string FilesHeader = "QuickAccessHeader";

        /// <summary>
        /// The version of the format that was used to create the file
        /// (change if file format is modified)
        /// </summary>
        internal const short FileVersion = 1;

        static void Main(string[] args) {
            throw new NotImplementedException();
        }

    }
}
