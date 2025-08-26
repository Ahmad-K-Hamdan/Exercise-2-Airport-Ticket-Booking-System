using Airport_Ticket_Booking_System.Bookings;
using Airport_Ticket_Booking_System.Flights;

class Program
{
    static void Main()
    {
        var flightService = new FlightService();
        var bookingService = new BookingService(flightService);

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
                    ShowManagerMenu(bookingService, flightService);
                    break;

                case "2":
                    ShowPassengerMenu(bookingService, flightService);
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

    static void ShowManagerMenu(BookingService bookingService, FlightService flightService)
    {
        Console.Clear();
        Console.WriteLine("Manager Menu");
        Console.WriteLine("1. View all flights");
        Console.WriteLine("2. View all bookings");
        Console.WriteLine("3. Display model validation details");
        Console.WriteLine("0. Back to main menu");
        Console.Write("Enter your choice: ");

        string choice = Console.ReadLine()?.Trim() ?? "";

        switch (choice)
        {
            case "1":
                flightService.PrintAllFlights();
                break;

            case "2":
                var bookingSearchService = new BookingSearchService();
                var allBookings = bookingService.GetAllBookings();
                bookingSearchService.SearchBookings(allBookings);
                break;

            case "3":
                FlightValidator.PrintModelValidationDetails<Flight>();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 3.");
                break;
        }

        Console.WriteLine("\n\nPress any key to return to the managers menu...");
        Console.ReadKey();
        ShowManagerMenu(bookingService, flightService);
    }

    static void ShowPassengerMenu(BookingService bookingService, FlightService flightService)
    {
        Console.Clear();
        Console.WriteLine("Passenger Menu");
        Console.WriteLine("1. Search flights & book");
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
                var flightSearchService = new FlightSearchService();
                var allFlights = flightService.GetAllFlights();
                var filteredFlights = flightSearchService.SearchFlights(allFlights);

                if (filteredFlights.Count > 0)
                {
                    bookingService.BookFlightFromSearch(filteredFlights);
                }
                break;

            case "2":
                Console.Write("\nEnter your Passenger ID: ");
                if (int.TryParse(Console.ReadLine(), out inputID))
                {
                    bookingService.ViewBookings(inputID);
                    break;
                }
                Console.WriteLine("Invalid Passenger ID.");
                break;

            case "3":
                Console.Write("\nEnter the ID for the booking you want to modify: ");
                if (!int.TryParse(Console.ReadLine(), out inputID))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid booking ID.");
                }
                bookingService.ModifyBooking(inputID);
                break;

            case "4":
                Console.Write("\nEnter the ID for the booking you want to cancel: ");
                if (!int.TryParse(Console.ReadLine(), out inputID))
                {
                    Console.WriteLine("Invalid ID. Please enter a valid booking ID.");
                }
                bookingService.CancelBooking(inputID);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option. Please choose a number between 0 and 4.");
                break;
        }

        Console.Write("\n\nPress any key to return to the passenger menu...");
        Console.ReadKey();
        ShowPassengerMenu(bookingService, flightService);
    }
}
