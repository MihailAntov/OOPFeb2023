using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Utils
{
    public class Validator
    {
        public static bool IsValid(object obj)
        {
            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties()
                .Where(p=>p.CustomAttributes.Any(
                    a=>typeof(MyValidationAttribute).IsAssignableFrom(a.AttributeType)))
                .ToArray();

            foreach(PropertyInfo property in properties)
            {
                IEnumerable<MyValidationAttribute> attributes =
                    property.GetCustomAttributes()
                        .Where(ca => typeof(MyValidationAttribute)
                        .IsAssignableFrom(ca.GetType()))
                        .Select(ca => (MyValidationAttribute)ca);

                foreach(MyValidationAttribute attribute in attributes)
                {
                    if(!attribute.IsValid(property.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
