namespace WpfApp1;

public class FindGCD
{
    public static int GCD(int x, int y) {
        if (x == 0) return y;
        while (y != 0)
        {
            if (x > y) {x -= y;}
            else {y -= x;}
        }
        return x;

    }

    public static int GCD(int a, int b, int c)
    {
        int x = GCD(a, b);
        int result = GCD(x, c);
        return result;
    }

    public static int GCD(int a, int b, int c, int d)
    {
        int x = GCD(a, b, c);
        int result = GCD(x, d);
        return result;
    }

    public static int GCD(int a, int b, int c, int d, int e)
    {
        int x = GCD(a, b, c, d);
        int result = GCD(x, e);
        return result;
    }

    public static int GCD(params int[] numbers)
    {
        if (numbers.Length == 0)
        {
            throw new ArgumentException("Необходимо хотя бы одно число");
        }

        int gcd = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            gcd = GCD(gcd, numbers[i]);
        }
        return gcd;
    }

    public static int GCD(string numbersString, char delimiter)
    {
        numbersString = numbersString.Trim();
        int[] numbers = numbersString.Split(delimiter).Select(int.Parse).ToArray();
        
        return GCD(numbers);
    }
}

public class FindGCDStein
{
    public static int GCDStein(int u, int v)
    {
        int k;
        if (u == 0 || v == 0)
            return u | v;

        for (k = 0; ((u | v) & 1) == 0; ++k)
        {
            u >>= 1;
            v >>= 1;
        }

        while ((u & 1) == 0)
            u >>= 1;
        do
        {
            while ((v & 1) == 0)
                v >>= 1;

            if (u < v)
            {
                v -= u;
            }
            else
            {
                int diff = u - v;
                u = v;
                v = diff;
            }

            v >>= 1;
        } while (v != 0);

        u <<= k;
        return u;
    }
}