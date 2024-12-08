namespace _2;

struct vector
{
    public double x { get; }
    public double y { get; }
    public double z { get; }
    
    // Конструктор
    public vector(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    //Модуль
    public double Magnitude()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public override string ToString()
    {
        return $"({x}; {y}; {z})";
    }

    //Перегразка операторов
    public static vector operator +(vector a, vector b)
    {
        return new vector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static vector operator -(vector a, vector b)
    {
        return new vector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    //скалярное произведение
    public double Scalar(vector b)
    {
        return (this.x * b.x + this.y * b.y + this.z * b.z);
    }

    public static vector operator *(vector a, vector b)
    {
        double crossX = a.y * b.z - a.z * b.y;
        double crossY = a.z * b.x - a.x * b.z;
        double crossZ = a.x * b.y - a.y * b.x;
        return new vector(crossX, crossY, crossZ);
    }

    public static vector operator /(vector a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException();
        }
        
        return new vector(a.x / b, a.y / b, a.z / b);
    }

    public static bool operator ==(vector a, vector b)
    {
        if (a.x == b.x && a.y == b.y && a.z == b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(vector a, vector b)
    {
        if (a.x != b.x || a.y != b.y || a.z != b.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static vector operator *(double scalar, vector a)
    {
        return new vector(a.x * scalar, a.y * scalar, a.z * scalar);
    }

    public static vector operator *(vector a, double scalar)
    {
        return new vector(a.x * scalar, a.y * scalar, a.z * scalar);
    }
}