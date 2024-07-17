using Calculator.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace Calculator.Modules
{
    internal class HistoryModule : IModule
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly List<string> history;
        private readonly int maxHistorySize = 100;
        private readonly string historyFilePath = "history.txt";

        /// <summary>
        /// Конструктор для класса HistoryModule. Инициализирует список истории и загружает историю из файла.
        /// </summary>
        public HistoryModule()
        {
            history = new List<string>();
            LoadHistory();
        }

        public void Execute()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("История операций:");
                ShowHistory();

                Console.WriteLine("1. Очистить историю");
                Console.WriteLine("2. Назад");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        ClearHistory();
                        SaveHistory();
                        break;
                    case '2':
                        SaveHistory();
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        /// <summary>
        /// Добавляет новую запись в список. Если размер превышает максимальный лимит "maxHistorySize = 100", самая старая запись удаляется
        /// </summary>
        /// <param name="entry">Запись, которая должна быть добавлена в список истории.</param>
        public void AddToHistory(string entry)
        {
            if (history.Count >= maxHistorySize)
            {
                history.RemoveAt(0); // Удаляем самую старую запись, если превышен лимит
            }
            history.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {entry}");
            LoggingManager.LogInfo($"Добавлено в историю: {entry}");
        }

        /// <summary>
        /// Очищает список истории и выводит сообщение о том, что история была очищена.
        /// </summary>
        public void ClearHistory()
        {
            history.Clear();
            Console.WriteLine("История очищена.");
        }

        /// <summary>
        /// Выводит содержимое списка истории на консоль.
        /// </summary>
        public void ShowHistory()
        {
            foreach (var entry in history)
            {
                Console.WriteLine(entry);
            }
            LoggingManager.LogInfo("Отображаемые истории.");
        }

        private void LoadHistory()
        {
            if (File.Exists(historyFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(historyFilePath);
                    history.AddRange(lines);
                    LoggingManager.LogInfo("История загрузилась из файла.");
                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке истории из файла: {ex.Message}");
                }
            }
        }

        private void SaveHistory()
        {
            try
            {
                File.WriteAllLines(historyFilePath, history);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении истории в файл: {ex.Message}");
            }
        }
    }
}
