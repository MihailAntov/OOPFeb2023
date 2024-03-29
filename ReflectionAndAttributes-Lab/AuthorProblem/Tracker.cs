﻿using System.Reflection;

namespace AuthorProblem;

public class Tracker
{
    

    public void PrintMethodsByAuthor()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        Type type = assembly.GetType("StartUp");
        MethodInfo[] methods = type.GetMethods();

        foreach(var method in methods)
        {
            
            
            if(method.CustomAttributes.Any(a=>a.AttributeType == typeof(AuthorAttribute)))
            {
                var attributes = method.GetCustomAttributes(false);
                foreach (AuthorAttribute attribute in attributes)
                {
                    Console.WriteLine($"{method.Name} is written by {attribute.Name}");
                }
                
            }
        }
    }
}