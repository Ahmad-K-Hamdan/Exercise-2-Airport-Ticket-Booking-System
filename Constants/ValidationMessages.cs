namespace Airport_Ticket_Booking_System.Constants;

public static class ValidationMessages
{
    public const string FlightNumberEmpty = "Flight number cannot be empty.";
    public const string FlightNumberLength = "Flight number must be between 3 and 10 characters.";

    public const string CountryEmpty = "Country cannot be empty.";
    public const string CountryTooShort = "Country name is too short.";
    public const string CountryLettersOnly = "Country name must contain only letters.";

    public const string AirportEmpty = "Airport cannot be empty.";
    public const string AirportTooShort = "Airport name is too short.";

    public const string DepartureNotSet = "Departure time is not set.";
    public const string DurationPositive = "Flight duration must be positive.";

    public const string CapacityPositive = "Flight capacity must be greater than zero.";
    public const string BookedSeatsNegative = "Booked seats cannot be negative.";
    public const string BookedSeatsExceed = "Booked seats cannot exceed flight capacity.";

    public const string MissingPrice = "Missing price for {0}";
    public const string NonPositivePrice = "Price for {0} cannot be non-positive.";
    public const string EconomyLowest = "Price for economy must be the lowest.";
    public const string FirstClassHighest = "Price for first class must be the highest.";
}
