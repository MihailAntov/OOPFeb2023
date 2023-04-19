using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using RobotService;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_000_000
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateRobotRecovery_ReportFromDocumentOpenTest()
    {
        var controller = CreateObjectInstance(GetType("Controller"));
        StringBuilder sb = new StringBuilder();

        var robotArgs1 = new object[] { "K-2SO", "IndustrialAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs1)}");
        var robotArgs2 = new object[] { "T-X", "IndustrialAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs2)}");
        var robotArgs3 = new object[] { "AVA", "DomesticAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs3)}");
        var robotArgs4 = new object[] { "KUSANAGI", "IndustrialAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs4)}");
        var robotArgs5 = new object[] { "C-3PO", "DomesticAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs5)}");
        var robotArgs6 = new object[] { "R2-D2", "DomesticAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs6)}");
        var robotArgs7 = new object[] { "C1-10P", "SocialAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs7)}");
        var robotArgs8 = new object[] { "C-3PO", "DomesticAssistant" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateRobot", robotArgs8)}");

        var suppArgs1 = new object[] { "FaceRecognitionCamera" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs1)}");
        var suppArgs2 = new object[] { "SpecializedArm" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs2)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs2)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs2)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs2)}");
        var suppArgs3 = new object[] { "LaserRadar" };
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs3)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs3)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs3)}");
        sb.AppendLine($"{InvokeMethod(controller, "CreateSupplement", suppArgs3)}");

        var serviceArgs1 = new object[] { "Dishwashing", 10045, 1000 };
        sb.AppendLine($"{InvokeMethod(controller, "PerformService", serviceArgs1)}");

        var upgradeArgs1 = new object[] { "C-3PO", "SpecializedArm" };
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs1)}");
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs1)}");
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs1)}");
        var upgradeArgs2 = new object[] { "C-3PO", "LaserRadar" };
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs2)}");
        var upgradeArgs3 = new object[] { "R2-D2", "SpecializedArm" };
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs3)}");
        var upgradeArgs4 = new object[] { "KUSANAGI", "LaserRadar" };
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs4)}");
        var upgradeArgs5 = new object[] { "KUSANAGI", "SpecializedArm" };
        sb.AppendLine($"{InvokeMethod(controller, "UpgradeRobot", upgradeArgs5)}");

        var performArgs1 = new object[] { "PaintRoad", 20082, 100000 };
        sb.AppendLine($"{InvokeMethod(controller, "PerformService", performArgs1)}");
        var performArgs2 = new object[] { "DishWashing", 10045, 1000 };
        sb.AppendLine($"{InvokeMethod(controller, "PerformService", performArgs2)}");
        var performArgs3 = new object[] { "AutomotiveAssembly", 10045, 25000 };
        sb.AppendLine($"{InvokeMethod(controller, "PerformService", performArgs3)}");

        var recoveryArgs1 = new object[] { "C-3PO", 3 };
        sb.AppendLine($"{InvokeMethod(controller, "RobotRecovery", recoveryArgs1)}");
        var recoveryArgs2 = new object[] { "KUSANAGI", 3 };
        sb.AppendLine($"{InvokeMethod(controller, "RobotRecovery", recoveryArgs2)}");

        sb.AppendLine($"{InvokeMethod(controller, "Report", null)}");

        var actualResult = sb.ToString().TrimEnd();

        var output = new StringBuilder();

        output.AppendLine("IndustrialAssistant K-2SO is created and added to the RobotRepository.");
        output.AppendLine("IndustrialAssistant T-X is created and added to the RobotRepository.");
        output.AppendLine("DomesticAssistant AVA is created and added to the RobotRepository.");
        output.AppendLine("IndustrialAssistant KUSANAGI is created and added to the RobotRepository.");
        output.AppendLine("DomesticAssistant C-3PO is created and added to the RobotRepository.");
        output.AppendLine("DomesticAssistant R2-D2 is created and added to the RobotRepository.");
        output.AppendLine("Robot type SocialAssistant cannot be created.");
        output.AppendLine("DomesticAssistant C-3PO is created and added to the RobotRepository.");
        output.AppendLine("FaceRecognitionCamera is not compatible with our robots.");
        output.AppendLine("SpecializedArm is created and added to the SupplementRepository.");
        output.AppendLine("SpecializedArm is created and added to the SupplementRepository.");
        output.AppendLine("SpecializedArm is created and added to the SupplementRepository.");
        output.AppendLine("SpecializedArm is created and added to the SupplementRepository.");
        output.AppendLine("LaserRadar is created and added to the SupplementRepository.");
        output.AppendLine("LaserRadar is created and added to the SupplementRepository.");
        output.AppendLine("LaserRadar is created and added to the SupplementRepository.");
        output.AppendLine("LaserRadar is created and added to the SupplementRepository.");
        output.AppendLine("Unable to perform service, 10045 not supported!");
        output.AppendLine("C-3PO is upgraded with SpecializedArm.");
        output.AppendLine("C-3PO is upgraded with SpecializedArm.");
        output.AppendLine("All C-3PO are already upgraded!");
        output.AppendLine("C-3PO is upgraded with LaserRadar.");
        output.AppendLine("R2-D2 is upgraded with SpecializedArm.");
        output.AppendLine("KUSANAGI is upgraded with LaserRadar.");
        output.AppendLine("KUSANAGI is upgraded with SpecializedArm.");
        output.AppendLine("PaintRoad cannot be executed! 70000 more power needed.");
        output.AppendLine("DishWashing is performed successfully with 1 robots.");
        output.AppendLine("AutomotiveAssembly is performed successfully with 2 robots.");
        output.AppendLine("Robots fed: 0");
        output.AppendLine("Robots fed: 1");
        output.AppendLine("IndustrialAssistant K-2SO:");
        output.AppendLine("--Maximum battery capacity: 40000");
        output.AppendLine("--Current battery level: 40000");
        output.AppendLine("--Supplements installed: none");
        output.AppendLine("IndustrialAssistant T-X:");
        output.AppendLine("--Maximum battery capacity: 40000");
        output.AppendLine("--Current battery level: 40000");
        output.AppendLine("--Supplements installed: none");
        output.AppendLine("DomesticAssistant AVA:");
        output.AppendLine("--Maximum battery capacity: 20000");
        output.AppendLine("--Current battery level: 20000");
        output.AppendLine("--Supplements installed: none");
        output.AppendLine("IndustrialAssistant KUSANAGI:");
        output.AppendLine("--Maximum battery capacity: 25000");
        output.AppendLine("--Current battery level: 15000");
        output.AppendLine("--Supplements installed: 20082 10045");
        output.AppendLine("DomesticAssistant C-3PO:");
        output.AppendLine("--Maximum battery capacity: 10000");
        output.AppendLine("--Current battery level: 10000");
        output.AppendLine("--Supplements installed: 10045");
        output.AppendLine("DomesticAssistant R2-D2:");
        output.AppendLine("--Maximum battery capacity: 10000");
        output.AppendLine("--Current battery level: 9000");
        output.AppendLine("--Supplements installed: 10045");
        output.AppendLine("DomesticAssistant C-3PO:");
        output.AppendLine("--Maximum battery capacity: 5000");
        output.AppendLine("--Current battery level: 5000");
        output.AppendLine("--Supplements installed: 10045 20082");

        var expectedResult = output.ToString().TrimEnd();

        Assert.AreEqual(expectedResult, actualResult);
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

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name.Contains(name));

        return type;
    }
}