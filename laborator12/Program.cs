using System;

Console.WriteLine("Lab 12 - Library");
Console.WriteLine();


// ---------- МАССИВ КНИГ ----------

// создаем список книг (массив)
Book[] books = new Book[]
{
    new Book { Title = "Book1", Year = 2001, IsTaken = false },
    new Book { Title = "Book2", Year = 2005, IsTaken = false },
    new Book { Title = "Book3", Year = 2010, IsTaken = false },
    new Book { Title = "Book4", Year = 2015, IsTaken = false },
    new Book { Title = "Book5", Year = 2020, IsTaken = false },
};


// ---------- МЕНЮ ----------

while (true) // бесконечный цикл → программа работает постоянно
{
    Console.WriteLine();
    Console.WriteLine("1 - Показать книги");
    Console.WriteLine("2 - Взять книгу");
    Console.WriteLine("3 - Вернуть книгу");
    Console.WriteLine("0 - Выход");

    string choice = Console.ReadLine()!; // ввод пользователя

    if (choice == "1")
    {
        PrintBooks(books); // вывод всех книг
    }
    else if (choice == "2")
    {
        Console.Write("Введите название: ");
        string title = Console.ReadLine()!;

        TakeBook(books, title); // попытка взять книгу
    }
    else if (choice == "3")
    {
        Console.Write("Введите название: ");
        string title = Console.ReadLine()!;

        ReturnBook(books, title); // попытка вернуть книгу
    }
    else if (choice == "0")
    {
        break; // выход из программы
    }
}


// ================= ФУНКЦИИ =================


// ---------- Показ всех книг ----------
static void PrintBooks(Book[] books)
{
    // проходим по всему массиву
    for (int i = 0; i < books.Length; i++)
    {
        Book b = books[i]; // текущая книга

        // вывод информации о книге
        Console.WriteLine(
            b.Title + " (" + b.Year + ") - " +
            (b.IsTaken ? "взята" : "доступна") // если true → взята
        );
    }
}


// ---------- Взять книгу ----------
static void TakeBook(Book[] books, string title)
{
    // ищем книгу по названию
    for (int i = 0; i < books.Length; i++)
    {
        if (books[i].Title == title) // нашли книгу
        {
            if (books[i].IsTaken) // уже взята
            {
                Console.WriteLine("Книга уже взята");
                return; // выходим
            }

            books[i].IsTaken = true; // меняем статус
            Console.WriteLine("Вы взяли книгу");
            return;
        }
    }

    // если не нашли книгу
    Console.WriteLine("Книга не найдена");
}


// ---------- Вернуть книгу ----------
static void ReturnBook(Book[] books, string title)
{
    for (int i = 0; i < books.Length; i++)
    {
        if (books[i].Title == title) // нашли
        {
            if (!books[i].IsTaken) // уже свободна
            {
                Console.WriteLine("Книга уже в библиотеке");
                return;
            }

            books[i].IsTaken = false; // возвращаем
            Console.WriteLine("Книга возвращена");
            return;
        }
    }

    // если книги нет
    Console.WriteLine("Ошибка: такой книги нет");
}


// ================= СТРУКТУРА =================

// структура объединяет данные одной книги
struct Book
{
    public string Title;   // название
    public int Year;       // год выпуска
    public bool IsTaken;   // статус (взята или нет)
}