using NUnit.Framework;
using QuickAccess;
using System;

namespace QuickAccessTests {
    public class Tests {
        
        [Test]
        public void Integration_SaveAndLoad() {
            var toSave = new ToSave {
                Title = "test text!"
            };

            for (var i = 0; i < 100; i++)
                toSave.Document += "LINE " + i;

            QuickAccess.QuickAccess.Save("test.bin", toSave);
            var load = QuickAccess.QuickAccess.Load<ToSave>("test.bin");

            Assert.NotNull(load);
            Assert.AreEqual(toSave.Title, load.Title);
            Assert.AreEqual(toSave.Document, load.Document);
        }

        [Test]
        public void Integration_SaveAndLoadQuickAccessData() {
            var toSave = new ToSave {
                Title = "test text!"
            };

            for (var i = 0; i < 100; i++)
                toSave.Document += "LINE " + i;

            QuickAccess.QuickAccess.Save("test.bin", toSave);
            var load = QuickAccess.QuickAccess.LoadQuickData<ToSave>("test.bin");

            Assert.NotNull(load);
            Assert.AreEqual(toSave.Title, load.Title);
            Assert.Null(load.Document);
        }


        [Serializable]
        private class ToSave {
            [QuickAccess]
            public string Title;

            public string Document;
        }

    }
}