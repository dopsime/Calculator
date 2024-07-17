using Calculator.Interfaces;

namespace Calculator.Modules
{
    internal class AdvancedOperationsModule : IModule
    {
        private HistoryModule historyModule;

        public AdvancedOperationsModule(HistoryModule historyModule)
        {
            this.historyModule = historyModule;
        }

        public void Execute()
        {
            string[] listOperation =
            {
                "1. Синус",
                "2. Косинус",
                "3. Тангенс",
                "4. Логарифмы",
                "5. Арксинус",
                "6. Арккосинус",
                "7. Арктангенс",
                "8. Натуральный логарифм",
                "9. Десятичный логарифм",
                "10. Значение числа Пи",
                "11. Выход"
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите операцию");

                foreach (string operation in listOperation) Console.WriteLine(operation);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CalculateTrigonometricFunction(Math.Sin);
                        break;
                    case "2":
                        CalculateTrigonometricFunction(Math.Cos);
                        break;
                    case "3":
                        CalculateTrigonometricFunction(Math.Tan);
                        break;
                    case "4":
                        PerformLogarithm();
                        break;
                    case "5":
                        CalculateArcTrigonometricFunction(Math.Asin);
                        break;
                    case "6":
                        CalculateArcTrigonometricFunction(Math.Acos);
                        break;
                    case "7":
                        CalculateArcTrigonometricFunction(Math.Atan);
                        break;
                    case "8":
                        CalculateLogarithm(Math.Log);
                        break;
                    case "9":
                        CalculateLogarithm(Math.Log10);
                        break;
                    case "10":
                        Console.WriteLine($"Значение числа Пи: {Math.PI}");
                        Console.ReadKey();
                        break;
                    case "11":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CalculateTrigonometricFunction(Func<double, double> trigFunction)
        {
            Console.Write("Введите значение: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Выберите единицу измерения:");
                Console.WriteLine("1. Радианы");
                Console.WriteLine("2. Градусы");

                string unitChoice = Console.ReadLine();

                double result = 0;

                switch (unitChoice)
                {
                    case "1":
                        result = trigFunction(value);
                        break;
                    case "2":
                        result = trigFunction(value * Math.PI / 180.0); // Перевод градусов в радианы
                        break;
                    default:
                        Console.WriteLine("Неверный выбор единицы измерения.");
                        Console.ReadKey();
                        return;
                }

                Console.WriteLine($"Результат: {result}");
                historyModule.AddToHistory($"Вычисление {trigFunction.Method.Name}({value}) = {result}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                Console.ReadKey();
            }
        }

        private void CalculateArcTrigonometricFunction(Func<double, double> arcTrigFunction)
        {
            Console.Write("Введите значение: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                if (arcTrigFunction == Math.Asin || arcTrigFunction == Math.Acos)
                {
                    if (value < -1 || value > 1)
                    {
                        Console.WriteLine("Для функций арксинуса и арккосинуса значение должно быть в диапазоне от -1 до 1.");
                        Console.ReadKey();
                        return;
                    }
                }

                double result = arcTrigFunction(value);
                Console.WriteLine($"Результат: {result}");
                historyModule.AddToHistory($"Вычисление {arcTrigFunction.Method.Name}({value}) = {result}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                Console.ReadKey();
            }
        }

        private void PerformLogarithm()
        {
            Console.Write("Введите значение: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                Console.Write("Введите основание логарифма: ");
                if (double.TryParse(Console.ReadLine(), out double baseValue))
                {
                    double result = Math.Log(value, baseValue);
                    Console.WriteLine($"Результат: {result}");
                    historyModule.AddToHistory($"Вычисление Log({baseValue}) = {result}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                Console.ReadKey();
            }
        }
        private void CalculateLogarithm(Func<double, double> logFunction)
        {
            Console.Write("Введите значение: ");
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                double result = logFunction(value);
                Console.WriteLine($"Результат: {result}");
                historyModule.AddToHistory($"Вычисление Log функций({logFunction}), ({value}) = {result}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, введите число.");
                Console.ReadKey();
            }
        }
    }
}
