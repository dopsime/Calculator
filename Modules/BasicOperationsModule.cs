using Calculator.Interfaces;

namespace Calculator.Modules
{
    internal class BasicOperationsModule : IModule
    {
        private HistoryModule historyModule;

        /// <summary>
        /// Конструктор для класса BasicOperationsModule. Инициализирует модуль истории
        /// </summary>
        /// <param name="historyModule"> Модуль истории</param>
        public BasicOperationsModule(HistoryModule historyModule)
        {
            this.historyModule = historyModule;
        }


        public void Execute()
        {
            Console.Clear();
            while (true)
            {
                try
                {
                    Console.Write("Введите первое число: ");
                    bool isValidInput1 = double.TryParse(Console.ReadLine(), out double number1);
                    if (!isValidInput1)
                    {
                        Console.WriteLine("Ошибка: введено некорректное значение.");
                        continue;
                    }

                    Console.Write("Выберите операцию (+, -, *, /) или введите 'exit' для выхода: ");
                    string operation = Console.ReadLine();
                    if (operation.ToLower() == "exit")
                    {
                        Console.Clear();
                        break;
                    }

                    Console.Write("Введите второе число: ");
                    bool isValidInput2 = double.TryParse(Console.ReadLine(), out double number2);
                    if (!isValidInput2)
                    {
                        Console.WriteLine("Ошибка: введено некорректное значение.");
                        continue;
                    }

                    double result = operation switch
                    {
                        "+" => number1 + number2,
                        "-" => number1 - number2,
                        "*" => number1 * number2,
                        "/" => number2 != 0 ? number1 / number2 : throw new DivideByZeroException("Невозможно делить на ноль."),
                        _ => throw new InvalidOperationException("Неверная операция.")
                    };
                    Console.WriteLine("Ваш результат: " + result);
                    historyModule.AddToHistory($"{number1} {operation} {number2} = {result}"); // Добавляю в историю вычисления
                    Console.ReadKey();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Ошибка: некорректный формат числа. " + ex.Message);
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла неизвестная ошибка: " + ex.Message);
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения или 'F' для выхода.");
                if (Console.ReadKey().Key == ConsoleKey.F)
                {
                    Console.Clear();
                    break;
                }

                Console.Clear();
            }
        }
    }
}
