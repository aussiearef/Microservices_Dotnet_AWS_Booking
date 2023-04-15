namespace Booking.Command.Models;

public class BookingRequest
{
    public string? IdToken { get; set; }
    public string? HotelId { get; set; }
    public string? CheckinDate { get; set; }
    public string? CheckoutDate { get; set; }
}