using System;


int n = int.Parse(Console.ReadLine());

Console.WriteLine(Factorial(n));

int Factorial(int n)
{
    if(n == 1)
    {
        return 1;
    }

    return n * Factorial(n - 1);
}