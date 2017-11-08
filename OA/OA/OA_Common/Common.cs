using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Common
{
    public static class Common
    {
        /// <summary>
        /// 获取后台所有action
        /// </summary>
        /// <returns></returns>
        public static List<string> GetALLPageByReflection()
        {
            List<string> actions = new List<string>();
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load("OA_Web");
            System.Collections.Generic.List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                string s = type.FullName.ToLower();
                if (type.Name.StartsWith("AccountCont")) continue;
                if (s.StartsWith("oa_web.controllers.admin.") && s.EndsWith("controller") && !s.Contains("base"))
                    typeList.Add(type);
            }
            typeList.Sort(delegate(Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            foreach (Type type in typeList)
            {
                //Response.Write(type.Name.Replace("Controller","") + "<br/>\n");
                System.Reflection.MemberInfo[] members = type.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly,
                Type.FilterName, "*");
                foreach (var m in members)
                {
                    if (m.DeclaringType.Attributes.HasFlag(System.Reflection.TypeAttributes.Public) != true)
                        continue;
                    string controller = type.Name.Replace("Controller", "");
                    string action = m.Name;
                    string url = "/" + controller + "/" + action;
                    if (!action.Contains("<") && !controller.Contains("Base"))
                    {
                        actions.Add(url);
                    }
                }
            }
            return actions;
        }

        /// <summary>
        /// 获取前台所有action
        /// </summary>
        /// <returns></returns>
        public static List<string> GetALLUserPageByReflection()
        {
            List<string> actions = new List<string>();
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load("OA_Web");
            System.Collections.Generic.List<Type> typeList = new List<Type>();
            var types = asm.GetTypes();
            foreach (Type type in types)
            {
                string s = type.FullName.ToLower();
                if (type.Name.StartsWith("AccountCont")) continue;
                if (!s.StartsWith("oa_web.controllers.admin") && s.StartsWith("oa_web.controllers.") && s.EndsWith("controller") && !s.Contains("base") && !s.Contains("mycommon"))
                    typeList.Add(type);
            }
            typeList.Sort(delegate(Type type1, Type type2) { return type1.FullName.CompareTo(type2.FullName); });
            foreach (Type type in typeList)
            {
                //Response.Write(type.Name.Replace("Controller","") + "<br/>\n");
                System.Reflection.MemberInfo[] members = type.FindMembers(System.Reflection.MemberTypes.Method,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.DeclaredOnly,
                Type.FilterName, "*");
                foreach (var m in members)
                {
                    if (m.DeclaringType.Attributes.HasFlag(System.Reflection.TypeAttributes.Public) != true)
                        continue;
                    string controller = type.Name.Replace("Controller", "");
                    string action = m.Name;
                    string url = "/" + controller + "/" + action;
                    if (!action.Contains("<") && !controller.Contains("Base"))
                    {
                        actions.Add(url);
                    }
                }
            }
            return actions;
        }
    }
}
