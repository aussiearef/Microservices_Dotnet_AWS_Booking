using Amazon.DynamoDBv2.DataModel;

namespace Booking.Query.Models;

[DynamoDBTable("Booking")]
public class BookingDto
{
    // HotelName and CityName attributes are used to be displayed in the website only. Not part of the booking table.
    public string? HotelName { get; set; }
    public string? CityName { get; set; }
    public string? HotelId { get; set; }
    public string? CheckinDate { get; set; }
    public string? CheckoutDate { get; set; }

    [DynamoDBHashKey("UserId")] public string? UserId { get; set; }

    [DynamoDBRangeKey("Id")] public string? Id { get; set; }

    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public int Status { get; set; }
}