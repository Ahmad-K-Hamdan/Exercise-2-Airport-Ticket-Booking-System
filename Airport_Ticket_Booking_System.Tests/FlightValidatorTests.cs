using Xunit;
using Airport_Ticket_Booking_System.Constants;
using Airport_Ticket_Booking_System.Flights;
using Airport_Ticket_Booking_System.Enums;

public class FlightValidatorTests
{
    [Theory]
    [Trait("Field Validating", "Capacity")]
    [InlineData(1)]
    [InlineData(100)]
    public void ValidateCapacity_ValidCapacity_ReturnsCapacity(int capacity)
    {
        var result = FlightValidator.ValidateCapacity(capacity);
        Assert.Equal(capacity, result);
    }

    [Theory]
    [Trait("Field Validating", "Capacity")]
    [InlineData(0)]
    [InlineData(-5)]
    public void ValidateCapacity_NonPositive_ThrowsArgumentException(int capacity)
    {
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidateCapacity(capacity));
        Assert.Equal(ValidationMessages.CapacityPositive, ex.Message);

    }

    [Theory]
    [Trait("Field Validating", "Booked Seats")]
    [InlineData(0, 10)]
    [InlineData(5, 10)]
    [InlineData(10, 10)]
    public void ValidateBookedSeats_ValidBookedSeats_ReturnsBookedSeats(int bookedSeats, int capacity)
    {
        var result = FlightValidator.ValidateBookedSeats(bookedSeats, capacity);
        Assert.Equal(bookedSeats, result);
    }

    [Theory]
    [Trait("Field Validating", "Booked Seats")]
    [InlineData(-1, 10)]
    [InlineData(-5, 10)]
    public void ValidateBookedSeats_Negative_ThrowsArgumentException(int bookedSeats, int capacity)
    {
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidateBookedSeats(bookedSeats, capacity));
        Assert.Equal(ValidationMessages.BookedSeatsNegative, ex.Message);
    }

    [Theory]
    [Trait("Field Validating", "Booked Seats")]
    [InlineData(11, 10)]
    [InlineData(100, 50)]
    public void ValidateBookedSeats_ExceedsCapacity_ThrowsArgumentException(int bookedSeats, int capacity)
    {
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidateBookedSeats(bookedSeats, capacity));
        Assert.Equal(ValidationMessages.BookedSeatsExceed, ex.Message);
    }

    [Fact]
    [Trait("Field Validating", "Price Per Class")]
    public void ValidatePricePerClass_ValidPrices_ReturnsDictionary()
    {
        var prices = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 100 },
            { FlightClass.Business, 200 },
            { FlightClass.FirstClass, 300 }
        };
        var result = FlightValidator.ValidatePricePerClass(prices);
        Assert.Equal(prices, result);
    }

    [Fact]
    [Trait("Field Validating", "Price Per Class")]
    public void ValidatePricePerClass_MissingClass_ThrowsArgumentException()
    {
        var prices = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 100 },
            { FlightClass.Business, 200 }
            // Missing FirstClass
        };
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidatePricePerClass(prices));
        Assert.Contains("Missing price", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Field Validating", "Price Per Class")]
    public void ValidatePricePerClass_NonPositivePrice_ThrowsArgumentException()
    {
        var prices = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 0 },
            { FlightClass.Business, 200 },
            { FlightClass.FirstClass, 300 }
        };
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidatePricePerClass(prices));
        Assert.Contains("cannot be non-positive", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Field Validating", "Price Per Class")]
    public void ValidatePricePerClass_EconomyNotLowest_ThrowsArgumentException()
    {
        var prices = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 200 },
            { FlightClass.Business, 150 },
            { FlightClass.FirstClass, 300 }
        };
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidatePricePerClass(prices));
        Assert.Equal(ValidationMessages.EconomyLowest, ex.Message);
    }

    [Fact]
    [Trait("Field Validating", "Price Per Class")]
    public void ValidatePricePerClass_FirstClassNotHighest_ThrowsArgumentException()
    {
        var prices = new Dictionary<FlightClass, decimal>
        {
            { FlightClass.Economy, 100 },
            { FlightClass.Business, 350 },
            { FlightClass.FirstClass, 300 }
        };
        var ex = Assert.Throws<ArgumentException>(() => FlightValidator.ValidatePricePerClass(prices));
        Assert.Equal(ValidationMessages.FirstClassHighest, ex.Message);
    }
}