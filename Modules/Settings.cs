using System;
using System.IO;
using System.Text.Json;

namespace Calculator.Modules
{
    public class Settings
    {
        private const string SettingsFilePath = "settings.json";

        public bool IsLoggingEnabled { get; set; }
        public bool IsRainbowAuthorEnabled { get; set; }

        /// <summary>
        /// Загружает настройки из файла.
        /// </summary>
        public static Settings Load()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    return JsonSerializer.Deserialize<Settings>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке настроек: {ex.Message}");
                }
            }

            return new Settings(); // Возвращаем новый экземпляр по умолчанию, если файл не найден или возникла ошибка при чтении
        }

        /// <summary>
        /// Сохраняет настройки в файл.
        /// </summary>
        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении настроек: {ex.Message}");
            }
        }
    }
}
