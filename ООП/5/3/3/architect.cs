using System;

namespace _3;

public interface IUser
{
    string Name { get; }
    
    int Ballance { get; set; }

    void AddBallance(int a);

    void ShowAllProducts();

    void SearchProducts(int ID);

    void BuyProduct(int ID, IUser user, IShop shop);

    void ShowBuyProducts();
}

public interface IShop
{
    string Name { get; set; }
    string Address { get; set; }

    void AddProduct(IProduct product);

    void RemoveProduct(IProduct product);
}

public interface IProduct
{
    int? ID { get; }
    string Name { get; set; }
    int Price { get; set; }
}

public interface ICheck
{
    IShop Shop { get; set; }
    IProduct Product { get; set; }
    DateTime DataTime { get; set; }
}
