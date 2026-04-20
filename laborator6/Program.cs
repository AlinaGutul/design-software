using System;

Console.WriteLine("Lab 6");
Console.WriteLine();

// 1
Console.WriteLine("---- 1 ----");
if (true) // условие true → блок выполняется
{
    Console.WriteLine("Hello"); // выводится
}

// 2
Console.WriteLine("---- 2 ----");
if (false) // условие false → блок НЕ выполняется
{
    Console.WriteLine("Hello");
}

// 3
Console.WriteLine("---- 3 ----");
bool execute = true;
if (execute) // true → выполняется
{
    Console.WriteLine("Hello");
}

bool notExecute = !execute; // !true = false
if (notExecute) // false → не выполняется
{
    Console.WriteLine("Not executed");
}

// 4
Console.WriteLine("---- 4 (F1) ----");
F1(); // A → B → Hello (вложенные if)

Console.WriteLine("---- 4 (F2) ----");
F2(); // A → B → Hello (B вызывается только если A = true)

Console.WriteLine("---- 4 (F3) ----");
F3(); // A → B → Hello (оба вызываются заранее)

// 6
Console.WriteLine("---- 6 ----");
if (false) // весь блок пропускается
{
    Console.WriteLine("A");
    Console.WriteLine("B");
}

// 7
Console.WriteLine("---- 7 ----");
if (false)
    Console.WriteLine("A"); // только эта строка относится к if
Console.WriteLine("B"); // выполняется всегда

// 8
Console.WriteLine("---- 8 ----");
if (false)
{
    Console.WriteLine("A");
}
else // выполняется, потому что if = false
{
    Console.WriteLine("B");
}

// 9
Console.WriteLine("---- 9 ----");
bool a = true;
if (a) // заходим
{
    a = false; // меняем значение
}
else
{
    Console.WriteLine("B"); // не выполнится
}

// 10
Console.WriteLine("---- 10 ----");
F(); // внутри return → функция сразу завершится

// 11
Console.WriteLine("---- 11 ----");
if (true)
    Console.WriteLine("A"); // выполняется
else
    Console.WriteLine("B");
Console.WriteLine("C"); // выполняется всегда

// 12
Console.WriteLine("---- 12 ----");
if (true)
    Console.WriteLine("A"); // выполняется
else
{
    Console.WriteLine("B");
}

// 15
Console.WriteLine("---- 15 ----");
int i = 0;
while (true)
{
    if (i == 4)
    {
        Console.WriteLine("ERROR: Should not happen");
        break; // выход из цикла
    }
    if (i == 3)
    {
        Console.WriteLine("Exit");
        break; // выход
    }
    if (i == 0)
    {
        Console.WriteLine("Increase by 2 on first iter");
        i += 2; // i = 2
        continue; // пропуск остального кода итерации
    }

    Console.WriteLine("Increase by 1 normally");
    i++; // i = 3
}

// 16
Console.WriteLine("---- 16 ----");
Console.WriteLine(F4()); // вернет 0


// -------- функции --------

static void F1()
{
    if (A()) // вызывается A
    {
        if (B()) // потом B
        {
            Console.WriteLine("Hello");
        }
    }
}

static void F2()
{
    if (A() && B()) // B вызывается только если A вернул true
    {
        Console.WriteLine("Hello");
    }
}

static void F3()
{
    bool a = A(); // A вызывается всегда
    bool b = B(); // B вызывается всегда
    bool ok = a && b;
    if (ok)
    {
        Console.WriteLine("Hello");
    }
}

static bool A()
{
    Console.WriteLine("A");
    return true;
}

static bool B()
{
    Console.WriteLine("B");
    return true;
}

static void F()
{
    if (true)
    {
        return; // сразу выход из функции
    }
    else
    {
        Console.WriteLine("Else");
    }
    Console.WriteLine("After Else"); // никогда не выполнится
}

static int F4()
{
    while (true)
    {
        if (true)
        {
            return 0; // сразу возвращает 0
        }
        break;
    }
    return 1;
}