namespace Airport_Ticket_Booking_System.Flights;

public class FlightService
{
    private readonly List<Flight> _flights;

    public FlightService()
    {
        _flights = FlightsRepository.GetFlights();
    }

    public Flight? FindFlightByNumber(string flightNumber)
    {
        return _flights.FirstOrDefault(flight => flight.FlightNumber == flightNumber);
    }

    public List<Flight> GetAllFlights()
    {
        return _flights.ToList();
    }

    public void PrintAllFlights()
    {
        if (_flights.Count == 0)
        {
            Console.WriteLine("\nThere are no flights available.");
            return;
        }

        Console.Write("\nAll available flights:\n");
        foreach (var flight in _flights)
        {
            Console.WriteLine(flight.ToDetailedString());
        }
    }
}
