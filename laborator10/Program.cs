using System;

Console.WriteLine("Lab - Algorithms");
Console.WriteLine();


// ---------- СПИСКИ (массивы) ----------

// массив — это список элементов в памяти
int[] a = { 1, 2, 3 };
int[] b = { 4, 5, 6 };

// результат такого же размера
int[] result = new int[a.Length];


// ---------- АЛГОРИТМ ----------

// переменная i — индекс (следит за текущим элементом)
for (int i = 0; i < a.Length; i++)
{
    // берем элементы с одинаковым индексом
    int sum = a[i] + b[i];

    result[i] = sum; // сохраняем результат
}


// ---------- ВЫВОД ----------

Console.WriteLine("Результат:");

for (int i = 0; i < result.Length; i++)
{
    Console.WriteLine(result[i]);
}