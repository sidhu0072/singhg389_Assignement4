using System;
using System.Collections.Generic;

namespace TicketingConsoleApp
{
    // Enum to represent seat labels
    enum SeatLabel { A, B, C, D }

    // Class representing a seat
    class Seat
    {
        public int Row { get; }
        public SeatLabel Label { get; }
        public bool IsBooked { get; set; }
        public Passenger Passenger { get; set; }

        public Seat(int row, SeatLabel label)
        {
            Row = row;
            Label = label;
            IsBooked = false;
            Passenger = null;
        }

        public override string ToString()
        {
            return $"{Row} {Label}";
        }
    }

    // Class representing a passenger
    class Passenger
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string SeatPreference { get; }
        public Seat Seat { get; set; }

        public Passenger(string firstName, string lastName, string seatPreference)
        {
            FirstName = firstName;
            LastName = lastName;
            SeatPreference = seatPreference;
            Seat = null;
        }
    }

    // Class representing the plane
    class Plane
    {
        private readonly List<List<Seat>> seats;

        public Plane()
        {
            seats = new List<List<Seat>>();
            for (int i = 0; i < 12; i++)
            {
                seats.Add(new List<Seat>());
                for (SeatLabel label = SeatLabel.A; label <= SeatLabel.D; label++)
                {
                    seats[i].Add(new Seat(i + 1, label));
                }
            }
        }

        public List<List<Seat>> Seats => seats;
    }

    // Main class to run the application
    class Program
    {
        static void Main(string[] args)
        {
            Plane plane = new Plane();
            List<Passenger> passengers = new List<Passenger>();

            while (true)
            {
                Console.WriteLine("\nPlease enter 1 to book a ticket.");
                Console.WriteLine("Please enter 2 to see seating chart.");
                Console.WriteLine("Please enter 3 exit the application.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookTicket(plane, passengers);
                        break;
                    case "2":
                        PrintSeatingChart(plane);
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void BookTicket(Plane plane, List<Passenger> passengers)
        {
            Console.WriteLine("Please enter the passenger's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter the passenger's last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick the first available seat:");
            string seatPreference = Console.ReadLine();

            Passenger passenger = new Passenger(firstName, lastName, seatPreference);

            foreach (var row in plane.Seats)
            {
                foreach (var seat in row)
                {
                    if (!seat.IsBooked && (seatPreference == "" || (seatPreference == "1" && (seat.Label == SeatLabel.A || seat.Label == SeatLabel.D)) || (seatPreference == "2" && (seat.Label == SeatLabel.B || seat.Label == SeatLabel.C))))
                    {
                        seat.IsBooked = true;
                        seat.Passenger = passenger;
                        passenger.Seat = seat;
                        passengers.Add(passenger);

                        Console.WriteLine($"The seat located in {seat} has been booked.");
                        return;
                    }
                }
            }

            Console.WriteLine("Sorry, all seats are booked.");
        }

        static void PrintSeatingChart(Plane plane)
        {
            foreach (var row in plane.Seats)
            {
                foreach (var seat in row)
                {
                    if (seat.IsBooked)
                    {
                        Console.Write($"{seat.Passenger.FirstName[0]}{seat.Passenger.LastName[0]} ");
                    }
                    else
                    {
                        Console.Write($"{seat.Label} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
