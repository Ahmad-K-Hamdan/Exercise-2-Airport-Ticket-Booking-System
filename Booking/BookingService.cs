using Airport_Ticket_Booking_System.Enums;
using Airport_Ticket_Booking_System.Flights;

namespace Airport_Ticket_Booking_System.Bookings;

public class BookingService
{
    private readonly List<Booking> _bookings;
    private readonly FlightService _flightService;

    public BookingService(FlightService flightService)
    {
        _flightService = flightService;
        _bookings = BookingsRepository.GetBookings(_flightService.GetAllFlights());
    }

    public void BookFlightFromSearch(List<Flight> filteredFlights)
    {
        Console.Write("\nDo you want to book a flight from the filtered results? (y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
            return;

        Console.Write("Select a flight: ");
        if (!int.TryParse(Console.ReadLine(), out int flightIndex)
            || flightIndex < 1
            || flightIndex > filteredFlights.Count)
        {
            Console.WriteLine($"Invalid selection. Please select a number between 1 and {filteredFlights.Count}");
            return;
        }

        var selectedFlight = filteredFlights[flightIndex - 1];

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

        Console.Write("Your passenger ID: ");
        if (!int.TryParse(Console.ReadLine(), out int passengerId) || seatCount < 1)
        {
            Console.WriteLine("Invalid passenger ID.");
            return;
        }

        var booking = new Booking(selectedFlight, passengerId, flightClass, seatCount);
        _bookings.Add(booking);
        selectedFlight.ReserveSeats(seatCount);

        BookingsRepository.SaveBookings(_bookings);
        FlightsRepository.SaveFlights(_flightService.GetAllFlights());
        Console.WriteLine($"\nSuccessfully booked {seatCount} seats on flight {selectedFlight.FlightNumber}.");
    }

    public void CancelBooking(int bookingId)
    {
        var booking = FindBookingById(bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        _bookings.Remove(booking!);
        booking.Flight.ReserveSeats(-booking.NumberOfSeats);
        FlightsRepository.SaveFlights(_flightService.GetAllFlights());
        BookingsRepository.SaveBookings(_bookings);
        System.Console.WriteLine($"Successfully deleted booking {bookingId}");
    }

    public void ModifyBooking(int bookingId)
    {
        var booking = FindBookingById(bookingId);
        if (booking is null)
        {
            Console.WriteLine($"No booking with ID {bookingId}");
            return;
        }
        ChangeBookingInformation(booking);
        BookingsRepository.SaveBookings(_bookings);
        FlightsRepository.SaveFlights(_flightService.GetAllFlights());
    }

    private void ChangeBookingInformation(Booking booking)
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

            var foundFlight = _flightService.FindFlightByNumber(newFlightNumber);
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

    public List<Booking> GetAllBookings()
    {
        return _bookings.ToList();
    }

    public void ViewBookings(int passengerID = 0)
    {
        List<Booking> bookings = _bookings;

        if (passengerID != 0)
        {
            bookings = _bookings.Where(b => b.PassengerId.Equals(passengerID)).ToList();
        }

        if (bookings.Count == 0)
        {
            Console.WriteLine("\nYou do not have any bookings.");
            return;
        }

        foreach (var booking in bookings)
        {
            Console.WriteLine(booking.ToDetailedString());
        }
    }

    private Booking? FindBookingById(int bookingId)
    {
        return _bookings.SingleOrDefault(booking => booking.BookingId == bookingId);
    }
}
