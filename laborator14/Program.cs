using System;

Console.WriteLine("Lab 14 - Static classes");
Console.WriteLine();


// ---------- СОЗДАЕМ МАССИВ КНИГ ----------

// массив = список объектов Book
Book[] books = new Book[]
{
    new Book { Title = "Book1", Year = 2001, IsTaken = false },
    new Book { Title = "Book2", Year = 2005, IsTaken = false },
    new Book { Title = "Book3", Year = 2010, IsTaken = false },
};


// ---------- МЕНЮ ----------

while (true) // бесконечный цикл → программа работает постоянно
{
    Console.WriteLine("1 - Показать");
    Console.WriteLine("2 - Взять");
    Console.WriteLine("3 - Вернуть");
    Console.WriteLine("0 - Выход");

    // ReadLine может вернуть null → используем string?
    string? choice = Console.ReadLine();

    // проверяем и null, и пустую строку
    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Пустой ввод");
        continue;
    }

    if (choice == "1")
    {
        // вызываем функцию из другого файла (static class)
        Functions.PrintBooks(books);
    }
    else if (choice == "2")
    {
        Console.Write("Название: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Ошибка: название не введено");
            continue;
        }

        // берем книгу
        Functions.TakeBook(books, title);
    }
    else if (choice == "3")
    {
        Console.Write("Название: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Ошибка: название не введено");
            continue;
        }

        // возвращаем книгу
        Functions.ReturnBook(books, title);
    }
    else if (choice == "0")
    {
        break; // выход из программы
    }
}


// ---------- СТРУКТУРА КНИГИ ----------

// struct объединяет данные одной книги
public struct Book
{
    public required string Title; // обязательно задать
    public int Year;              // год выпуска
    public bool IsTaken;          // статус: взята или нет
}