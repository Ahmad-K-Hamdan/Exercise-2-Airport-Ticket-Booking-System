using Airport_Ticket_Booking_System;

class Program
{
    static void Main()
    {
        System.Console.WriteLine();
        var AllFlights = FlightImporter.ImportFlights();
        System.Console.WriteLine();
        System.Console.WriteLine();
        System.Console.WriteLine();
        AllFlights.PrintAllFlights();
    }
}
