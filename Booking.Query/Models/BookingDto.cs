using Amazon.DynamoDBv2.DataModel;

namespace Booking.Query.Models;

[DynamoDBTable("Booking")]
public class BookingDto
{
    public string? IdToken { get; set; }
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