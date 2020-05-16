using System;

namespace AirplaneSimulator
{
    partial class Plane
    {
        public bool IsFlightStarted { get; set; } = false;

        public bool IsScoredMaxSpeed { get; set; }

        private void ChangeSpeed(int value)
        {
            if (value < 0)
            {
                if ((Speed + value) < 0)
                    Speed = 0;
            }

            Speed += value;
        }

        private void ChangeHeight(int value)
        {
            if (value < 0)
            {
                if ((Height + value) < 0)
                    Height = 0;

                else Height += value;
            }
            else
                Height += value;
            
        }

        private void HeightUp(ConsoleKeyInfo actionInfo)
        {
            if (AirplaneSimulator.Action.IsPower(actionInfo, actionInfo.Key))
                ChangeHeight(500);
            else if (AirplaneSimulator.Action.IsNormal(actionInfo, actionInfo.Key))
                ChangeHeight(250);
        }

        private void HeightDown(ConsoleKeyInfo actionInfo)
        {
            if (AirplaneSimulator.Action.IsPower(actionInfo, actionInfo.Key))
                ChangeHeight(-500);
            else if (AirplaneSimulator.Action.IsNormal(actionInfo, actionInfo.Key))
                ChangeHeight(-250);
        }

        private void SpeedUp(ConsoleKeyInfo actionInfo)
        {
            if (AirplaneSimulator.Action.IsPower(actionInfo, actionInfo.Key))
                ChangeSpeed(150);
            else if (AirplaneSimulator.Action.IsNormal(actionInfo, actionInfo.Key))
                ChangeSpeed(50);
        }

        private void SpeedDown(ConsoleKeyInfo actionInfo)
        {
            if (AirplaneSimulator.Action.IsPower(actionInfo, actionInfo.Key))
                ChangeSpeed(-150);
            else if (AirplaneSimulator.Action.IsNormal(actionInfo, actionInfo.Key))
                ChangeSpeed(-50);
        }

        private void CalcPilotPenalty()
        {
            int sum = 0;
            foreach (Dispatcher dispatcher in dispatchers)
            {
                sum += dispatcher.Penalty;
            }

            pilot.Penalty = sum;
        }

        private void Action()
        {
            ConsoleKeyInfo ActionInfo = pilot.GetAction();
            Console.Clear();

            if (AirplaneSimulator.Action.IsStartFlight(ActionInfo)) IsFlightStarted = true;

            switch (ActionInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    HeightUp(ActionInfo);
                    break;

                case ConsoleKey.DownArrow:
                    HeightDown(ActionInfo);
                    break;
                
                case ConsoleKey.RightArrow:
                    SpeedUp(ActionInfo);
                    break;

                case ConsoleKey.LeftArrow:
                    SpeedDown(ActionInfo);
                    break;

                case ConsoleKey.D1:
                    MenuDispatchers();
                    break;

                default:
                    break;
            }
        }

        private bool IsNormalFlight()
        {
            bool isNormalFlight = true;

            try
            {
                foreach (Dispatcher dispatcher in dispatchers)
                {
                    dispatcher.Monitor();
                }
            }
            catch (IndicatorIsBelowZero indicatorIsBelowZero)
            {
                Console.Clear();
                Console.WriteLine($"Высота: {Height}м, скорость: {Speed}км/час");
                Console.WriteLine();
                ShowException(indicatorIsBelowZero.Message);
                isNormalFlight = false;
            }
            catch (LackOfSpeedDuringTheFlight lackOfSpeedDuringTheFlight)
            {
                Console.Clear();
                Console.WriteLine($"Высота: {Height}м, скорость: {Speed}км/час");
                Console.WriteLine();
                ShowException(lackOfSpeedDuringTheFlight.Message);
                isNormalFlight = false;
            }
            catch (PlaneCrashed planeCrashed)
            {
                Console.Clear();
                Console.WriteLine($"Текущая высота: {Height}м, рекомендуемая высота: {ShowRecommendedHeight()}м");
                ShowException(planeCrashed.Message);
                isNormalFlight = false;
            }
            catch (Unfit unfit)
            {
                Console.Clear();
                ShowException(unfit.Message);
                isNormalFlight = false;
            }
            catch (MaximumSpeedHasNotBeenReached maximumSpeedHasNotBeenReached)
            {
                Console.Clear();
                ShowException(maximumSpeedHasNotBeenReached.Message);
                isNormalFlight = false;
            }
            catch (CompleteFlight completeFlight)
            {
                Console.Clear();
                Console.WriteLine(completeFlight.Message);
                Console.WriteLine();
                ShowComplete();
                isNormalFlight = false;
            }

            CalcPilotPenalty();
            
            return isNormalFlight;
        }

        private void Process()
        {
            do
            {
                ShowProcess();
                Action();             
            } while (IsNormalFlight());           
        }
    }
}
