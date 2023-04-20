using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldNames)
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine($"Class under investigation: {className}");

            Type type = Assembly.GetExecutingAssembly().GetType(className);

            Object classInstance = Activator.CreateInstance(type);
            FieldInfo[] fields = type.GetFields();

            foreach(var field in fields)
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Assembly.GetExecutingAssembly().GetType(className);

            Object instance = Activator.CreateInstance(type);

            FieldInfo[] publicFields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] classPublicMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] classNonPublicMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            foreach(FieldInfo field in publicFields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach(MemberInfo method in classNonPublicMethods.Where(m=>m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} must be public!");

            }

            foreach (MemberInfo method in classPublicMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} must be private!");

            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            Type type = Assembly.GetExecutingAssembly().GetType(className);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"All Private Methods of Class: { className}");
            sb.AppendLine($"Base Class: {type.BaseType.Name}");

            MethodInfo[] privateMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public );
            foreach(var method in privateMethods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString().TrimEnd();

        }

        public string CollectGettersAndSetters(string className)
        {
            Type type = Assembly.GetExecutingAssembly().GetType(className);
            StringBuilder sb = new StringBuilder();
            MethodInfo[] classMethods = type.GetMethods();

            foreach(var method in classMethods.Where(m=>m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");               
            }

            foreach (var method in classMethods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
