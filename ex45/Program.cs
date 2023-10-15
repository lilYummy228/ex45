using System;
using System.Collections.Generic;

namespace ex45
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddDirection = "1";
            const string CommandShowAllDirections = "2";
            const string CommandExit = "3";

            RailwayStation railwayStation = new RailwayStation();
            TicketOffice ticketOffice = new TicketOffice();
            Train train = new Train();
            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("Конфигуратор пассажирских поездов");
                Console.Write($"\n{CommandAddDirection} - создать направление\n" +
                    $"{CommandShowAllDirections} - показать все направления\n" +
                    $"{CommandExit} - выйти из программы\n" +
                    $"\nВаш ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandAddDirection:
                        railwayStation.CreateDirection(ticketOffice, train);
                        break;

                    case CommandShowAllDirections:
                        railwayStation.ShowAllDirections(ticketOffice, train);
                        break;

                    case CommandExit:
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Неккоректный ввод...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Train
    {
        List<Wagon> _wagons = new List<Wagon>();

        public void Create(TicketOffice tickets)
        {
            const string CommandAddSmallWagon = "1";
            const string CommandAddMediumWagon = "2";
            const string CommandAddLargeWagon = "3";
            const string CommandSendTrain = "4";

            bool isPassengersPlanted = true;
            char smallWagonMark = 'S';
            char mediumWagonMark = 'M';
            char largeWagonMark = 'L';
            int smallWagonCapacity = 20;
            int mediumWagonCapacity = 50;
            int largeWagonCapacity = 100;
            int passengers = tickets.SellTickets();

            while (isPassengersPlanted)
            {
                if (passengers > 0)
                {
                    Console.WriteLine($"На это направление куплено {passengers} билетов");
                }
                else
                {
                    int freePlaces = passengers * -1;
                    Console.WriteLine($"В поезде есть {freePlaces} свободных мест");
                }

                Console.Write($"{CommandAddSmallWagon} - добавляет вагон вместимостью {smallWagonCapacity} мест\n" +
                    $"{CommandAddMediumWagon} - добавляет вагон вместимостью {mediumWagonCapacity} мест\n" +
                    $"{CommandAddLargeWagon} - добавляет вагон вместимостью {largeWagonCapacity} мест\n" +
                    $"{CommandSendTrain} - отправить поезд\n" +
                    $"\nВаш ввод: ");

                switch (Console.ReadLine())
                {
                    case CommandAddSmallWagon:
                        passengers -= AddWagon(smallWagonCapacity, smallWagonMark);
                        break;

                    case CommandAddMediumWagon:
                        passengers -= AddWagon(mediumWagonCapacity, mediumWagonMark);
                        break;

                    case CommandAddLargeWagon:
                        passengers -= AddWagon(largeWagonCapacity, largeWagonMark);
                        break;

                    case CommandSendTrain:
                        isPassengersPlanted = IsPassengersPlanted(passengers);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ShowTrain()
        {
            foreach (Wagon wagon in _wagons)
            {
                wagon.ShowWagons();
            }

            Console.WriteLine("[)");
        }

        private int AddWagon(int wagonCapacity, char wagonMark)
        {
            _wagons.Add(new Wagon(wagonMark, wagonCapacity));
            Console.WriteLine($"Вагон [{wagonMark}] добавлен...");
            return wagonCapacity;
        }

        private bool IsPassengersPlanted(int passengers)
        {
            if (passengers <= 0)
            {
                Console.WriteLine("Все пассажиры размещены, отправляем поезд...");
                return false;
            }
            else
            {
                Console.WriteLine("Недостаточно мест для всех пассажиров, купивших билеты. Добавьте еще вагонов...");
                return true;
            }
        }
    }

    class Wagon
    {
        private char _wagonMark;
        private int _wagonCapacity;

        public Wagon(char wagonMark, int wagonCapacity)
        {
            _wagonMark = wagonMark;
            _wagonCapacity = wagonCapacity;
        }

        public void ShowWagons()
        {
            Console.Write($"[{_wagonMark}] - ");
        }
    }

    class TicketOffice
    {
        List<Direction> _directions = new List<Direction>();

        public void CreateRoute()
        {
            Console.Write("\nВпишите точку отправления: ");
            string departure = Console.ReadLine();
            Console.Write("Впишите точку прибытия: ");
            string arrival = Console.ReadLine();
            _directions.Add(new Direction(departure, arrival));
        }

        public void ShowAllDirections()
        {
            foreach (Direction direction in _directions)
            {
                direction.ShowInfo();
            }
        }

        public int SellTickets()
        {
            Random random = new Random();
            int selledTickets = random.Next(100, 501);
            return selledTickets;
        }
    }

    class Direction
    {
        private string _pointOfDeparture;
        private string _pointOfArrival;

        public Direction(string pointOfDeparture, string pointOfArrival)
        {
            _pointOfDeparture = pointOfDeparture;
            _pointOfArrival = pointOfArrival;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_pointOfDeparture} - {_pointOfArrival}");
        }
    }

    class RailwayStation
    {
        List<Train> _trains = new List<Train>();

        public void CreateDirection(TicketOffice ticketOffice, Train train)
        {
            ticketOffice.CreateRoute();
            train.Create(ticketOffice);
        }

        public void AddTrains(Train train)
        {
            
        }

        public void ShowAllDirections(TicketOffice ticketOffice, Train train)
        {
            Console.Clear();
            ticketOffice.ShowAllDirections();
            Console.SetCursorPosition(30, 0);
            train.ShowTrain();
        }
    }
}
