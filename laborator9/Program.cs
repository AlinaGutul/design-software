using System;

Console.WriteLine("Lab 9");
Console.WriteLine("1 - задача");
Console.WriteLine("2 - анализ");

string mode = Console.ReadLine()!; // читаем выбор пользователя

if (mode == "1")
{
    RunMainTask(); // считаем заказы клиентов
}
else
{
    Analysis.Run(); // запускаем анализ
}


// ================= ОСНОВНАЯ ЗАДАЧА =================
void RunMainTask()
{
    // создаем объект с ценами (все в одном месте)
    Prices prices = new Prices
    {
        Drink = 10,
        First = 20,
        Second = 30
    };

    // ---------- клиент 1 ----------
    {
        // блок → переменные живут только внутри
        Order order = new Order
        {
            DrinkCount = 1,
            FirstCount = 2,
            SecondCount = 1
        };

        // считаем стоимость через функцию
        int total = CustomerTotal(prices, order);

        Console.WriteLine("Клиент 1: " + total);
    }

    // ---------- клиент 2 ----------
    {
        // те же имена переменных → но другой блок
        Order order = new Order
        {
            DrinkCount = 3,
            FirstCount = 0,
            SecondCount = 2
        };

        int total = CustomerTotal(prices, order);

        Console.WriteLine("Клиент 2: " + total);
    }
}


// ---------- ФУНКЦИЯ ----------
static int CustomerTotal(Prices prices, Order order)
{
    // считаем: цена * количество
    int result =
        prices.Drink * order.DrinkCount +
        prices.First * order.FirstCount +
        prices.Second * order.SecondCount;

    return result; // возвращаем итог
}


// ---------- СТРУКТУРЫ ----------

// структура для хранения цен (группировка данных)
struct Prices
{
    public int Drink;
    public int First;
    public int Second;
}

// структура для хранения заказа клиента
struct Order
{
    public int DrinkCount;
    public int FirstCount;
    public int SecondCount;
}