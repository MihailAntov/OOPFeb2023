using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using OnlineShop;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Test_000_004
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void BuyComputer_ShouldReturnCorrectResult()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var computerArguments = new object[] { "DesktopComputer", 1, "Asus", "ROG", 500M };
        InvokeMethod(controller, "AddComputer", computerArguments);

        var peripheralArguments = new object[] { 1, 1, "Headset", "Razer", "Thresher", 300M, 70, "AUX" };
        InvokeMethod(controller, "AddPeripheral", peripheralArguments);

        var componentArguments = new object[] { 1, 3, "CentralProcessingUnit", "Intel", "Xeon", 1600M, 82, 9 };
        InvokeMethod(controller, "AddComponent", componentArguments);

        var buyComputerArguments = new object[] { 1 };
        var validActualResult = InvokeMethod(controller, "BuyComputer", buyComputerArguments);

        var validExpectedResultOne = "Overall Performance: 117.50. Price: 2400.00 - DesktopComputer: Asus ROG (Id: 1) Components (1):  Overall Performance: 102.50. Price: 1600.00 - CentralProcessingUnit: Intel Xeon (Id: 3) Generation: 9 Peripherals (1); Average Overall Performance (70.00):  Overall Performance: 70.00. Price: 300.00 - Headset: Razer Thresher (Id: 1) Connection Type: AUX";
        var validExpectedResultTwo = "Overall Performance: 117.5. Price: 2400 - DesktopComputer: Asus ROG (Id: 1) Components (1):  Overall Performance: 102.5. Price: 1600 - CentralProcessingUnit: Intel Xeon (Id: 3) Generation: 9 Peripherals (1); Average Overall Performance (70):  Overall Performance: 70. Price: 300 - Headset: Razer Thresher (Id: 1) Connection Type: AUX";

        var validExpectedResultWithoutNewLinesOne = RemoveNewLines(validExpectedResultOne);
        var validExpectedResultWithoutNewLinesTwo = RemoveNewLines(validExpectedResultTwo);
        var validActualResultWithoutNewLines = RemoveNewLines(validActualResult?.ToString());

        Assert.IsTrue(validExpectedResultWithoutNewLinesOne == validActualResultWithoutNewLines || validExpectedResultWithoutNewLinesTwo == validActualResultWithoutNewLines);
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        try
        {
            var result = obj.GetType()
                .GetMethod(methodName)
                .Invoke(obj, parameters);

            return result;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static object CreateObjectInstance(Type type, params object[] parameters)
    {
        try
        {
            var desiredConstructor = type.GetConstructors()
                .FirstOrDefault(x => x.GetParameters().Any());

            if (desiredConstructor == null)
            {
                return Activator.CreateInstance(type, parameters);
            }

            var instances = new List<object>();

            foreach (var parameterInfo in desiredConstructor.GetParameters())
            {
                var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static string RemoveNewLines(string content)
    {
        return content?.Replace("\r", "")
            ?.Replace("\n", "");
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == name);

        return type;
    }
}