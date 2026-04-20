using System;

// static класс — нельзя создать объект,
// используется только для хранения функций
public static class Functions
{
    // ---------- Показ всех книг ----------
    public static void PrintBooks(Book[] books)
    {
        // перебираем массив
        for (int i = 0; i < books.Length; i++)
        {
            Book b = books[i];

            // выводим информацию о книге
            Console.WriteLine(
                b.Title + " (" + b.Year + ") - " +
                (b.IsTaken ? "взята" : "доступна")
            );
        }
    }


    // ---------- Взять книгу ----------
    public static void TakeBook(Book[] books, string title)
    {
        // ищем книгу по названию
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i].Title == title)
            {
                // если уже взята → ошибка
                if (books[i].IsTaken)
                {
                    Console.WriteLine("Книга уже взята");
                    return;
                }

                // меняем статус
                books[i].IsTaken = true;

                Console.WriteLine("Книга взята");
                return;
            }
        }

        // если не нашли
        Console.WriteLine("Книга не найдена");
    }


    // ---------- Вернуть книгу ----------
    public static void ReturnBook(Book[] books, string title)
    {
        for (int i = 0; i < books.Length; i++)
        {
            if (books[i].Title == title)
            {
                // если книга уже свободна
                if (!books[i].IsTaken)
                {
                    Console.WriteLine("Книга уже в библиотеке");
                    return;
                }

                // меняем статус обратно
                books[i].IsTaken = false;

                Console.WriteLine("Книга возвращена");
                return;
            }
        }

        // если книги нет
        Console.WriteLine("Ошибка: такой книги нет");
    }
}