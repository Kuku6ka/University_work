namespace _3;

class Product : IProduct
{
    public int? ID { get; private set; }
    public string Name {get; set;}
    public int Price {get; set;}

    public Product()
    {
        ID = 0;
        Name = "";
        Price = 0;
    }
    
    public Product(int? id, string name, int price)
    {
        Random rand  = new Random();
        
        Name = name;
        Price = price;

        if (id == null)
        {
            ID = rand.Next(1, int.MaxValue);    
        }
        else
        {
            ID = id;
        }
    }
    
    public override string ToString()
    {
        return $"id: {ID} \t name: {Name} \t prise: {Price}";
    }

    public void FromString(string input)
    {
        int id;
        string name;
        int price;
        
        if (input.Contains("id:"))
        {
            id = Convert.ToInt32(input.Split("id:")[1].Split("name:")[0].Trim());
            ID = id;
        }

        if (input.Contains("name:"))
        {
            name = input.Split("name:")[1].Split("prise:")[0].Trim();
            Name = name;
        }

        if (input.Contains("prise:"))
        {
            price = Convert.ToInt32(input.Split("prise:")[1].Trim());
            Price = price;
        }
    }
}

class Check : ICheck
{
    public IShop Shop {get; set;}
    public IProduct Product {get; set;}
    public DateTime DataTime {get; set;}

    public Check(IShop shop, IProduct product)
    {
        Shop = shop;
        Product = product;
        DataTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"CHECK\nshop: {Shop} \nproduct: {Product} \ndatatime: {DataTime}";
    }
}

class Shop : IShop
{
    public string Name {get; set;}
    
    public string Address {get; set;}

    public Shop(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public override string ToString()
    {
        return $"name: {Name} \t address: {Address}";
    }

    public void AddProduct(IProduct product)
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")))
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"), true))
                {
                    sw.WriteLine(product.ToString() + "\n");
                }
            }
            else
            {
                File.Create(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"));
                
                using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"), true))
                {
                    sw.WriteLine(product.ToString() + "\n");
                }
            }
        }
        else
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "src"));
            
            File.Create(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"));
            
            using (StreamWriter sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"), true))
            {
                sw.WriteLine(product.ToString() + "\n");
            }
        }
    }

    public void RemoveProduct(IProduct product)
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")))
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
            {
                using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
                using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line != product.ToString())
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }
}

class User : IUser
{
    public string Name { get; }
    public int Ballance { get; set;}

    public void AddBallance(int sum)
    {
        Ballance += sum;
    }

    public void ShowAllProducts()
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")))
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
            {
                using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            else
            {
                Console.WriteLine("Товаров ещё нет!");
            }
        }
        else
        {
            Console.WriteLine("Товаров ещё нет!");
        }
    }

    public void SearchProducts(int ID)
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")))
        {
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
            {
                using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(ID.ToString()))
                        {
                            Console.WriteLine($"товар найден: {line}");
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }

    public void BuyProduct(int ID, IUser user, IShop shop)
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")))
        {
            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
            {
                using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products")))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(ID.ToString()))
                        {
                            Product p = new Product();
                            p.FromString(line);
                            
                            user.Ballance = Ballance - p.Price;
                            if (user.Ballance < 0)
                            {
                                Console.WriteLine("У вас недостаточно средств!");
                                break;
                            }
                            
                            Console.WriteLine($"товар: {line} куплен!");
                            Check check = new Check(shop, p);
                            Console.WriteLine(check);
                            
                            

                            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Buy")))
                            {
                                using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Buy"), true))
                                {
                                    writer.WriteLine(p.ToString());
                                }
                            }
                            else
                            {
                                File.Create(Path.Combine(Directory.GetCurrentDirectory(), "src", "Buy"));
                                
                                using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "src", "Products"), true))
                                {
                                    writer.WriteLine(p.ToString());
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }

    public void ShowBuyProducts()
    {
        if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src")) && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "src", "Buy")))
        {
            using (StreamReader reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "src", "Buy")))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("Вы ничего не покупали");
        }
    }
}