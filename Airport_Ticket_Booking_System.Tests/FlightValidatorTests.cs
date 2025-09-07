using Xunit;
using Airport_Ticket_Booking_System.Constants;
using Airport_Ticket_Booking_System.Flights;

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
}