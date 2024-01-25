
using StoreApp;
using StoreApp.Exceptions;

Market market = new Market();
market.AlcoholPercentLimit = 45;
string opt;
do
{
    opt = ChoseOperation();

    switch (opt)
    {
        case "1":
            AddProduct();
            break;
        case "2":
            RemoveProduct();    
            break;
        case "3":
            ShowProducts();
            break;
        case "6":
            Console.WriteLine(market.AvgAlcoholPercent);
            break;
        default:
            break;
    }

} while (opt!="0");




string ChoseOperation()
{
    ShowMenu();
    Console.WriteLine("Emeliyyat secin: ");
    return Console.ReadLine();
}
void ShowMenu()
{
    Console.WriteLine("1. Mehsul elave et");
    Console.WriteLine("2. Mehsul sil");
    Console.WriteLine("3. Mehsullara bax");
    Console.WriteLine("4. Mehsulu yenile");
    Console.WriteLine("5. Mehsulu sat");
    Console.WriteLine("6. Ortalama alkoqol faizi");
    Console.WriteLine("0. Cix");
}

void AddProduct()
{
    Console.WriteLine("Ad: ");
    string name = Console.ReadLine();

    Console.WriteLine("Maya qiymeti: ");
    string costPriceStr = Console.ReadLine();
    double costPrice = Convert.ToDouble(costPriceStr);

    Console.WriteLine("Satis qiymeti: ");
    string salePriceStr = Console.ReadLine();
    double salePrice = Convert.ToDouble(salePriceStr);


    Console.WriteLine("Yararliliq bitme muddeti: ");
    string expireDateStr = Console.ReadLine();
    DateTime expireDate = Convert.ToDateTime(expireDateStr);

checkIsDrink:
    Console.WriteLine("Icki mehsuludurmu? y/n");
    string isDrinkStr = Console.ReadLine();

    Product product;
    string alchoPercentStr = null;
    if (isDrinkStr == "y")
    {
        Console.WriteLine("Alkoqol faizi: ");
        alchoPercentStr = Console.ReadLine();
        double alchoPercent = Convert.ToDouble(alchoPercentStr);
        product = new DrinkProduct(name, salePrice, costPrice, expireDate, alchoPercent);
    }
    else if (isDrinkStr == "n")
    {
        product = new Product(name, salePrice, costPrice, expireDate);
    }
    else
        goto checkIsDrink;

    market.AddProduct(product);
}

void ShowProducts()
{
    Console.WriteLine("a. Butun mehsullar");
    Console.WriteLine("b. Alkoqullu ickiler");
    Console.WriteLine("c  Alkoqolsuz ickiler");
    Console.WriteLine("Secim edin:");
    string showOpt = Console.ReadLine();

    switch (showOpt)
    {
        case "a":
            for (int i = 0; i < market.Products.Length; i++)
                Console.WriteLine(market.Products[i]);
            break;
        case "b":
            var alcoholProducts = market.GetAllAlcoholDrinks();
            for (int i = 0; i < alcoholProducts.Length; i++)
                Console.WriteLine(alcoholProducts[i]);
            break;
        case "c":
            var nonAlcoholProducts = market.GetAllNonAlcoholDrinks();
            for (int i = 0; i < nonAlcoholProducts.Length; i++)
                Console.WriteLine(nonAlcoholProducts[i]);
            break;
        default:
            Console.WriteLine("Seciminiz yanlisdir");
            break;
    }
}


void RemoveProduct()
{
    Console.WriteLine("======== silme emeliyyati ==========");
    for (int i = 0; i < market.Products.Length; i++)
        Console.WriteLine(market.Products[i]);
    Console.WriteLine("Melsul no:");
    string noStr = Console.ReadLine();
    int no = Convert.ToInt32(noStr);

    try
    {
        market.RemoveProductByNo(no);
    }
    catch (ProductNotFoundException)
    {
        Console.WriteLine($"{no} nomreli mehsul yoxdur");
    }
    catch (ProductExpireDateException)
    {
        Console.WriteLine($"Mehsulun yararliqli muddetinin bitmesine 1 ilden cox var");
    }
    catch
    {
        Console.WriteLine("Bilinmedik bir xeta bas verdi");
    }

}

