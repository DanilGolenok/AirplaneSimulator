using System;

namespace AirplaneSimulator
{
    class Action
    {
        public static bool IsNormal(ConsoleKeyInfo keyInfo, ConsoleKey Direction)   
        {
            return keyInfo.Key == Direction; 
        }
        
        public static bool IsPower(ConsoleKeyInfo keyInfo, ConsoleKey Direction)
        {
            return keyInfo.Modifiers == ConsoleModifiers.Control && keyInfo.Key == Direction;
        }       

        public static bool IsStartFlight(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key == ConsoleKey.UpArrow ||
                   keyInfo.Key == ConsoleKey.DownArrow ||
                   keyInfo.Key == ConsoleKey.LeftArrow ||
                   keyInfo.Key == ConsoleKey.RightArrow;
        }
    }
}
