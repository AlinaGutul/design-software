using System;

Console.WriteLine("Lab 11");
Console.WriteLine();


// ================= ОСНОВНАЯ ЗАДАЧА =================

// два входных массива (списки чисел)
int[] a = { 1, 2, 3 };
int[] b = { 4, 5, 6 };

// создаем массивы для результата (той же длины)
int[] resultFor = new int[a.Length];
int[] resultWhile = new int[a.Length];

// вызываем алгоритмы → они ЗАПОЛНЯЮТ массивы
SumArraysFor(a, b, resultFor);
SumArraysWhile(a, b, resultWhile);

// печатает только main
Console.WriteLine("Результат (for):");
Print(resultFor);

Console.WriteLine("Результат (while):");
Print(resultWhile);


// ================= ДОПОЛНИТЕЛЬНАЯ ПРАКТИКА =================

Console.WriteLine();
Console.WriteLine("=== ДОПОЛНИТЕЛЬНЫЕ АЛГОРИТМЫ ===");

int[] test = { 1, 6, 3, 10, 7 };

// считаем сколько > 5
int count = CountGreaterThanFive(test);
Console.WriteLine("Чисел > 5: " + count);

// ищем максимум
int max = FindMax(test);
Console.WriteLine("Максимум: " + max);

// ищем 2 максимума
(int max1, int max2) = FindTwoMax(test);
Console.WriteLine("Два максимума: " + max1 + ", " + max2);

// генерируем простые числа
int[] primes = GeneratePrimes(5);
Console.WriteLine("Простые числа:");
Print(primes);

// генерируем Фибоначчи
int[] fib = GenerateFibonacci(6);
Console.WriteLine("Фибоначчи:");
Print(fib);


// ================= АЛГОРИТМЫ =================

// версия с for
static void SumArraysFor(int[] a, int[] b, int[] result)
{
    // проверка (контракт)
    // если длины разные → ошибка
    if (a.Length != b.Length || a.Length != result.Length)
        throw new ArgumentException("Массивы должны быть одинаковой длины");

    // идем по всем индексам
    for (int i = 0; i < a.Length; i++)
    {
        // берем элементы с одинаковым индексом
        int x = a[i];
        int y = b[i];

        // складываем
        int sum = x + y;

        // записываем результат
        result[i] = sum;
    }
}


// версия с while
static void SumArraysWhile(int[] a, int[] b, int[] result)
{
    if (a.Length != b.Length || a.Length != result.Length)
        throw new ArgumentException("Массивы должны быть одинаковой длины");

    int i = 0; // начинаем с первого элемента

    while (i < a.Length) // пока не дошли до конца
    {
        result[i] = a[i] + b[i];
        i++; // обязательно увеличиваем индекс
    }
}


// ================= ВСПОМОГАТЕЛЬНОЕ =================

// печать массива (алгоритмы сами ничего не печатают)
static void Print(int[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        Console.WriteLine(arr[i]);
    }
}


// ================= ДОП. АЛГОРИТМЫ =================

// 1. Подсчет чисел > 5
static int CountGreaterThanFive(int[] arr)
{
    int count = 0; // считаем сколько нашли

    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] > 5) // проверяем условие
        {
            count++; // увеличиваем счетчик
        }
    }

    return count;
}


// 2. Поиск максимума
static int FindMax(int[] arr)
{
    int max = arr[0]; // сначала считаем первый элемент максимумом

    for (int i = 1; i < arr.Length; i++)
    {
        if (arr[i] > max)
        {
            max = arr[i]; // нашли больше → обновили
        }
    }

    return max;
}


// 3. Два максимума
static (int, int) FindTwoMax(int[] arr)
{
    int max1 = int.MinValue; // самый большой
    int max2 = int.MinValue; // второй

    for (int i = 0; i < arr.Length; i++)
    {
        int x = arr[i];

        if (x > max1)
        {
            max2 = max1; // старый максимум становится вторым
            max1 = x;
        }
        else if (x > max2)
        {
            max2 = x;
        }
    }

    return (max1, max2);
}


// 4. Простые числа
static int[] GeneratePrimes(int n)
{
    int[] result = new int[n];
    int count = 0;   // сколько уже нашли
    int number = 2;  // начинаем с 2

    while (count < n)
    {
        if (IsPrime(number)) // проверяем число
        {
            result[count] = number;
            count++;
        }

        number++; // проверяем следующее
    }

    return result;
}


// проверка простого числа
static bool IsPrime(int x)
{
    if (x < 2) return false;

    for (int i = 2; i < x; i++)
    {
        if (x % i == 0) // делится без остатка
            return false;
    }

    return true;
}


// 5. Фибоначчи
static int[] GenerateFibonacci(int n)
{
    int[] result = new int[n];

    result[0] = 0;

    if (n > 1)
        result[1] = 1;

    for (int i = 2; i < n; i++)
    {
        // каждое число = сумма двух предыдущих
        result[i] = result[i - 1] + result[i - 2];
    }

    return result;
}