using System;
using System.Collections.Generic;
using System.Diagnostics;

Console.WriteLine("Lab 16 - Encapsulation");
Console.WriteLine();


// ============================================================
// СОЗДАНИЕ БИБЛИОТЕКИ
// ============================================================

// Создаем объект библиотеки.
// В конструктор передаем список книг.
// Снаружи мы не получаем прямой доступ к внутренним полям библиотеки.
Library library = new Library(new List<Book>
{
    // Создаем книгу: название, автор, год.
    new Book("Book1", "Author1", 2001),
    new Book("Book2", "Author2", 2005),
    new Book("Another Book", "Author1", 2010),
});


// ============================================================
// ОСНОВНОЕ МЕНЮ
// ============================================================

// Бесконечный цикл.
// Будет работать, пока пользователь не выберет 0.
while (true)
{
    Console.WriteLine();
    Console.WriteLine("1 - Показать все");
    Console.WriteLine("2 - Взять книгу");
    Console.WriteLine("3 - Вернуть книгу");
    Console.WriteLine("4 - Фильтр");
    Console.WriteLine("0 - Выход");

    // ReadLine может вернуть null, поэтому string?
    string? choice = Console.ReadLine();

    // Если строка пустая или null, команда невалидна.
    if (string.IsNullOrEmpty(choice))
    {
        Console.WriteLine("Пустой ввод");
        continue; // переходим к следующей итерации меню
    }

    // Показать все книги
    if (choice == "1")
    {
        // Вызываем метод библиотеки.
        // Внешний код не знает, как именно книги хранятся внутри.
        library.PrintAll();
    }
    // Взять книгу
    else if (choice == "2")
    {
        Console.Write("Название: ");

        // Читаем название книги
        string? title = Console.ReadLine();

        // Если пользователь ничего не ввел, пропускаем команду
        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Название не введено");
            continue;
        }

        // Просим библиотеку выдать книгу
        library.TakeBook(title);
    }
    // Вернуть книгу
    else if (choice == "3")
    {
        Console.Write("Название: ");

        string? title = Console.ReadLine();

        if (string.IsNullOrEmpty(title))
        {
            Console.WriteLine("Название не введено");
            continue;
        }

        // Просим библиотеку вернуть книгу
        library.ReturnBook(title);
    }
    // Фильтрация
    else if (choice == "4")
    {
        // Сначала читаем фильтр с консоли
        LibraryFilter filter = ReadLibraryFilter();

        // Потом печатаем только подходящие книги
        library.PrintFiltered(filter);
    }
    // Выход
    else if (choice == "0")
    {
        break; // завершаем цикл и программу
    }
    else
    {
        // Если введена неизвестная команда
        Console.WriteLine("Неизвестная команда");
    }
}


// ============================================================
// ФУНКЦИЯ ЧТЕНИЯ ФИЛЬТРА
// ============================================================

// Эта функция пошагово собирает фильтр с консоли.
// На выходе возвращает уже готовый объект LibraryFilter.
LibraryFilter ReadLibraryFilter()
{
    // Создаем сам фильтр
    var filter = new LibraryFilter();

    // Создаем вспомогательный объект ввода.
    // Он помогает безопасно преобразовывать строки пользователя
    // в значения фильтра.
    var input = new InputLibraryFilter(filter);

    // -----------------------------
    // Ввод статуса книги
    // -----------------------------
    while (true)
    {
        Console.Write("Status (Free, Taken, Any): ");
        string? str = Console.ReadLine();

        // Метод Status пытается распарсить строку в enum.
        // Если не получилось, возвращает false.
        bool ok = input.Status(str);

        if (!ok)
        {
            Console.WriteLine("Ошибка");
            continue; // просим ввести снова
        }

        break; // статус введен корректно
    }

    // -----------------------------
    // Ввод включаемой строки
    // -----------------------------
    Console.Write("Включает текст: ");

    // Если строка пустая, InputLibraryFilter сам превратит ее в null.
    input.NameInclude(Console.ReadLine());

    // -----------------------------
    // Ввод исключаемой строки
    // -----------------------------
    Console.Write("Исключает текст: ");
    input.NameExclude(Console.ReadLine());

    // -----------------------------
    // Ввод автора
    // -----------------------------
    Console.Write("Автор: ");
    input.Author(Console.ReadLine());

    // -----------------------------
    // Ввод начала названия
    // -----------------------------
    Console.Write("Начинается с: ");
    input.StartsWith(Console.ReadLine());

    // Возвращаем собранный фильтр
    return filter;
}


