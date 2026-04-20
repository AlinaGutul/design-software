using System;

Console.WriteLine("Lab 13 - Nullability");
Console.WriteLine();


// ---------- ДАННЫЕ ----------

// создаем массив книг
Book[] books = new Book[]
{
    new Book { Title = "Book1", Year = 2001, IsTaken = false },
    new Book { Title = "Book2", Year = 2005, IsTaken = false },
};


// ---------- МЕНЮ ----------

while (true)
{
    Console.WriteLine("1 - Показать");
    Console.WriteLine("2 - Взять");
    Console.WriteLine("0 - Выход");

    // ReadLine МОЖЕТ вернуть null → поэтому string?
    string? choice = Console.ReadLine();

    // проверка на null (иначе ошибка)
    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Пустой ввод");
        continue;
    }

    if (choice == "1")
    {
        PrintBooks(books);
    }
    else if (choice == "2")
    {
        Console.Write("Название: ");

        // снова может быть null
        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Ошибка: название не введено");
            continue;
        }

        TakeBook(books, title);
    }
    else if (choice == "0")
    {
        break;
    }
}


// ================= ФУНКЦИИ =================

// вывод книг
static void PrintBooks(Book[] books)
{
    for (int i = 0; i < books.Length; i++)
    {
        Console.WriteLine(books[i].Title);
    }
}


// взять книгу
static void TakeBook(Book[] books, string title)
{
    for (int i = 0; i < books.Length; i++)
    {
        if (books[i].Title == title)
        {
            books[i].IsTaken = true;
            Console.WriteLine("Книга взята");
            return;
        }
    }

    Console.WriteLine("Книга не найдена");
}


// ================= СТРУКТУРА =================

struct Book
{
    // required → обязательно задать при создании
    public required string Title;

    public int Year;
    public bool IsTaken;
}