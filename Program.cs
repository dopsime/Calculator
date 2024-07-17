using System;
using Calculator.Modules;
using NLog;

namespace Calculator
{
    internal class Program
    {
        static void Main()
        {

            LoggingManager.LogInfo("Программа запущена.");

            Calculators calculators = new Calculators();
            calculators.Start();

            LoggingManager.LogInfo("Программа завершена.");

        }
    }
}
