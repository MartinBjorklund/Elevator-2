using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Elevator2
{
    class Program
    {
        static void Main()
        {
            List<Elevator> elevators = LoadElevatorsFromFile();

            while (true)
            {
                DisplayHeader();
                DisplayStatus(elevators);
                Elevator elevator = AskWhichElevatorToMove(elevators);
                Direction direction = AskForDirection();
                ElevatorMoveResponse response;

                if (direction == Direction.U)
                    response = elevator.TryGoUp(elevator);
                else response = elevator.TryGoDown(elevator);

                DisplayResponse(direction, response, elevator);
            }
        }


        private static List<Elevator> LoadElevatorsFromFile()
        {
            var result = new List<Elevator>();

            foreach (string row in File.ReadAllLines(@"..\..\..\Data\TextFile1.txt"))
            {
                string[] rowArray = row.Split(',');
                result.Add(
                new Elevator(
                    rowArray[0],
                    int.Parse(rowArray[2]),
                    int.Parse(rowArray[3]),
                    int.Parse(rowArray[1]),
                    int.Parse(rowArray[4])));
            }
            return result;

        }

        private static void DisplayStatus(List<Elevator> elevators)
        {
            //Headern
            TableHead("Name".PadRight(5) + "CurrenFloor ".PadRight(10) + "TopFLoor ".PadRight(10) + "LowestFloor ".PadRight(17)
                + "UntilMaintenance ".PadRight(21) + "Power ".PadRight(0));

            foreach (var e in elevators)
            {
                string isOn = e.IsPower ? "ON" : "OFF";
                string row = e.Name.ToString().PadRight(10) + e.CurrentFloor.ToString().PadRight(10) + e.TopFLoor.ToString().PadRight(10) +
                     e.LowestFloor.ToString().PadRight(15) +
                     e.UntilMaintenance.ToString().PadRight(20) +
                     isOn.PadRight(0);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(row);
                Console.BackgroundColor = ConsoleColor.Black;
            }

        }

        private static Direction AskForDirection()
        {
            while (true)
            {
                Console.Write("Enter U to go up and D to go down: ");
                string input = Console.ReadLine();

                if (input.ToUpper() == "D")
                {
                    return Direction.D;
                    
                }
                if (input.ToUpper() == "U")
                {
                    return Direction.U;
                }
                else
                {
                    Console.WriteLine("Enter a valid command!");
                }

            }

        }

        private static Elevator AskWhichElevatorToMove(List<Elevator> elevators)
        {
            while (true)
            {
                Console.Write("Enter wich elevator to move: ");
                string input = Console.ReadLine();
                var elevator = elevators.FirstOrDefault(e => e.Name == input);
                if (elevator != null)
                {
                    return elevator;

                }
            }
        }

        private static void DisplayResponse(Direction direction, ElevatorMoveResponse response, Elevator elevator)
        {
            switch (response)
            {
                case ElevatorMoveResponse.cantGoUp:
                    break;
                case ElevatorMoveResponse.cantGoDown:
                    break;
                case ElevatorMoveResponse.powerIsOff:
                    break;
                case ElevatorMoveResponse.succes:
                    string upDown = direction == Direction.U ? "Up" : "Down";
                    Console.WriteLine($"{elevator.Name} will move {upDown} to floor {elevator.CurrentFloor}");
                   
                    break;
                default:
                    break;
            }
            Console.Clear();
        }

        private static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Elevator app 2.0");
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void TableHead(string message)
        {

            Console.WriteLine($"{message}");
        }
    }
}
