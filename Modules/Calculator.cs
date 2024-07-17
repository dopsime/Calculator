using Calculator.Interfaces;
using NLog;

namespace Calculator.Modules
{
    internal class Calculators
    {
        private readonly List<IModule> _modules;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Settings _settings;

        public Calculators()
        {
            var historyModule = new HistoryModule();

            _modules = new List<IModule>
            {
                new BasicOperationsModule(historyModule  ),
                new AdvancedOperationsModule(historyModule  ),
                historyModule,
                new ExpressionEvaluatorModule(historyModule  ),
            };

            _settings = Settings.Load();
            if (_settings.IsLoggingEnabled)
            {
                var config = new NLog.Config.XmlLoggingConfiguration("NLog.config", true);
                LogManager.Configuration = config;
                Logger.Info("Логирование включено.");
            }

            LoggingManager.IsLoggingEnabled = _settings.IsLoggingEnabled;

            if (LoggingManager.IsLoggingEnabled)
            {
                var config = new NLog.Config.XmlLoggingConfiguration("NLog.config", true);
                LogManager.Configuration = config;
                LoggingManager.LogInfo("Логирование включено.");
            }
        }

        /// <summary>
        /// Запуск калькулятора
        /// </summary>
        public void Start()
        {
            if (_settings.IsRainbowAuthorEnabled)
            {
                PrintRainbowTextWithDelay();
            }
            Console.WriteLine("Добро пожаловать в калькулятор!");

            while (true)
            {

                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1. Основные операции (+, -, *, /)");
                Console.WriteLine("2. Продвинутые операции (тригонометрические функции, логарифмы)");
                Console.WriteLine("3. История операций");
                Console.WriteLine("4. Вычисление выражения");
                Console.WriteLine("5. Настройки");
                Console.WriteLine("6. Выход");

                ConsoleKeyInfo key = Console.ReadKey();  // Запрос ввода пользователя и обработка выбора
                Console.WriteLine(); // Переход на новую строку после ввода

                switch (key.KeyChar)
                {
                    case '1':
                        _modules[0].Execute();
                        break;
                    case '2':
                        _modules[1].Execute();
                        break;
                    case '3':
                        _modules[2].Execute();
                        break;
                    case '4':
                        _modules[3].Execute();
                        break;
                    case '5':
                        ShowSettings();
                        break;
                    case '6':
                        return;
                    default:
                        Console.WriteLine("Неверная операция. Пожалуйста, выберите операцию от 1 до 5.");
                        break;
                }
            }
        }

        private void ShowSettings()
        {
            while (true)
            {
                Console.WriteLine("Настройки:");
                Console.WriteLine($"1. Радужный автор: {(_settings.IsRainbowAuthorEnabled ? "Включен" : "Выключен")}");
                Console.WriteLine($"2. Логирование: {(_settings.IsLoggingEnabled ? "Включено" : "Выключено")}");
                Console.WriteLine("3. Назад");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        _settings.IsRainbowAuthorEnabled = !_settings.IsRainbowAuthorEnabled;
                        Console.WriteLine(_settings.IsRainbowAuthorEnabled ? "Радужный текст включен." : "Радужный текст выключен.");
                        break;
                    case '2':
                        _settings.IsLoggingEnabled = !_settings.IsLoggingEnabled;
                        LoggingManager.IsLoggingEnabled = _settings.IsLoggingEnabled;
                        Console.WriteLine(_settings.IsLoggingEnabled ? "Логирование включено." : "Логирование выключено.");
                        if (_settings.IsLoggingEnabled)
                        {
                            var config = new NLog.Config.XmlLoggingConfiguration("NLog.config", true);
                            LogManager.Configuration = config;
                            Console.Clear();
                            LoggingManager.LogInfo("Логирование включено.");
                        }
                        else
                        {
                            Console.Clear();
                            LogManager.Shutdown();
                            Console.WriteLine("Логирование выключено.");
                        }
                        break;
                    case '3':
                        Console.Clear();
                        _settings.Save();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 3.");
                        break;
                }
            }
        }

        static void PrintRainbowTextWithDelay()
        {
            string text = @"
              ________  ________  ________  ________  ___  _____ ______   _______      
             |\   ___ \|\   __  \|\   __  \|\   ____\|\  \|\   _ \  _   \|\  ___ \     
             \ \  \_|\ \ \  \|\  \ \  \|\  \ \  \___|\ \  \ \  \\\__\ \  \ \   __/|    
              \ \  \ \\ \ \  \\\  \ \   ____\ \_____  \ \  \ \  \\|__| \  \ \  \_|/__  
               \ \  \_\\ \ \  \\\  \ \  \___|\|____|\  \ \  \ \  \    \ \  \ \  \_|\ \ 
                \ \_______\ \_______\ \__\     ____\_\  \ \__\ \__\    \ \__\ \_______\
                 \|_______|\|_______|\|__|    |\_________\|__|\|__|     \|__|\|_______|
                                              \|_________|                              
            ";

            // Разделение текста на строки
            string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Цвета для радужного эффекта
            ConsoleColor[] colors = {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan
            };

            // Установка размера консоли
            Console.WindowWidth = 100;
            Console.WindowHeight = 30;

            // Печать текста с радужными цветами
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (char c in lines[i])
                {
                    // Выбор случайного цвета из массива colors
                    ConsoleColor color = colors[new Random().Next(colors.Length)];
                    Console.ForegroundColor = color;
                    Console.Write(c);
                    Thread.Sleep(5); // Задержка для постепенного появления
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Gray; // Возврат цвета по умолчанию
        }

    }
}
