using System;

Console.WriteLine("Lab 7");
Console.WriteLine("1 - вопросы");
Console.WriteLine("2 - практика");

string mode = Console.ReadLine()!; // читаем режим (строка)

if (mode == "1")
{
    RunQuestions(); // запускаем примеры
}
else
{
    RunPractice(); // запускаем практику
}


// ================= ВОПРОСЫ =================
void RunQuestions()
{
    Console.WriteLine("---- 1 Parse ----");
    try
    {
        string a = Console.ReadLine()!; // пользователь вводит строку
        int b = int.Parse(a); // пытаемся превратить в число
        // если строка не число → будет ошибка
        Console.WriteLine(b);
    }
    catch
    {
        Console.WriteLine("Ошибка парсинга"); // ловим ошибку
    }

    Console.WriteLine("---- 2 TryParse ----");
    string a2 = Console.ReadLine()!;
    int b2;

    // TryParse:
    // пытается преобразовать строку → если получилось = true
    // если нет → false, но программа НЕ падает
    bool success = int.TryParse(a2, out b2);

    Console.WriteLine(success); // получилось или нет
    Console.WriteLine(b2);      // результат (или 0 если ошибка)

    Console.WriteLine("---- 3 ----");
    int x3 = 0;
    F3(x3); // передаем значение (копию!)
    Console.WriteLine(x3); // не изменится → 0

    Console.WriteLine("---- 4 ----");
    int x4 = 0;
    F4(out x4); // передаем саму переменную (ссылка)
    Console.WriteLine(x4); // изменится → 1

    Console.WriteLine("---- 5 ----");
    int x5 = 0;
    F5(out x5); // имя внутри функции не важно
    Console.WriteLine(x5); // 1

    Console.WriteLine("---- 12 ----");
    int x12 = 0;
    F12(out x12); // out записывает значение
    Console.WriteLine(x12); // 1 (return мы не используем)

    Console.WriteLine("---- 13 ----");
    int a13 = 0;
    int b13 = F13(out a13); 
    // out → a13 = 1
    // return → b13 = 2

    Console.WriteLine(a13);
    Console.WriteLine(b13);

    Console.WriteLine("---- 14 ----");
    int a14;
    int b14;
    F14(out a14, out b14); // сразу 2 значения
    Console.WriteLine(a14); // 1
    Console.WriteLine(b14); // 2
}


// ================= ПРАКТИКА =================
void RunPractice()
{
    Console.WriteLine("Практика: ввод 3 чисел > 2");

    int ReadNumber()
    {
        int number;

        while (true) // бесконечный цикл, пока не введут правильно
        {
            Console.WriteLine("Введите число больше 2:");
            string input = Console.ReadLine()!;

            // проверяем сразу 2 условия:
            // 1. это число?
            // 2. больше ли 2?
            if (int.TryParse(input, out number) && number > 2)
            {
                return number; // если всё ок → возвращаем число
            }

            // если ошибка → просим снова
            Console.WriteLine("Ошибка, попробуйте снова");
        }
    }

    // вводим 3 числа (каждое проверяется)
    int a = ReadNumber();
    int b = ReadNumber();
    int c = ReadNumber();

    int result = a * b * c; // считаем произведение

    Console.WriteLine("Результат: " + result);
}


// ================= ФУНКЦИИ =================

// передача по значению (копия)
static void F3(int a)
{
    a = 1; // меняем только копию, снаружи не изменится
}

// передача через out (ссылка)
static void F4(out int b)
{
    b = 1; // обязательно присвоить значение
}

static void F5(out int a)
{
    a = 1; // это та же переменная, просто имя совпало
}

// out + return
static int F12(out int b)
{
    b = 1;      // записываем в переменную
    return 2;   // отдельное возвращаемое значение
}

static int F13(out int c)
{
    c = 1;      // через out
    return 2;   // через return
}

// несколько out
static void F14(out int c, out int d)
{
    c = 1;
    d = 2;
}