using System;

Console.WriteLine("Lab 15 - Instance methods");
Console.WriteLine();


// ---------- СОЗДАЕМ БИБЛИОТЕКУ ----------

// создаем ОБЪЕКТ (instance) класса Library
// это "реальная" библиотека, с которой мы будем работать
Library library = new Library
{
    // обязательно задаем массив книг (required поле)
    Books = new Book[]
    {
        new Book { Title = "Book1", Year = 2001, IsTaken = false },
        new Book { Title = "Book2", Year = 2005, IsTaken = false },
        new Book { Title = "Book3", Year = 2010, IsTaken = false },
    }
};


// ---------- МЕНЮ ----------

while (true) // бесконечный цикл → программа работает постоянно
{
    Console.WriteLine("1 - Показать");
    Console.WriteLine("2 - Взять");
    Console.WriteLine("3 - Вернуть");
    Console.WriteLine("0 - Выход");

    // пользователь вводит строку (может быть null)
    string? choice = Console.ReadLine();

    // защита от null и пустой строки
    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Пустой ввод");
        continue; // возвращаемся в начало цикла
    }

    if (choice == "1")
    {
        // вызываем МЕТОД объекта library
        // внутри он сам знает, какие книги у него есть
        library.PrintBooks();
    }
    else if (choice == "2")
    {
        Console.Write("Название: ");

        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Ошибка");
            continue;
        }

        // ВАЖНО:
        // раньше было Functions.TakeBook(books, title)
        // теперь вызываем метод у объекта
        library.TakeBook(title);
    }
    else if (choice == "3")
    {
        Console.Write("Название: ");

        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Ошибка");
            continue;
        }

        // метод возвращения книги
        library.ReturnBook(title);
    }
    else if (choice == "0")
    {
        break; // выход из программы
    }
}


// ================= КЛАССЫ =================


// ---------- КЛАСС БИБЛИОТЕКИ ----------

// это "абстракция" всей системы
// в нем хранятся данные и логика
class Library
{
    // required → обязательно задать при создании объекта
    public required Book[] Books;


    // ---------- МЕТОД: Показ книг ----------
    public void PrintBooks()
    {
        // this.Books — это массив книг внутри объекта
        for (int i = 0; i < this.Books.Length; i++)
        {
            Book b = this.Books[i];

            Console.WriteLine(
                b.Title + " (" + b.Year + ") - " +
                (b.IsTaken ? "взята" : "доступна")
            );
        }
    }


    // ---------- МЕТОД: Взять книгу ----------
    public void TakeBook(string title)
    {
        // ищем книгу по названию
        for (int i = 0; i < this.Books.Length; i++)
        {
            if (this.Books[i].Title == title)
            {
                // если уже взята — нельзя взять
                if (this.Books[i].IsTaken)
                {
                    Console.WriteLine("Книга уже взята");
                    return;
                }

                // меняем состояние книги
                this.Books[i].IsTaken = true;

                Console.WriteLine("Книга взята");
                return;
            }
        }

        // если не нашли
        Console.WriteLine("Книга не найдена");
    }


    // ---------- МЕТОД: Вернуть книгу ----------
    public void ReturnBook(string title)
    {
        for (int i = 0; i < this.Books.Length; i++)
        {
            if (this.Books[i].Title == title)
            {
                // если книга уже свободна
                if (!this.Books[i].IsTaken)
                {
                    Console.WriteLine("Книга уже в библиотеке");
                    return;
                }

                // меняем статус обратно
                this.Books[i].IsTaken = false;

                Console.WriteLine("Книга возвращена");
                return;
            }
        }

        Console.WriteLine("Ошибка: такой книги нет");
    }
}


// ---------- СТРУКТУРА КНИГИ ----------

// struct — это одна книга
struct Book
{
    public required string Title; // название (обязательно)
    public int Year;              // год выпуска
    public bool IsTaken;          // статус
}