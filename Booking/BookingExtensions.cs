using Airport_Ticket_Booking_System.Enums;
using Airport_Ticket_Booking_System.Flights;

namespace Airport_Ticket_Booking_System.Bookings;

public static class BookingExtensions
{
    public static decimal CalculatePricePaid(this Booking booking)
    {
        if (booking.Flight.PricePerClass.TryGetValue(booking.Class, out var pricePerSeat))
        {
            return pricePerSeat * booking.NumberOfSeats;
        }
        throw new ArgumentException($"Price not found for class {booking.Class}");
    }

    public static void BookingsFiltering(this List<Booking> bookings)
    {
        var searchService = new BookingSearchService();

        while (true)
        {
            searchService.ShowCriteriaMenu();
            if (searchService.HandleCriteriaMenuInput())
            {
                var filteredBookings = searchService.FilterBookings(bookings);

                if (filteredBookings.Count == 0)
                {
                    Console.WriteLine("\nNo bookings matched the selected criteria.");
                }
                else
                {
                    Console.WriteLine("\nCurrent Bookings:");
                    for (int i = 0; i < filteredBookings.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {filteredBookings[i].ToDetailedString()}");
                    }
                }
                Console.Write("\nFilter again? (y/n): ");
                if (Console.ReadLine()?.ToLower() != "y")
                {
                    break;
                }
            }
        }
    }

