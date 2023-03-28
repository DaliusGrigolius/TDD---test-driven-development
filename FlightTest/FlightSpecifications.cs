namespace FlightTest
{
    public class FlightSpecifications
    {
        [Theory]
        [InlineData(3,1,2)]
        [InlineData(6,3,3)]
        [InlineData(1,1,0)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
        {
            // Given
            var flight = new Flight(seatCapacity: seatCapacity);
            // When
            flight.Book("da@da.lt", numberOfSeats);
            // Then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Avoids_overbooking()
        {
            // Given
            var flight = new Flight(seatCapacity: 3);
            // When
            var error = flight.Book("da@da.lt", 4);
            // Then
            error.Should().BeOfType<OverbookingError>();
        }

        [Fact]
        public void Books_flights_successfully()
        {
            // Given
            var flight = new Flight(seatCapacity: 3);
            // When
            var error = flight.Book("da@da.lt", 1);
            // Then
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_bookings()
        {
            // Given
            var flight = new Flight(seatCapacity: 150);
            // When
            flight.Book(passengerEmail: "da@da.lt", numberOfSeats: 4);
            // Then
            flight.BookingList.Should().ContainEquivalentOf(new Booking("da@da.lt", 4));
        }

        [Theory]
        [InlineData(3,1,1,3)]
        [InlineData(4,2,2,4)]
        [InlineData(7,5,4,6)]
        public void Cancel_bookings_frees_up_the_seats(
            int initialCapacity,
            int numberOfSeatsToBook,
            int numberOfSeatsToCancel,
            int remainingNumberOfSeats)
        {
            // Given
            var flight = new Flight(initialCapacity);
            flight.Book(passengerEmail: "da@da.lt", numberOfSeats: numberOfSeatsToBook);
            // When
            flight.CancelBooking(passengerEmail: "da@da.lt", numberOfSeats: numberOfSeatsToCancel);
            // Then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
        {
            // Given
            var flight = new Flight(3);
            // When
            var error = flight.CancelBooking(passengerEmail: "da@da.lt", numberOfSeats: 2);
            // Then
            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Returns_null_when_successfully_cancels_a_booking()
        {
            // Given
            var flight = new Flight(3);
            flight.Book(passengerEmail: "da@da.lt", numberOfSeats: 1);
            // When
            var error = flight.CancelBooking(passengerEmail: "da@da.lt", numberOfSeats: 1);
            // Then
            error.Should().BeNull();
        }
    }
}