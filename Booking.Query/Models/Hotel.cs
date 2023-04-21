using Amazon.DynamoDBv2.DataModel;

namespace Booking.Query.Models;

[DynamoDBTable("Hotels_Order_Domain")]
public class Hotel
{
    public string? Id { get; set; }

    [DynamoDBHashKey("userId")] public string? UserId { get; set; }

    public string? Name { get; set; }
    public string? CityName { get; set; }
}