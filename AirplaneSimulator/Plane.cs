using System.Collections.Generic;

namespace AirplaneSimulator
{
    delegate Plane PlaneInfo();    

    partial class Plane
    {        
        private Pilot pilot = new Pilot();
        private List<Dispatcher> dispatchers = new List<Dispatcher>();
        public int Speed { get; set; }
        public int Height { get; set; }
        
        public Plane GetInfo() => this;

        public void StartFlight()
        {
            pilot.Init();

            do
            {
                InitDispatchers();
                ShowTask();
                Process();
            } while (!IsEndTraining());
        }

        public Plane()
        {
            planeInfo = GetInfo;
        }
    }
}
