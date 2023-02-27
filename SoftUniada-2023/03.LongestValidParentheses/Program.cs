using System;

string input = Console.ReadLine();
bool open = false;
int counter = 0;
int highestCount = 0;

for(int i = 0; i < input.Length; i++)
{
    if(!open)
    {
        if (input[i] == '(')
        {
            open = true;
            counter++;
        }
        else
        {
            
            if(counter > highestCount)
            {
                highestCount = counter;
            }
            counter = 0;
        }
    }
    else
    {
        if(input[i] == ')')
        {
            open=false;
            counter++;
        }
        else
        {
            counter--;
            if (counter > highestCount)
            {
                highestCount = counter;
            }
            counter = 1;
            
        }
    }

}

if(open)
{
    counter--;
}

if (counter > highestCount)
{
    highestCount = counter;
}

Console.WriteLine(highestCount);
