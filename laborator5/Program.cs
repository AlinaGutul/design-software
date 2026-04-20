using System;

Console.WriteLine("Lab 5");
Console.WriteLine();

// 1
bool a1 = true;
Console.WriteLine(a1);

// 3
bool a3 = 1 == 2;
Console.WriteLine(a3);

// 4
int x4 = 3;
int y4 = 4;
bool b4 = x4 == y4;
Console.WriteLine(b4);

// 5
int x5 = 3;
int y5 = 4;
bool b5 = x5 * 2 == y5 + 4;
Console.WriteLine(b5);

// 6
bool a6 = 1 > 2;
a6 = 3 == 3;
Console.WriteLine(a6);

// 7
bool a7 = true;
F(a7);

// 8
F(5 > 3);

// 9
bool result9 = F2();
Console.WriteLine(result9);

// 10
bool result10 = IsGreater(5, 3);
Console.WriteLine(result10);

// 11
bool a11 = true;
bool b11 = false;
bool c11 = a11 == b11;
Console.WriteLine(c11);

// 12
bool a12 = false;
bool b12 = !a12;
Console.WriteLine(b12);

// 13
bool a13 = true;
bool b13 = false;
bool c13 = a13 && b13;
Console.WriteLine(c13);

// 14
bool r14 = A1() && B1();

// 15
bool r15 = A2() && B2();

// 16
bool r16 = A3() && B3();

// 17
bool r17 = A4() || B4();


// ---------- функции ----------

static void F(bool x)
{
    Console.WriteLine(x);
}

static bool F2()
{
    return true;
}

static bool IsGreater(int a, int b)
{
    return a > b;
}

// 14
static bool A1()
{
    Console.WriteLine("A");
    return true;
}

static bool B1()
{
    Console.WriteLine("B");
    return true;
}

// 15
static bool A2()
{
    Console.WriteLine("A");
    return true;
}

static bool B2()
{
    Console.WriteLine("B");
    return false;
}

// 16
static bool A3()
{
    Console.WriteLine("A");
    return false;
}

static bool B3()
{
    Console.WriteLine("B");
    return true;
}

// 17
static bool A4()
{
    Console.WriteLine("A");
    return true;
}

static bool B4()
{
    Console.WriteLine("B");
    return true;
}