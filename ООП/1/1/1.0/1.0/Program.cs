using System;

class Program
{
    static void Main()
    {
           tablet();

           byte a = 125;
           byte b = 145;
           a.ne_uavnie_to_int();
           b.uavnie_to_int();
            
           Class c = new Class();
           Class d = new Class();
           c.uavnie_to_obj();
           d.ne_uavnie_to_obj();
           
           Cat e = new Cat();
           Cat f = new Cat();
           e.is_to_Animal();
           f.as_to_Animal();
           
           Money g = new Money{money = 100};
           double x1 = (double)g;
           double x2 = 11;
           Money h = x2;

           string i = "155";
           int j = int.Parse(i);
           int k;
           bool result = int.TryParse(i, out k);
           if (result)
           {
               Console.WriteLine("преобразование выполнено без ошибок");
           }
           else
           {
               Console.WriteLine("преобразование завершилось неудачей");
           }
           

           Console.ReadKey();
    }

    static void tablet()
    {
        Console.WriteLine("byte -> short, ushort, int, uint, long, ulong, float, double, decimal");
        Console.WriteLine("sbyte -> short, int, long, float, double, decimal");
        Console.WriteLine("sbyte -> short, int, long, float, double, decimal");
        Console.WriteLine("short -> int, long, float, double, decimal");
        Console.WriteLine("ushort -> int, uint, long, ulong, float, double, decimal");
        Console.WriteLine("int -> long, float, double, decimal");
        Console.WriteLine("uint -> long, ulong, float, double, decimal");
        Console.WriteLine("long -> float, double, decimal");
        Console.WriteLine("ulong -> float, double, decimal");
        Console.WriteLine("float -> double");
        Console.WriteLine("char -> ushort, int, uint, long, ulong, float, double, decimal");
        Console.WriteLine("\n"); 
    }
}