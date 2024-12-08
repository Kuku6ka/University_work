namespace _1;

struct complex
{
    //Значения комплексного числа
    private double real { get; set; }
    private double imaginary { get; set; }

    //Конструктор комплексного числа
    public complex(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    //Переопределение метода ToString
    public override string ToString()
    {
        return $"{real} + {imaginary}i";
    }
    
    //Модуль комплексного числа
    public double Magnitude()
    {
        return Math.Sqrt(this.real * this.real + this.imaginary * this.imaginary);
    }

    public (double Magnitude, double Angle) ToExponentialForm()
    {
        double magnitude = Math.Sqrt(real * real + imaginary * imaginary);
        double angle = Math.Atan2(imaginary, real);
        return (magnitude, angle);
    }
    
    public static complex FromExponentialForm(double magnitude, double angle)
    {
        double real = magnitude * Math.Cos(angle);
        double imaginary = magnitude * Math.Sin(angle);
        return new complex(real, imaginary);
    }

    //Переопределение операторов
    public static complex operator +(complex one, complex two)
    {
        return new complex(one.real + two.real, one.imaginary + two.imaginary);
    }

    public static complex operator -(complex one, complex two)
    {
        return new complex(one.real - two.real, one.imaginary - two.imaginary);
    }

    public static complex operator *(complex one, complex two)
    {
        return new complex(
            one.real * two.real - one.imaginary * two.imaginary,
            one.real * two.imaginary + one.imaginary * two.real
        );
    }

    public static complex operator /(complex one, complex two)
    {
        return new complex(((one.real * two.real + one.imaginary * two.imaginary) / (two.real * two.real + two.imaginary * two.imaginary)),
            ((two.real * one.imaginary - one.real * two.imaginary) / (two.real * two.real + two.imaginary * two.imaginary)));
    }

    public static bool operator ==(complex one, complex two)
    {
        if (one.real == two.real && one.imaginary == two.imaginary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator !=(complex one, complex two)
    {
        if (one.real != two.real || one.imaginary != two.imaginary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}