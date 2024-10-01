public class Class
{

}

public class Animal
{

}

public class Cat: Animal
{

}

public class Money
{
    public double money;
    
    public static implicit operator Money(double x)
    {
        return new Money{money = x};
    }

    public static explicit operator double(Money x)
    {
        return x.money;
    }
}


public static class Transform
{
    public static int uavnie_to_int(this byte input)
    {
        return (int)input;
    }

    public static int ne_uavnie_to_int(this byte input)
    {
        int a = input;
        return a;
    }

    public static object ne_uavnie_to_obj(this Class c)
    {
        object a = c;
        return a;
    }

    public static object uavnie_to_obj(this Class c)
    {
        return (object)c;
    }

    public static Animal is_to_Animal(this Cat d)
    {
        if(d is Animal)
        {
            return new Animal();
        }
        else
        {
            throw new InvalidCastException("класс не является Animal");
        }
    }

    public static Animal as_to_Animal(this Cat d)
    {
        Animal animal = d as Animal;
        if (animal == null)
        {
            throw new InvalidCastException("класс не является Animal");
        }
        return animal;
    }
}