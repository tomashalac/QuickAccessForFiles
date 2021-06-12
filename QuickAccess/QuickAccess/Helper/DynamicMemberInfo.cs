using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QuickAccess.Helper {
    internal class DynamicMemberInfo {
        private const BindingFlags FlagsInstance = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        private readonly FieldInfo field;
        private readonly PropertyInfo propery;

        internal DynamicMemberInfo(FieldInfo field) {
            this.field = field;
        }

        internal DynamicMemberInfo(PropertyInfo propertyInfo) {
            this.propery = propertyInfo;
        }

        internal MemberInfo Member {
            get {
                if (field != null)
                    return field;
                return propery;
            }
        }

        internal Type GetMemberType() {
            if (field != null)
                return field.FieldType;
            if (propery != null)
                return propery.PropertyType;
            return null;
        }

        internal object GetValue(object obj) {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (field != null)
                return field.GetValue(obj);

            return propery.GetValue(obj, null);
        }

        internal void SetValue(object obj, object value) {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (field != null) {
                field.SetValue(obj, value);
                return;
            }

            propery.SetValue(obj, value, null);
        }

        internal ATTRIBUTE GetAttribute<ATTRIBUTE>() where ATTRIBUTE : Attribute {
            object[] list = Member.GetCustomAttributes(typeof(ATTRIBUTE), false);
            if (list == null || list.Length == 0)
                return null;

            return list.First<object>() as ATTRIBUTE;
        }

        internal static List<DynamicMemberInfo> GetListFieldsCanSave(Type type) {
            var fields = new List<DynamicMemberInfo>();

            foreach (FieldInfo field in type.GetFields(FlagsInstance)) {
                fields.Add(new DynamicMemberInfo(field));
            }

            foreach (PropertyInfo prop in type.GetProperties(FlagsInstance)) {
                fields.Add(new DynamicMemberInfo(prop));
            }

            return fields;
        }

    }
}