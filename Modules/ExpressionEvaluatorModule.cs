using System.Data;
using Calculator.Interfaces;

namespace Calculator.Modules
{
    internal class ExpressionEvaluatorModule : IModule
    {
        private HistoryModule historyModule;

        public ExpressionEvaluatorModule(HistoryModule historyModule)
        {
            this.historyModule = historyModule;
        }

        public void Execute()
        {
            Console.WriteLine("Данная операция не поддерживает сложные математические функции. \nТакие как, тригонометрические функции, возведение в степень и логарифмы.");
            Console.ReadKey();
            Console.Clear();
            Console.Write("Введите выражение: ");
            string expression = Console.ReadLine();

            try
            {
                var result = EvaluateException(expression);
                Console.WriteLine($"Результат: {result}");
                historyModule.AddToHistory($"Вычисление выражения: {expression}, = {result}");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при вычисление выражения: {ex.Message}");
            }
        }

        /// <summary>
        /// Оценивает математическое выражение с помощью DataTable
        /// </summary>
        /// <param name="expression">Математическое выражение для оценки</param>
        /// <returns>Результат вычисленного математического выражения</returns>
        private double EvaluateException(string expression)
        {
            // DataTable для вычисления выражения
            DataTable table = new DataTable();
            var result = table.Compute(expression, string.Empty);
            return Convert.ToDouble(result);
        }
    }
}
