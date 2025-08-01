using Airport_Ticket_Booking_System;

class Program
{
    static void Main()
    {
        var prices1 = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 150.0m },
            { FlightClass.Business, 450.0m },
            { FlightClass.FirstClass, 900.0m }
        };

        var flight1 = new Flight(
            flightNumber: "TR150",
            departureCountry: "Jordan",
            departureAirport: "QAIA",
            departureDateTime: new DateTime(2025, 8, 1, 10, 30, 0),
            destinationCountry: "Turkey",
            destinationAirport: "IST",
            flightDuration: TimeSpan.FromHours(2.75),
            pricePerClass: prices1,
            capacity: 180,
            bookedSeats: 150
        );

        var prices2 = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 120.0m },
            { FlightClass.Business, 350.0m },
            { FlightClass.FirstClass, 750.0m }
        };

        var flight2 = new Flight(
            flightNumber: "PG109",
            departureCountry: "Turkey",
            departureAirport: "AYT",
            departureDateTime: new DateTime(2025, 8, 2, 15, 45, 0),
            destinationCountry: "Jordan",
            destinationAirport: "QAIA",
            flightDuration: TimeSpan.FromHours(25),
            pricePerClass: prices2,
            capacity: 220,
            bookedSeats: 100
        );

        System.Console.WriteLine(flight1);
        System.Console.WriteLine(flight2);
    }
}
