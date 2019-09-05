namespace Elevator2
{
    public class Elevator
    {
        public string Name { get; }
        public int TopFLoor { get; }
        public int LowestFloor { get;  }
        public int CurrentFloor { get; private set; }
        public int UntilMaintenance { get; private set; }
        public bool IsPower => UntilMaintenance > 0;
        public Elevator(string name, int topFloor, int lowestFloor, int currentFloor)
        {
            Name = name;
            TopFLoor = topFloor;
            LowestFloor = lowestFloor;
            CurrentFloor = currentFloor;
        }
        public Elevator(string name, int topFloor, int lowestFloor, int currentFloor, int untilMaintenance)
        {
            Name = name;
            TopFLoor = topFloor;
            LowestFloor = lowestFloor;
            UntilMaintenance = untilMaintenance;
            CurrentFloor = currentFloor;
        }
        public Elevator(string name, int topFloor, int lowestFloor) : this (name,topFloor,lowestFloor,10)
        { 
        }

        public ElevatorMoveResponse TryGoUp(Elevator elevator)

        {
            if (CurrentFloor >= TopFLoor)
            {
                return ElevatorMoveResponse.cantGoDown;
            }
            if (!IsPower)
            {
                return ElevatorMoveResponse.powerIsOff;
            }
            CurrentFloor++;
            WearAndTear();
            return ElevatorMoveResponse.succes;
        }
        public ElevatorMoveResponse TryGoDown(Elevator elevator)
        {
            if (CurrentFloor <= LowestFloor)
            {
                return ElevatorMoveResponse.cantGoDown;
            }
            if (!IsPower)
            {
                return ElevatorMoveResponse.powerIsOff;
            }
            CurrentFloor--;
            WearAndTear();
            return ElevatorMoveResponse.succes;
        }
        public void WearAndTear()
        {
            if (UntilMaintenance > 0)
            {
                UntilMaintenance--;
            }
        }
    }
}