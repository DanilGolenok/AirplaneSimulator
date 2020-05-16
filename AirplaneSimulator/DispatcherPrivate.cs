using System;

namespace AirplaneSimulator
{
    delegate void ShowMessagePenalty();

    partial class Dispatcher
    {
        ShowMessagePenalty showMessagePenalty;

        private bool isMessagePenalty;

        private void MonitorIndicators()
        {  
            int difference = planeInfo.Invoke().Height - GetRecommendedHeight();
            difference = difference < 0 ? difference * -1 : difference;

            if (planeInfo.Invoke().Speed >= 1000)
                planeInfo.Invoke().IsScoredMaxSpeed = true;

            if (planeInfo.Invoke().Speed > 1000)
            {
                OverSpeedPenalty();
                showMessagePenalty = ShowOverSpeedPenalty;
                isMessagePenalty = true;
            }

            if (difference >= 1000)
                throw new PlaneCrashed();

            else if (difference > 600 && difference < 1000)
            {
                OverHeightPenalty(100);
                showMessagePenalty = ShowOverHeightPenalty_600;
                isMessagePenalty = true;
            }

            else if (difference > 300 && difference <= 600)
            {
                OverHeightPenalty(50);
                showMessagePenalty = ShowOverHeightPenalty_300;
                isMessagePenalty = true;
            }

            if (Penalty >= 1000)
                throw new Unfit();                    
        }

        private void ConclusionAboutEnd()
        {
            if (planeInfo.Invoke().Height == 0 && !planeInfo.Invoke().IsScoredMaxSpeed)
                throw new MaximumSpeedHasNotBeenReached();

            else if (planeInfo.Invoke().Height == 0 && planeInfo.Invoke().IsScoredMaxSpeed)
                throw new CompleteFlight();
        }

        private void OverSpeedPenalty()
        {
            Penalty += 100;
        }

        private void OverHeightPenalty(int value)
        {
            Penalty += value;
        }

        private void ShowOverSpeedPenalty()
        {
            Console.Write("Скорость выше 1000км/час,");
            Console.Write(" штраф: 100 очков");
        }

        private void ShowOverHeightPenalty_300()
        {
            Console.Write($"Разница высот от рекомендуемой более 300м");
            Console.Write($", штраф: 50 очков");
        }

        private void ShowOverHeightPenalty_600()
        {
            Console.Write($"Разница высот от рекомендуемой более 600м");
            Console.Write($", штраф: 100 очков");
        }

    }
}
