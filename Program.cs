using Airport_Ticket_Booking_System;
using Airport_Ticket_Booking_System.Booking;

class Program
{
    static void Main()
    {
        var flights = FlightImporter.ImportFlights();
        var bookings = new List<Booking>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Airport Ticket Booking System!");
            Console.WriteLine("Are you a Manager or a Passenger?");
            Console.WriteLine("1. Manager");
            Console.WriteLine("2. Passenger");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    ShowManagerMenu(flights, bookings);
                    break;

                case "2":
                    ShowPassengerMenu(flights, bookings);
                    break;

                case "0":
                    Console.WriteLine("Thank you for using the system. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please choose a number between 0 and 2.");
                    break;
            }
        }
    }

    static void ShowManagerMenu(List<Flight> flights, List<Booking> bookings)
    {
        Console.Clear();
        Console.WriteLine("Manager Menu");
        Console.WriteLine("1. Import flights from CSV");
        Console.WriteLine("2. View all bookings");
        Console.WriteLine("3. Display model validation details");
        Console.WriteLine("0. Back to main menu");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
            case "1":
                break;

            case "2":
                break;

            case "3":
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 3.");
                break;
        }

        Console.WriteLine("\n\nPress any key to return to the managers menu...");
        Console.ReadKey();
        ShowManagerMenu(flights, bookings);
    }

    static void ShowPassengerMenu(List<Flight> flights, List<Booking> bookings)
    {
        Console.Clear();
        Console.WriteLine("Passenger Menu");
        Console.WriteLine("1. Search flights");
        Console.WriteLine("2. View my bookings");
        Console.WriteLine("3. Modify booking");
        Console.WriteLine("4. Cancel booking");
        Console.WriteLine("0. Back to main menu");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine()?.Trim() ?? "";
        int inputID;

        switch (choice)
        {
            case "1":
                bookings.SearchAndBook(flights);
                break;

            case "2":
                bookings.ViewBookings();
                break;

            case "3":
                Console.Write("\nEnter the ID for the booking you want to modify: ");
                if (int.TryParse(Console.ReadLine(), out inputID))
                {
                    bookings.ModifyBooking(flights, inputID);
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid booking ID.");
                }
                break;

            case "4":
                Console.Write("\nEnter the ID for the booking you want to cancel: ");
                if (int.TryParse(Console.ReadLine(), out inputID))
                {
                    bookings.CancelBooking(inputID);
                }
                else
                {
                    Console.WriteLine("Invalid ID. Please enter a valid booking ID.");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 5.");
                break;
        }

        Console.Write("\n\nPress any key to return to the passenger menu...");
        Console.ReadKey();
        ShowPassengerMenu(flights, bookings);
    }
}
