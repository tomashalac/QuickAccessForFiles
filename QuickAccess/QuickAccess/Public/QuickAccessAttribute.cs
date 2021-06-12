using System;

namespace QuickAccess {

    /// <summary>
    /// This allows you to save certain attributes separately from the main object,
    /// so that they are available without having to load the entire file.
    /// It is used for listing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class QuickAccessAttribute : Attribute {
        /// <summary>
        /// The name with which the field will be saved.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// So the field is available with quick access.
        /// </summary>
        /// <param name="name">The name with which the field will be saved.</param>
        public QuickAccessAttribute(string name) {
            this.Name = name;
        }

        /// <summary>
        /// So the field is available with quick access.
        /// </summary>
        public QuickAccessAttribute() { }
    }
}