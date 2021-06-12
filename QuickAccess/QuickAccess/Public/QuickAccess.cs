using QuickAccess.Helper;
using QuickAccess.SaveAndLoad;
using System;
using System.Globalization;
using System.IO;

namespace QuickAccess {
    public static class QuickAccess {

        /// <summary>
        /// Loads all the information that was saved in the file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static T Load<T>(string fullFileName) {
            CheckFileExist(fullFileName);

            using Stream stream = File.Open(fullFileName, FileMode.Open);
            var version = new HeaderManager(stream).Load();
            if (version == 0)
                throw new ArgumentOutOfRangeException("This file is not valid to load.");

            new QuickAccessManager(stream).Skip();

            return new ObjectManager<T>(stream).Load();
        }

        /// <summary>
        /// Loads only de Quick Access Data from the file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public static T LoadQuickData<T>(string fullFileName) {
            CheckFileExist(fullFileName);

            using Stream stream = File.Open(fullFileName, FileMode.Open);
            var version = new HeaderManager(stream).Load();
            if (version == 0)
                throw new ArgumentOutOfRangeException("This file is not valid to load.");

            var quickData = new QuickAccessManager(stream).Load();
            return QuickAccessParser.QuickAccessToObject<T>(quickData);
        }

        /// <summary>
        /// Save the object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullFileName"></param>
        /// <param name="toSave"></param>
        public static void Save<T>(string fullFileName, T toSave) {
            var quickAccessData = QuickAccessParser.ObjectToQuickAccess(toSave);


            using Stream stream = File.Open(fullFileName, FileMode.Create);
            new HeaderManager(stream).Save(Program.FileVersion);
            new QuickAccessManager(stream).Save(quickAccessData);
            new ObjectManager<T>(stream).Save(toSave);
        }


        private static void CheckFileExist(string fullFileName) {
            if (!File.Exists(fullFileName))
                throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture,
                    "The file to load does not exist!, File path: {0}", fullFileName));
        }

    }
}
