using System;

namespace AirplaneSimulator
{
    class PlaneCrashed : SystemException
    {
        public override string Message => "Самолёт разбился, разница высоты от рекомендуемой больше 1000м";
    }

    class Unfit : SystemException
    {
        public override string Message => "Сумма штрафов от одного из диспетчеров превысила 1000";
    }

    class IndicatorIsBelowZero : SystemException
    {
        public override string Message => "Один из показателей ниже нуля";
    }

    class LackOfSpeedDuringTheFlight : SystemException
    {
        public override string Message => "Самолёт разбился из-за отсутствия скорости во время полёта";
    }

    class MaximumSpeedHasNotBeenReached : SystemException
    {
        public override string Message => "Самолёт приземлился не набрав максимальной скорости во время полёта (1000км/час)";
    }

    class CompleteFlight : SystemException
    {
        public override string Message => "Симулируемый полёт успешно сдан";
    }


}