// ============================================================
// КЛАСС БИБЛИОТЕКИ
// ============================================================

class Library
{
    // Полный список всех книг.
    // private = снаружи прямого доступа нет.
    private readonly List<Book> _books;

    // Отдельный список взятых книг.
    // Снаружи тоже недоступен.
    private readonly List<Book> _taken = new();

    // Отдельный список свободных книг.
    private readonly List<Book> _free = new();

    // --------------------------------------------------------
    // КОНСТРУКТОР
    // --------------------------------------------------------
    // Конструктор вызывается при создании объекта.
    // Он получает исходный список книг и раскладывает книги по внутренним спискам.
    public Library(List<Book> books)
    {
        // Сохраняем полный список
        _books = books;

        // Изначально считаем, что все книги свободны.
        // Поэтому каждую книгу кладем в список _free.
        foreach (Book b in books)
        {
            _free.Add(b);
        }
    }

    // --------------------------------------------------------
    // ПЕЧАТЬ ВСЕХ КНИГ
    // --------------------------------------------------------
    public void PrintAll()
    {
        // Перебираем полный список книг
        foreach (Book b in _books)
        {
            // Определяем статус книги.
            // Если книга есть в списке _taken, значит она взята.
            // Иначе считаем ее доступной.
            string status = _taken.Contains(b) ? "взята" : "доступна";

            // Печатаем все данные книги вместе со статусом
            Console.WriteLine($"{b.Title} ({b.Author}, {b.Year}) - {status}");
        }
    }

    // --------------------------------------------------------
    // ВЗЯТЬ КНИГУ
    // --------------------------------------------------------
    public void TakeBook(string title)
    {
        // Ищем книгу только среди свободных.
        // Если книга уже взята, в _free ее нет.
        for (int i = 0; i < _free.Count; i++)
        {
            // Проверяем название текущей книги
            if (_free[i].Title == title)
            {
                // Сохраняем найденную книгу во временную переменную
                Book book = _free[i];

                // Удаляем книгу из списка свободных
                _free.RemoveAt(i);

                // Добавляем книгу в список взятых
                _taken.Add(book);

                Console.WriteLine("Книга взята");
                return; // завершаем метод
            }
        }

        // Если цикл закончился и книга не нашлась,
        // значит она либо отсутствует, либо уже взята.
        Console.WriteLine("Не найдена или уже взята");
    }

    // --------------------------------------------------------
    // ВЕРНУТЬ КНИГУ
    // --------------------------------------------------------
    public void ReturnBook(string title)
    {
        // Ищем книгу только среди взятых.
        for (int i = 0; i < _taken.Count; i++)
        {
            // Сравниваем название
            if (_taken[i].Title == title)
            {
                // Сохраняем книгу во временную переменную
                Book book = _taken[i];

                // Удаляем из списка взятых
                _taken.RemoveAt(i);

                // Добавляем обратно в список свободных
                _free.Add(book);

                Console.WriteLine("Книга возвращена");
                return;
            }
        }

        // Если не нашли во взятых, вернуть нельзя.
        Console.WriteLine("Ошибка: такой книги нет");
    }

    // --------------------------------------------------------
    // ПЕЧАТЬ ПО ФИЛЬТРУ
    // --------------------------------------------------------
    public void PrintFiltered(LibraryFilter f)
    {
        // Перебираем все книги
        foreach (Book b in _books)
        {
            // -----------------------------
            // Проверка фильтра по статусу
            // -----------------------------

            // Если фильтр требует только свободные,
            // а книга сейчас взята, пропускаем ее.
            if (f.Status == TakenStatusFilter.Free && _taken.Contains(b))
            {
                continue;
            }

            // Если фильтр требует только взятые,
            // а книга сейчас свободна, тоже пропускаем.
            if (f.Status == TakenStatusFilter.Taken && _free.Contains(b))
            {
                continue;
            }

            // -----------------------------
            // Проверка "должна содержать строку"
            // -----------------------------
            if (f.NameInclude != null && !b.Title.Contains(f.NameInclude))
            {
                continue;
            }

            // -----------------------------
            // Проверка "не должна содержать строку"
            // -----------------------------
            if (f.NameExclude != null && b.Title.Contains(f.NameExclude))
            {
                continue;
            }

            // -----------------------------
            // Проверка автора
            // -----------------------------
            if (f.Author != null && b.Author != f.Author)
            {
                continue;
            }

            // -----------------------------
            // Проверка начала строки
            // -----------------------------
            if (f.StartsWith != null && !b.Title.StartsWith(f.StartsWith))
            {
                continue;
            }

            // Если книга прошла все проверки,
            // определяем текущий статус для вывода.
            string status = _taken.Contains(b) ? "взята" : "доступна";

            // Печатаем книгу
            Console.WriteLine($"{b.Title} ({b.Author}, {b.Year}) - {status}");
        }
    }
}


