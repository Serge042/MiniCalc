using System;

// Интерфейс для операции сложения
public interface IAdder
{
    double Add(double a, double b);
}

// Интерфейс логгера
public interface ILogger
{
    void LogEvent(string message);
    void LogError(string message);
}

// Реализация калькулятора
public class Calculator : IAdder
{
    private readonly ILogger _logger;

    // Внедрение зависимости через конструктор
    public Calculator(ILogger logger)
    {
        _logger = logger;
    }

    public double Add(double a, double b)
    {
        _logger.LogEvent($"Выполняется сложение: {a} + {b}");
        return a + b;
    }
}

// Реализация цветного логгера
public class ConsoleLogger : ILogger
{
    public void LogEvent(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[СОБЫТИЕ] {DateTime.Now:HH:mm:ss}: {message}");
        Console.ResetColor();
    }

    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ОШИБКА] {DateTime.Now:HH:mm:ss}: {message}");
        Console.ResetColor();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Ручное создание зависимостей (простой DI-контейнер)
        ILogger logger = new ConsoleLogger();
        IAdder adder = new Calculator(logger);

        logger.LogEvent("Запуск мини-калькулятора");
        
        try
        {
            // Ввод первого числа
            Console.Write("Введите первое число: ");
            double number1 = GetNumberFromUser(logger);
            
            // Ввод второго числа
            Console.Write("Введите второе число: ");
            double number2 = GetNumberFromUser(logger);
            
            // Выполнение сложения
            double result = adder.Add(number1, number2);
            
            // Вывод результата
            Console.WriteLine($"Результат сложения: {number1} + {number2} = {result}");
            logger.LogEvent("Операция выполнена успешно");
        }
        catch (FormatException)
        {
            logger.LogError("Введено некорректное число. Пожалуйста, вводите только числа.");
        }
        catch (OverflowException)
        {
            logger.LogError("Введено слишком большое или слишком маленькое число.");
        }
        catch (Exception ex)
        {
            logger.LogError($"Произошла непредвиденная ошибка: {ex.Message}");
        }
        finally
        {
            logger.LogEvent("Завершение работы калькулятора");
        }
    }
    
    // Метод для безопасного получения числа от пользователя
    private static double GetNumberFromUser(ILogger logger)
    {
        string input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            logger.LogError("Получен пустой ввод");
            throw new FormatException("Пустой ввод");
        }
        
        try
        {
            return double.Parse(input);
        }
        catch (Exception ex)
        {
            logger.LogError($"Ошибка при парсинге числа: {ex.Message}");
            throw;
        }
    }
}
