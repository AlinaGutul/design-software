using System;

Console.WriteLine("Field Mask Demo");
Console.WriteLine();


// ============================================================
// СОЗДАЕМ ДВА ОБЪЕКТА
// ============================================================

var p1 = new Person
{
    FirstName = "Anna",
    LastName = "Ivanova",
    Age = 20
};

var p2 = new Person
{
    FirstName = "Ana",
    LastName = "Petrova",
    Age = 20
};


// ============================================================
// НАХОДИМ РАЗЛИЧИЯ
// ============================================================

var diff = Functions.Diff(p1, p2);

// проверяем конкретные поля
if (diff.LastName)
{
    Console.WriteLine("Фамилии отличаются");
}


// ============================================================
// ПРОВЕРКА ЧЕРЕЗ МАСКУ
// ============================================================

var checkMask = new PersonFieldMask
{
    FirstName = true,
    LastName = true
};

// если хотя бы одно из этих полей отличается
if (Functions.AreSet(diff, checkMask))
{
    Console.WriteLine("Есть различия в имени или фамилии");
}


// ============================================================
// ПЕЧАТЬ С МАСКОЙ
// ============================================================

Console.WriteLine();
Console.WriteLine("Печать по маске:");

Functions.Print(p1, new PersonFieldMask
{
    FirstName = true,
    Age = true
});


// ============================================================
// DOMAIN MODEL
// ============================================================

class Person
{
    public required string FirstName;
    public required string LastName;
    public int Age;
}


// ============================================================
// ENUM ПОЛЕЙ
// ============================================================

enum PersonField
{
    FirstName = 0,
    LastName = 1,
    Age = 2
}


// ============================================================
// МАСКА ПОЛЕЙ (НА БИТАХ)
// ============================================================

struct PersonFieldMask
{
    private int _value;

    // получить значение бита
    public bool Get(PersonField field)
    {
        int bit = 1 << (int)field;
        return (_value & bit) != 0;
    }

    // установить значение бита
    public void Set(PersonField field, bool value)
    {
        int bit = 1 << (int)field;

        if (value)
            _value |= bit;   // включаем бит
        else
            _value &= ~bit;  // выключаем бит
    }

    // свойства для удобства
    public bool FirstName
    {
        get => Get(PersonField.FirstName);
        set => Set(PersonField.FirstName, value);
    }

    public bool LastName
    {
        get => Get(PersonField.LastName);
        set => Set(PersonField.LastName, value);
    }

    public bool Age
    {
        get => Get(PersonField.Age);
        set => Set(PersonField.Age, value);
    }
}


// ============================================================
// ФУНКЦИИ
// ============================================================

static class Functions
{
    // сравнение объектов → возвращает маску различий
    public static PersonFieldMask Diff(Person a, Person b)
    {
        PersonFieldMask mask = new();

        if (a.FirstName != b.FirstName)
            mask.FirstName = true;

        if (a.LastName != b.LastName)
            mask.LastName = true;

        if (a.Age != b.Age)
            mask.Age = true;

        return mask;
    }


    // проверка: все ли поля mask входят в diff
    public static bool AreSet(PersonFieldMask mask, PersonFieldMask toCheck)
    {
        return (maskValue(mask) & maskValue(toCheck)) == maskValue(toCheck);
    }

    // вспомогательная функция (доступ к внутреннему значению)
    static int maskValue(PersonFieldMask m)
    {
        return typeof(PersonFieldMask)
            .GetField("_value", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .GetValue(m) is int v ? v : 0;
    }


    // печать по маске
    public static void Print(Person p, PersonFieldMask mask)
    {
        if (mask.FirstName)
            Console.WriteLine("FirstName: " + p.FirstName);

        if (mask.LastName)
            Console.WriteLine("LastName: " + p.LastName);

        if (mask.Age)
            Console.WriteLine("Age: " + p.Age);
    }
}