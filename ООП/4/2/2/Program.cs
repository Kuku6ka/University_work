using _2;

partial class Program
{
    static void Main()
    {
        double mass = 70;
        vector a = new vector(0, -7, 0);
        double shirot = 45.0;
        double EarthAngularVelocity = 7.2921e-5;
        
        double shirotRadian = shirot * Math.PI / 180.0;
        
        vector omega = new vector(
            EarthAngularVelocity * Math.Cos(shirotRadian),
            0,
            EarthAngularVelocity * Math.Sin(shirotRadian)
        );
        
        Console.WriteLine(2 * mass * (a * omega));
    }
}