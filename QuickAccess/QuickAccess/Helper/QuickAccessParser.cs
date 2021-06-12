using System;
using System.Collections.Generic;

namespace QuickAccess.Helper {
    internal class QuickAccessParser {

        internal static T QuickAccessToObject<T>(Dictionary<string, object> quickAccessData) {
            var instance = Activator.CreateInstance<T>();
            var fields = DynamicMemberInfo.GetListFieldsCanSave(typeof(T));

            foreach(var field in fields) {
                var atribute = field.GetAttribute<QuickAccessAttribute>();
                if (atribute == null)
                    continue;

                var name = atribute.Name;
                if (string.IsNullOrWhiteSpace(name))
                    name = field.Member.Name;

                if (quickAccessData.ContainsKey(name)) {
                    field.SetValue(instance, quickAccessData[name]);
                }
            }

            return instance;
        }


        internal static Dictionary<string, object> ObjectToQuickAccess(object toSave) {
            var quickAccess = new Dictionary<string, object>();


            foreach (DynamicMemberInfo member in DynamicMemberInfo.GetListFieldsCanSave(toSave.GetType())) {
                QuickAccessAttribute quickAccessAtribute = member.GetAttribute<QuickAccessAttribute>();
                if (quickAccessAtribute == null)
                    continue;

                object value;
                try {
                    value = member.GetValue(toSave);
                } catch (Exception e) {
                    Console.WriteLine("Exception in type: " + toSave.GetType() + " Member name: " + member.Member.Name +
                        ".\n" + e.ToString());
                    continue;
                }

                string name = quickAccessAtribute.Name;

                if (string.IsNullOrWhiteSpace(name))
                    name = member.Member.Name;

                quickAccess.Add(name, value);
            }

            return quickAccess;
        }


    }
}
