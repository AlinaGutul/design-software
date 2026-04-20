using System;

public static class Analysis
{
    public static void Run()
    {
        Console.WriteLine();
        Console.WriteLine("=== АНАЛИЗ ===");

        // ================= 1 =================
        Console.WriteLine("---- 1 int ----");

        int b = 6;
        F1(b); // передаем копию значения
        Console.WriteLine(b); // 6

        // int → тип-значение → копия
        // внутри функции создается отдельная переменная a
        // b не меняется


        // ================= 2 =================
        Console.WriteLine("---- 2 одинаковые имена ----");

        int a = 6;
        F2(a);
        Console.WriteLine(a); // 6

        // одинаковые имена ≠ одна переменная
        // внутри функции своя переменная a


        // ================= 3 =================
        Console.WriteLine("---- 3 string ----");

        string s = "hello";
        F3(s);
        Console.WriteLine(s); // hello

        // string — ссылочный тип
        // но передается копия ссылки
        // b = "world" меняет только локальную переменную


        // ================= 4 struct =================
        Console.WriteLine("---- 4 struct ----");

        A_struct s4 = new A_struct { value = 1 };
        F4_struct(s4);
        Console.WriteLine(s4.value); // 1

        // struct — тип-значение → копия
        // изменения не сохраняются


        // ================= 4 class =================
        Console.WriteLine("---- 4 class ----");

        A_class c4 = new A_class { value = 1 };
        F4_class(c4);
        Console.WriteLine(c4.value); // 2

        // class — ссылочный тип
        // передается ссылка → изменяется объект
    }


    // ---------- функции ----------

    static void F1(int a)
    {
        a = 5; // меняем только копию
    }

    static void F2(int a)
    {
        a = 5;
    }

    static void F3(string b)
    {
        b = "world"; // новая строка, локально
    }

    static void F4_struct(A_struct a)
    {
        a.value = 2; // меняем копию struct
    }

    static void F4_class(A_class a)
    {
        a.value = 2; // меняем объект
    }


    // ---------- типы ----------

    struct A_struct
    {
        public int value;
    }

    class A_class
    {
        public int value;
    }
}