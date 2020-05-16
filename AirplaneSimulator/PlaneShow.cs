using System;
using System.IO;

namespace AirplaneSimulator
{
    partial class Plane
    {
        private void ShowTask()
        {
            Console.WriteLine("    Добро пожаловать в Airplane Simulator.");
            Console.WriteLine();
            Console.WriteLine("    Задача пилота - взлететь на самолёте, набрать максимальную скорость 1000км/час, ");
            Console.WriteLine(" после чего посадить самолёт");
            Console.WriteLine();
            Console.WriteLine("    Процесс полёта будут контролировать диспетчеры,");
            Console.WriteLine(" предоставляя указания по показателям высоты/скорости.");
            Console.WriteLine(" Если разница от рекомендуемых диспетчерами показателями");
            Console.WriteLine(" будет сильно отличатся - пилоту будут начисляться штрафные баллы.");
            Console.WriteLine(" Если пилот набирает более 1000 штрафных баллов от любого из дисетчеров, ");
            Console.WriteLine(" то полёт прекратится, а пилот будет признан не годным к полётам.");
            Console.WriteLine();
            Console.WriteLine("    Управление самолётом осуществляется с помощью стрелочек на клавиатуре.");
            Console.WriteLine(" Пример: -> - Увеличить скорость на 50км/ч, shift + -> - Увеличить скорость на 150км/ч");
            Console.WriteLine();
            Console.WriteLine("    Если нужно вызвать меню с диспетчерами, нажмите [1]");
            Console.WriteLine();
            Continue();
        }

        private void ShowDispatchers()
        {
            Console.WriteLine("Список диспетчеров: ");

            for (int i = 0; i < dispatchers.Count; ++i)
            {
                Console.Write($"{i + 1}. ");
                dispatchers[i].ShowName();
            }
        }

        private void Continue()
        {
            ConsoleKeyInfo keyInfo;

            do
            {
                Console.Write(" Для продолжения нажмите Enter");
                keyInfo = Console.ReadKey();
                Console.Clear();
            } while (keyInfo.Key != ConsoleKey.Enter);
        }

        private void ShowPlane()
        {
            Console.WriteLine(File.ReadAllText("plane.txt"));
        }

        private void ShowDispatchersMonitor()
        {
            foreach (Dispatcher dispatcher in dispatchers)
            {
                dispatcher.ShowMonitor();
            }
        }

        private int  ShowRecommendedHeight()
        {
            int averageHeight = 0;
            int sum = 0;

            foreach (Dispatcher dispatcher in dispatchers)
            {
                sum += dispatcher.GetRecommendedHeight();
            }

            try
            {
                averageHeight = sum / dispatchers.Count;
            }
            catch (DivideByZeroException)
            {
                
            }

            return averageHeight;
        }

        private void ShowProcess()
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t Пилот: {pilot.Name}");            
            Console.WriteLine();
            Console.WriteLine($"Текущая скорость: {Speed}км/час");
            Console.WriteLine($"Текущая высота: {Height}м, \t\t Рекомендуемая высота {ShowRecommendedHeight()}м");
            Console.WriteLine();
            ShowPlane();
            Console.WriteLine();
            Console.WriteLine("[1] Список диспетчеров: ");
            ShowDispatchersMonitor();
        }

        private void ShowException(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            ShowFail();
        }

        private void ShowDispatchersInfo()
        {
            Console.WriteLine("Подведение итогов: \n");
            foreach (Dispatcher dispatcher in dispatchers)
            {
                dispatcher.ShowInfo();
            }
        }

        private void ShowComplete()
        {
            ShowPlane();
            Console.WriteLine();
            ShowDispatchersInfo();
            Console.WriteLine();
            Console.WriteLine($"Общая сумма штрафов: {pilot.Penalty}");
            Console.WriteLine();
        }

        private void ShowFail()
        {
            ShowPlane();
            Console.WriteLine();
            Console.WriteLine("Полёт провален, пилот был признан не годным");
            Console.WriteLine();  
        }

        private bool IsEndTraining()
        {
            Reset();

            int choice;

            do
            {
                Console.WriteLine("[1] Начать полёт снова");
                Console.WriteLine("[0] Завершить");
                Console.Write("-> ");
                while (!int.TryParse(Console.ReadLine(), out choice));
                Console.Clear();

            } while (choice < 0 || choice > 1);

            return choice == 0;
        }
    }
}
