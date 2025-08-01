namespace Airport_Ticket_Booking_System;

public enum FlightClass
{
    Economy,
    Business,
    FirstClass
}

public class Flight
{
    public string FlightNumber { get; }
    public string DepartureCountry { get; }
    public string DepartureAirport { get; }
    public DateTime DepartureDateTime { get; }
    public string DestinationCountry { get; }
    public string DestinationAirport { get; }
    public TimeSpan FlightDuration { get; }
    public Dictionary<FlightClass, decimal> PricePerClass { get; }
    public int Capacity { get; }
    public int BookedSeats { get; }

    public DateTime ArrivalDateTime => DepartureDateTime + FlightDuration;
    public int AvailableSeats => Capacity - BookedSeats;

    public Flight(string flightNumber, string departureCountry, string departureAirport, DateTime departureDateTime, string destinationCountry, string destinationAirport, TimeSpan flightDuration, Dictionary<FlightClass, decimal> pricePerClass, int capacity, int bookedSeats)
    {
        FlightNumber = flightNumber;
        DepartureCountry = departureCountry;
        DepartureAirport = departureAirport;
        DepartureDateTime = departureDateTime;
        DestinationCountry = destinationCountry;
        DestinationAirport = destinationAirport;
        FlightDuration = flightDuration;
        Capacity = capacity;
        BookedSeats = bookedSeats;
        foreach (FlightClass flightClass in Enum.GetValues<FlightClass>())
        {
            if (!pricePerClass.ContainsKey(flightClass))
                throw new ArgumentException($"Missing price for {flightClass}");
            if (pricePerClass[flightClass] < 0)
                throw new ArgumentException($"Price for {flightClass} cannot be negative");
        }
        PricePerClass = pricePerClass;
    }

    public static void ImportFlights()
    {
        // TODO
    }

    public override string ToString()
    {
        return $"Flight {FlightNumber} | " +
                $"{DepartureAirport} ({DepartureCountry}) -> {DestinationAirport} ({DestinationCountry}) | " +
                $"Departs: {DepartureDateTime:HH:mm yyyy-MM-dd} | " +
                $"Arrives: {ArrivalDateTime:HH:mm yyyy-MM-dd} | " +
                $"Duration: {(int)FlightDuration.TotalHours}h {FlightDuration.Minutes}m | " +
                $"Seats: {AvailableSeats}/{Capacity} available | " +
                $"Prices: Economy ${PricePerClass[FlightClass.Economy]}, " +
                $"Business ${PricePerClass[FlightClass.Business]}, " +
                $"First ${PricePerClass[FlightClass.FirstClass]}";
    }
}
