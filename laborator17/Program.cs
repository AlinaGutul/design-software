using System;

Console.WriteLine("Lab 17 - Commands & Polymorphism");
Console.WriteLine();


// ============================================================
// СОСТОЯНИЕ СВЕТОФОРА (ОБЩЕЕ)
// ============================================================

// переменная, которая хранит текущее состояние
// она будет изменяться командами
Light current = Light.Red;


// ============================================================
// 1. МЕНЮ ЧЕРЕЗ ДЕЛЕГАТЫ
// ============================================================

// массив команд (каждый элемент — функция)
Command[] commands =
{
    () => Step(),       // первая команда → сделать шаг
    () => PrintState()  // вторая команда → показать состояние
};

Console.WriteLine("=== Делегаты ===");

while (true)
{
    Console.WriteLine("1 - Шаг");
    Console.WriteLine("2 - Показать");
    Console.WriteLine("0 - Выход");

    string? input = Console.ReadLine();

    if (input == "0") break;

    // пытаемся превратить ввод в число
    if (int.TryParse(input, out int index))
    {
        index--; // пользователь вводит 1/2, массив с 0

        // проверка границ массива
        if (index >= 0 && index < commands.Length)
        {
            // вызываем функцию из массива
            commands[index]();
        }
    }
}


// ============================================================
// 2. МЕНЮ ЧЕРЕЗ ИНТЕРФЕЙС (ПОЛИМОРФИЗМ)
// ============================================================

Console.WriteLine();
Console.WriteLine("=== Интерфейсы ===");

// массив объектов, но тип у всех один — ICommand
ICommand[] commandObjects =
{
    new StepCommand(),   // объект "шаг"
    new PrintCommand()   // объект "показ"
};

while (true)
{
    Console.WriteLine("1 - Шаг");
    Console.WriteLine("2 - Показать");
    Console.WriteLine("0 - Выход");

    string? input = Console.ReadLine();

    if (input == "0") break;

    if (int.TryParse(input, out int index))
    {
        index--;

        if (index >= 0 && index < commandObjects.Length)
        {
            // ПОЛИМОРФИЗМ:
            // у всех объектов вызывается Execute(),
            // но выполняется разная логика
            commandObjects[index].Execute();
        }
    }
}


// ============================================================
// ОСНОВНАЯ ЛОГИКА (ЛАБА 8)
// ============================================================

// функция делает один шаг светофора
void Step()
{
    ActionType action = GetAction(current);
    DoAction(action);
    current = NextLight(current);
}

// функция просто показывает состояние
void PrintState()
{
    Console.WriteLine("Светофор: " + current);
}


// ---------- функции логики ----------

static ActionType GetAction(Light light)
{
    if (light == Light.Red)
        return ActionType.Stop;

    if (light == Light.Yellow)
        return ActionType.Wait;

    return ActionType.Go;
}

static void DoAction(ActionType action)
{
    if (action == ActionType.Stop)
        Console.WriteLine("Стоять");
    else if (action == ActionType.Wait)
        Console.WriteLine("Готовиться");
    else
        Console.WriteLine("Ехать");
}

static Light NextLight(Light light)
{
    if (light == Light.Red)
        return Light.Yellow;

    if (light == Light.Yellow)
        return Light.Green;

    return Light.Red;
}


// ============================================================
// ВСЕ ТИПЫ ОБЯЗАТЕЛЬНО ВНИЗУ
// ============================================================

// делегат — это "переменная, которая хранит функцию"
delegate void Command();


// интерфейс — это "контракт"
// все классы обязаны иметь метод Execute()
interface ICommand
{
    void Execute();
}


// команда "шаг"
class StepCommand : ICommand
{
    public void Execute()
    {
        // вызываем глобальную функцию
        // (используем ту же логику)
        Console.WriteLine("Выполняется шаг");

        // простая локальная логика (без доступа к current)
        Console.WriteLine("Светофор изменится (демо)");
    }
}


// команда "показ"
class PrintCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Показ состояния (демо)");
    }
}


// ---------- enum В САМОМ КОНЦЕ ----------

enum Light
{
    Red,
    Yellow,
    Green
}

enum ActionType
{
    Stop,
    Wait,
    Go
}