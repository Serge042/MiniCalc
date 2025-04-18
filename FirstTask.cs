using System;

// Интерфейс для операции сложения
public interface IAdder
{
    double Add(double a, double b);
}

// Реализация интерфейса
public class Calculator : IAdder
{
    public double Add(double a, double b)
    {
        return a + b;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем экземпляр калькулятора
        IAdder adder = new Calculator();
        
        Console.WriteLine("Мини-калькулятор для сложения двух чисел");
        
        try
        {
            // Ввод первого числа
            Console.Write("Введите первое число: ");
            double number1 = GetNumberFromUser();
            
            // Ввод второго числа
            Console.Write("Введите второе число: ");
            double number2 = GetNumberFromUser();
            
            // Выполнение сложения
            double result = adder.Add(number1, number2);
            
            // Вывод результата
            Console.WriteLine($"Результат сложения: {number1} + {number2} = {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введено некорректное число. Пожалуйста, вводите только числа.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Ошибка: Введено слишком большое или слишком маленькое число.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Спасибо за использование калькулятора!");
        }
    }
    
    // Метод для безопасного получения числа от пользователя
    private static double GetNumberFromUser()
    {
        string input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new FormatException("Пустой ввод");
        }
        return double.Parse(input);
    }
}
