
using System;
using System.Linq;
using System.Text;





public class Quick
{
    public static void Sort(int[] a)
    {
        Sort(a, 0, a.Length - 1);
    }

    private static void Sort(int[] a, int lo, int hi) 
    {
        if(lo >= hi)
        {
            return;
        }

        int p = Partition(a, lo, hi);
        Sort(a, lo, p - 1);
        Sort(a, p + 1, hi);
    }

    private static int Partition(int[]a, int lo, int hi)
    {
        if(lo >= hi)
        {
            return lo;
        }

        int i = lo;
        int j = hi + 1;
        while(true)
        {
            while ( a[i] < a[lo])
            {
                i++;
            }

            while (a[i] < a[lo])
            {
                i++;
            }
        }
    }
}

