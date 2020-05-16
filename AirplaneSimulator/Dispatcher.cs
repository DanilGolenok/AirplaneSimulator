using System;

namespace AirplaneSimulator
{
    partial class Dispatcher
    {
        private PlaneInfo planeInfo;
        public string Name { get; set; }
        public int WeatherCorrection { get; set; }
        public int Penalty { get; set; }

        public int GetRecommendedHeight() => (6 * planeInfo.Invoke().Speed) - WeatherCorrection;

        public void Monitor()
        {    
            if(planeInfo.Invoke().IsFlightStarted)
            {
                if (planeInfo.Invoke().Speed < 0 || planeInfo.Invoke().Height < 0)
                    throw new IndicatorIsBelowZero();

                if (planeInfo.Invoke().Speed == 0 && planeInfo.Invoke().Height > 0)
                    throw new LackOfSpeedDuringTheFlight();

                if (planeInfo.Invoke().Speed >= 50)
                {
                    try
                    {
                        MonitorIndicators();
                    }
                    catch (LackOfSpeedDuringTheFlight lackOfSpeedDuringTheFlight)
                    {
                        throw lackOfSpeedDuringTheFlight;
                    }
                    catch (PlaneCrashed planeCrashed)
                    {
                        throw planeCrashed;
                    }
                    catch (Unfit unfit)
                    {
                        throw unfit;
                    }
                }
                else
                {
                    try
                    {
                        ConclusionAboutEnd();
                    }
                    catch (MaximumSpeedHasNotBeenReached maximumSpeedHasNotBeenReached)
                    {
                        throw maximumSpeedHasNotBeenReached;
                    }
                    catch (CompleteFlight endFlight)
                    {
                        throw endFlight;
                    }
                }
            }
        }

        public void Init()
        {
            Console.WriteLine("Введите имя диспетчера: ");
            Console.Write("-> ");
            Name = Console.ReadLine();
            Console.Clear();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Диспетчер {Name}, штарфные очки: {Penalty} ");
        }

        public void ShowMonitor()
        {
            Console.Write($"Диспетчер {Name}, штарфные очки: {Penalty} ");
            if (isMessagePenalty)
                showMessagePenalty.Invoke();
            isMessagePenalty = false;
            Console.WriteLine();
            
        }

        public void ShowName()
        {
            Console.WriteLine(Name);
        }

        public Dispatcher(PlaneInfo planeInfo)
        {
            this.planeInfo = planeInfo;

            Random random = new Random();
            WeatherCorrection = random.Next(-200, 200);
        }
    }
}
 