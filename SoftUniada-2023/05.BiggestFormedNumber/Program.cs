using System;
using System.Linq;
using System.Collections.Generic;

List<Number> nums = Console.ReadLine()
    .Split(" ")
    .Select(n=> new Number(int.Parse(n)))
    .ToList();

int maxLength = nums.Max(n => n.Value.ToString().Length);

nums = nums.OrderByDescending(n => n.ValueStr(maxLength)).ToList();

Console.WriteLine(string.Join("",nums.Select(n=>n.Value)));

class Number
{
    public Number(int value)
    {
        Value = value;
        
    }
    public int Value { get; set; }
    public string ValueStr(int digits)
    {
        string num = Value.ToString();
        num += new string('0', digits - num.Length);

        return num;
    }
}
