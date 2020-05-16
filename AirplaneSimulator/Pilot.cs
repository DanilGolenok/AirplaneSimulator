using System;

namespace AirplaneSimulator
{
    class Pilot
    {
        public string Name { get; set; }
        public int Penalty { get; set; }

        public ConsoleKeyInfo GetAction()
        {
            return Console.ReadKey();
        }

        public void Init()
        {
            Console.WriteLine("Имя пилота: ");
            Console.Write("-> ");
            Name = Console.ReadLine();
            Console.Clear();
        }
    }
}
