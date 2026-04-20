using System;

Console.WriteLine("Lab 8 - Traffic Light");
Console.WriteLine();

Light current = Light.Red; // начальное состояние

while (true)
{
    Console.WriteLine("Светофор: " + current);

    ActionType action = GetAction(current); // определяем действие
    DoAction(action); // выполняем действие

    Console.WriteLine();

    current = NextLight(current); // переключаем цвет

    Thread.Sleep(1000);
}


// ---------- функции ----------

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


// ---------- enum В КОНЦЕ ----------

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