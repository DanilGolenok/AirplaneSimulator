using System;

namespace AirplaneSimulator
{
    partial class Plane
    {
        private PlaneInfo planeInfo;

        private void MenuDispatchers()
        {
            int choice;

            do
            {
                Console.WriteLine("[1] Добавить диспетчера");
                Console.WriteLine("[2] Удалить диспетчера");
                Console.WriteLine();
                Console.WriteLine("[0] Назад");
                Console.Write("-> ");
                while (!int.TryParse(Console.ReadLine(), out choice));
                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Add();
                        break;

                    case 2:
                        Remove();
                        break;

                    default:
                        break;
                }

            } while (choice != 0);

        }

        private int EnterNumDispatchers()
        {
            int numDispatchers;

            do
            {
                Console.WriteLine("Введите количество диспетчеров: ");
                Console.Write("-> ");

                while (!int.TryParse(Console.ReadLine(), out numDispatchers));
                Console.Clear();

                if(numDispatchers < 2)
                    Console.WriteLine("Количество диспетчеров не может быть меньше двух\n");

            } while (numDispatchers < 2);


            return numDispatchers;
        }

        private void InitDispatchers()
        {
            int numDispatchers = EnterNumDispatchers();

            for(int i = 0; i < numDispatchers; ++i)
            {
                dispatchers.Add(new Dispatcher(planeInfo));

                Console.WriteLine($"Диспетчер #{i + 1}: \n");
                dispatchers[i].Init();
            }
            Console.Clear();

            ShowDispatchers();

            Console.WriteLine();
            Continue();
        }

        private Dispatcher GetNewDispatcher()
        {
            Console.WriteLine("Введите имя нового диспетчера: ");
            Console.Write("-> ");
            
            return new Dispatcher(planeInfo) { Name = Console.ReadLine() };
        }

        private void Add()
        {
            dispatchers.Add(GetNewDispatcher());
            Console.Clear();
            Console.WriteLine("Новый диспетчер успешно добавлен");
            Console.WriteLine();
            Continue();
        }

        private void RemoveDispatcher()
        {
            int choice;

            ShowDispatchers();
            Console.WriteLine("Выберите диспетчера для удаления:");
            Console.Write("-> ");
            while (!int.TryParse(Console.ReadLine(), out choice));
            Console.Clear();

            dispatchers.Remove(dispatchers[choice - 1]);

            Console.WriteLine("Диспетчер успешно удалён");
            Console.WriteLine();
            ShowDispatchers();
        }

        private void Remove()
        {
            if(dispatchers.Count > 2)
            {
                RemoveDispatcher();
            }
            else Console.WriteLine("Количество диспетчеров не может быть меньше двух");

            Console.WriteLine();
            Continue();
        }

        private void Reset()
        {
            Speed = Height = 0;
            dispatchers.Clear();
            pilot.Penalty = 0;
            IsFlightStarted = false;
            IsScoredMaxSpeed = false;
        }

    }
}
