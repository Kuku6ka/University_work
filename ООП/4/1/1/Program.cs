using _1;

partial class Program
{
    static void Main()
    {
        complex one = new complex(12, 12);
        var (i, j) = one.ToExponentialForm();
        Console.WriteLine(complex.FromExponentialForm(i, j));
    }
}