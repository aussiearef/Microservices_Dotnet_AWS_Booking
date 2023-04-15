using System.IdentityModel.Tokens.Jwt;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Booking.Command.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.UseHttpsRedirection();


app.MapGet("/book", async (BookingRequest request) =>
{
    var token = request.IdToken;
    var idTokenDetails = new JwtSecurityToken(token);

    var userId = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? "";
    var givenName = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "given_name")?.Value ?? "";
    var familyName = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "family_name")?.Value ?? "";
    var email = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? "";
    var phoneNumber = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "phone_number")?.Value ?? "";

    var dto = new BookingDto
    {
        Id = Guid.NewGuid().ToString(),
        HotelId = request.HotelId,
        CheckinDate = request.CheckinDate,
        CheckoutDate = request.CheckoutDate,
        UserId = userId,
        GivenName = givenName,
        FamilyName = familyName,
        Email = email,
        PhoneNumber = phoneNumber
    };

    var dbClient = new AmazonDynamoDBClient();
    var dbContext = new DynamoDBContext(dbClient);
    await dbContext.SaveAsync(dto);
});

app.Run();