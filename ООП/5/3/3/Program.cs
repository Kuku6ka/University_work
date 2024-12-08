using System.Data;
using _3;


partial class Program
{
    static void Main()
    {
        Shop s = new Shop("Shop", "Pushkin 3");
        Product a = new Product(null, "book", 400);
                
        User you = new User();
        you.AddBallance(500);
        you.ShowBuyProducts();
    }
}