// ============================================================
// КЛАСС КНИГИ
// ============================================================

class Book
{
    // Название книги
    public string Title { get; }

    // Автор книги
    public string Author { get; }

    // Год выпуска
    public int Year { get; }

    // Конструктор книги.
    // Требует все значения сразу, чтобы объект не был "полупустым".
    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
}


// ============================================================
// ENUM ДЛЯ ФИЛЬТРА СТАТУСА
// ============================================================

enum TakenStatusFilter
{
    // Только свободные книги
    Free,

    // Только взятые книги
    Taken,

    // Любой статус
    Any
}


// ============================================================
// КЛАСС ФИЛЬТРА
// ============================================================

sealed class LibraryFilter
{
    // Скрытые поля для строковых фильтров
    private string? _nameInclude;
    private string? _nameExclude;
    private string? _author;
    private string? _startsWith;

    // Фильтр по статусу.
    // По умолчанию Any = не фильтровать по статусу.
    public TakenStatusFilter Status { get; set; } = TakenStatusFilter.Any;

    // -----------------------------
    // Фильтр "название должно содержать"
    // -----------------------------
    public string? NameInclude
    {
        get
        {
            return _nameInclude;
        }
        set
        {
            // Контракт: пустая строка запрещена.
            // Либо null, либо нормальная строка.
            Debug.Assert(value != "");
            _nameInclude = value;
        }
    }

    // -----------------------------
    // Фильтр "название не должно содержать"
    // -----------------------------
    public string? NameExclude
    {
        get
        {
            return _nameExclude;
        }
        set
        {
            Debug.Assert(value != "");
            _nameExclude = value;
        }
    }

    // -----------------------------
    // Фильтр по автору
    // -----------------------------
    public string? Author
    {
        get
        {
            return _author;
        }
        set
        {
            Debug.Assert(value != "");
            _author = value;
        }
    }

    // -----------------------------
    // Фильтр "название начинается с"
    // -----------------------------
    public string? StartsWith
    {
        get
        {
            return _startsWith;
        }
        set
        {
            Debug.Assert(value != "");
            _startsWith = value;
        }
    }
}


// ============================================================
// КЛАСС ВВОДА ФИЛЬТРА
// ============================================================

sealed class InputLibraryFilter
{
    // Внутри хранится реальный LibraryFilter,
    // в который этот класс будет записывать значения.
    private readonly LibraryFilter _impl;

    public InputLibraryFilter(LibraryFilter impl)
    {
        _impl = impl;
    }

    // --------------------------------------------------------
    // Установка статуса из строки
    // --------------------------------------------------------
    public bool Status(string? str)
    {
        // null не подходит
        if (str == null)
        {
            return false;
        }

        // Пробуем распарсить строку в enum.
        // ignoreCase: true → можно писать free, FREE, Free.
        bool ok = Enum.TryParse(
            value: str,
            ignoreCase: true,
            result: out TakenStatusFilter status);

        // Если не получилось, возвращаем false
        if (!ok)
        {
            return false;
        }

        // Если получилось, записываем в фильтр
        _impl.Status = status;
        return true;
    }

    // --------------------------------------------------------
    // Установка NameInclude
    // --------------------------------------------------------
    public void NameInclude(string? str)
    {
        // По контракту пустая строка не нужна.
        // Поэтому переводим "" в null.
        if (str == "")
        {
            str = null;
        }

        _impl.NameInclude = str;
    }

    // --------------------------------------------------------
    // Установка NameExclude
    // --------------------------------------------------------
    public void NameExclude(string? str)
    {
        if (str == "")
        {
            str = null;
        }

        _impl.NameExclude = str;
    }

    // --------------------------------------------------------
    // Установка Author
    // --------------------------------------------------------
    public void Author(string? str)
    {
        if (str == "")
        {
            str = null;
        }

        _impl.Author = str;
    }

    // --------------------------------------------------------
    // Установка StartsWith
    // --------------------------------------------------------
    public void StartsWith(string? str)
    {
        if (str == "")
        {
            str = null;
        }

        _impl.StartsWith = str;
    }
}