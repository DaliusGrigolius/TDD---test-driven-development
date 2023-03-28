namespace Application
{
    public class CancelBookingDto
    {
        public Guid FlightId { get; set; }
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }

        public CancelBookingDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            FlightId = flightId;
            PassengerEmail = passengerEmail;
            NumberOfSeats = numberOfSeats;
        }


    }
}