    public static void SearchAndBook(this List<Booking> bookings, List<Flight> flights)
    {
        var searchService = new FlightSearchService();

        while (true)
        {
            searchService.ShowCriteriaMenu();
            if (searchService.HandleCriteriaMenuInput())
            {
                var filteredFlights = searchService.FilterFlights(flights);

                if (filteredFlights.Count == 0)
                {
                    Console.Write("\nNo matching flights. Retry filtering? (y/n): ");
                    if (Console.ReadLine()?.ToLower() != "y")
                    {
                        break;
                    }
                    continue;
                }

                Console.WriteLine("\nAvailable Flights:");
                for (int i = 0; i < filteredFlights.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {filteredFlights[i].ToDetailedString()}");
                }

                Console.Write("\nDo you want to book a flight from the filtered results? (y/n): ");
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    bookings.BookFlightPrompt(filteredFlights);
                    break;
                }
                else
                {
                    Console.Write("Return to filtering menu? (y/n): ");
                    if (Console.ReadLine()?.ToLower() != "y")
                    {
                        break;
                    }
                }
            }
        }
    }

    private static void BookFlightPrompt(this List<Booking> bookings, List<Flight> flights)
    {
        Console.Write("Select a flight: ");
        if (
            !int.TryParse(Console.ReadLine(), out int flightIndex)
            || flightIndex < 1
            || flightIndex > flights.Count
        )
        {
            Console.WriteLine(
                $"Invalid selection. Please select a number between 1 and {flights.Count}"
            );
            return;
        }

        var selectedFlight = flights[flightIndex - 1];

        Console.Write("Select flight class (Economy, Business, FirstClass): ");
        if (!Enum.TryParse(Console.ReadLine(), true, out FlightClass flightClass))
        {
            Console.WriteLine("Invalid class. Try Economy, Business, or FirstClass.");
            return;
        }

        Console.Write("Number of seats: ");
        if (!int.TryParse(Console.ReadLine(), out int seatCount) || seatCount < 1)
        {
            Console.WriteLine("Invalid seat count.");
            return;
        }

        if (seatCount > selectedFlight.AvailableSeats())
        {
            Console.WriteLine("Not enough seats available.");
            return;
        }

        bookings.BookFlight(selectedFlight, flightClass, seatCount);
    }

    private static void BookFlight(
        this List<Booking> bookings,
        Flight flight,
        FlightClass flightClass,
        int seatCount
    )
    {
        var booking = new Booking(flight, flightClass, seatCount);
        bookings.Add(booking);
        flight.ReserveSeats(seatCount);
        Console.WriteLine(
            $"\nSuccessfully booked for {seatCount} on flight {flight.FlightNumber}."
        );
    }

    public static void CancelBooking(this List<Booking> bookings, int bookingId)
    {
        var booking = FindBookingById(bookings, bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        bookings.Remove(booking!);
        booking.Flight.ReserveSeats(-booking.NumberOfSeats);
    }

    public static void ModifyBooking(
        this List<Booking> bookings,
        List<Flight> flights,
        int bookingId
    )
    {
        var booking = FindBookingById(bookings, bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        ChangeBookingInformation(booking, flights);
    }

    private static void ChangeBookingInformation(Booking booking, List<Flight> flights)
    {
        Console.WriteLine("\n--- Current Booking Details ---");
        Console.WriteLine(booking.ToDetailedString());

        Console.WriteLine("\n--- Modify Booking ---");

        Flight originalFlight = booking.Flight;
        int originalSeats = booking.NumberOfSeats;
        Flight newFlight = originalFlight;
        int newSeatCount = originalSeats;

        Console.Write("Change flight? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new flight number: ");
            string newFlightNumber = Console.ReadLine()!;

            var foundFlight = flights.FindFlightByNumber(newFlightNumber);
            if (foundFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            if (foundFlight.AvailableSeats() < originalSeats)
            {
                Console.WriteLine(
                    $"\nNot enough seats available on flight {newFlightNumber}. Available: {foundFlight.AvailableSeats()}, Needed: {originalSeats}"
                );
                return;
            }

            newFlight = foundFlight;
        }

        Console.Write("Change class? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new class (Economy, Business, FirstClass): ");
            if (Enum.TryParse<FlightClass>(Console.ReadLine(), true, out var newClass))
            {
                booking.Class = newClass;
            }
            else
            {
                Console.WriteLine("Invalid class. Try Economy, Business, or FirstClass.");
                return;
            }
        }

        Console.Write("Change number of seats? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            Console.Write("Enter new number of seats: ");
            if (int.TryParse(Console.ReadLine(), out int enteredSeats) && enteredSeats > 0)
            {
                if (newFlight.AvailableSeats() + originalSeats < enteredSeats)
                {
                    Console.WriteLine(
                        $"\nNot enough seats available on flight {newFlight.FlightNumber}. Available: {newFlight.AvailableSeats() + originalSeats}, Needed: {enteredSeats}"
                    );
                    return;
                }

                newSeatCount = enteredSeats;
            }
            else
            {
                Console.WriteLine("Invalid number.");
                return;
            }
        }

        originalFlight.BookedSeats -= originalSeats;
        newFlight.BookedSeats += newSeatCount;

        booking.Flight = newFlight;
        booking.NumberOfSeats = newSeatCount;
        booking.BookingDate = DateTime.Now;

        Console.WriteLine("\nBooking updated successfully.");
    }

    public static void ViewBookings(this List<Booking> bookings)
    {
        if (bookings.Count == 0)
        {
            Console.WriteLine("\nYou do not have any bookings.");
            return;
        }
        Console.Write("\nYour bookings:");
        foreach (var booking in bookings)
        {
            Console.WriteLine(booking.ToDetailedString());
        }
    }

    private static Booking? FindBookingById(this List<Booking> bookings, int bookingId)
    {
        return bookings.SingleOrDefault(booking => booking.BookingId == bookingId);
    }

    public static string ToDetailedString(this Booking booking)
    {
        return $"\n****************************************************\n"
            + $"Booking ID   : {booking.BookingId}\n"
            + $"Flight No    : {booking.Flight.FlightNumber}\n"
            + $"From         : {booking.Flight.DepartureAirport}, {booking.Flight.DepartureCountry}\n"
            + $"To           : {booking.Flight.DestinationAirport}, {booking.Flight.DestinationCountry}\n"
            + $"Departure    : {booking.Flight.DepartureDateTime}\n"
            + $"Class        : {booking.Class}\n"
            + $"Seats        : {booking.NumberOfSeats}\n"
            + $"Price Paid   : {booking.CalculatePricePaid():C}\n"
            + $"Booked At    : {booking.BookingDate}";
    }
}
