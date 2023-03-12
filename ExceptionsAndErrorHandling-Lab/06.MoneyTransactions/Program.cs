using System;
using System.Collections.Generic;

string[] accounts = Console.ReadLine()
    .Split(",");

Dictionary<int, decimal> balances = new Dictionary<int, decimal>();
HashSet<string> commands = new HashSet<string> { "Deposit", "Withdraw" };
foreach(string account in accounts)
{
    string[] accountArgs = account.Split("-");
    int accountNumber = int.Parse(accountArgs[0]);
    decimal balance = decimal.Parse(accountArgs[1]);

    balances.Add(accountNumber, balance);
}


string input;

while((input = Console.ReadLine())!= "End")
{
    try
    {
        string[] commandArgs = input.Split(" ");
        string command = commandArgs[0];
        int accountNumber = int.Parse(commandArgs[1]);
        decimal sum = decimal.Parse(commandArgs[2]);

        if(!commands.Contains(command))
        {
            throw new InvalidOperationException("Invalid command!");
        }

        if(!balances.ContainsKey(accountNumber))
        {
            throw new ArgumentException("Invalid account!");
        }

        if(command == "Deposit")
        {
            balances[accountNumber] += sum;
        }
        else if (command == "Withdraw")
        {
            if (balances[accountNumber] - sum < 0)
            {
                throw new InvalidOperationException("Insufficient balance!");
            }
            balances[accountNumber] -= sum;
        }

        Console.WriteLine($"Account {accountNumber} has new balance: {balances[accountNumber]:f2}");
    }
    catch(InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.WriteLine("Enter another command");
    }
}