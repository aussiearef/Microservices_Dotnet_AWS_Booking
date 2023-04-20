using System.IdentityModel.Tokens.Jwt;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Booking.Query.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c=>
    c.AddDefaultPolicy(p=>p.
        AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();
app.UseCors();


app.MapGet("/query", async (string? idToken) =>
{
    var idTokenDetails = new JwtSecurityToken(idToken);
    var userId = idTokenDetails.Claims.First(x=>x.Type=="sub")?.Value ?? "";
    var groups = idTokenDetails.Claims.FirstOrDefault(x => x.Type == "cognito:groups")?.Value ?? "";

    var result = new List<BookingDto>();

    using var dbClient = new AmazonDynamoDBClient();
    using var dbContext = new DynamoDBContext(dbClient);
    
    if (string.IsNullOrEmpty(groups))  // ordinary user
    {
        result.AddRange( await dbContext.FromQueryAsync<BookingDto>(new QueryOperationConfig
        {
          Filter   = new QueryFilter("UserId", QueryOperator.Equal, userId),
          IndexName = "UserId-index"
        }).GetRemainingAsync());
    }
    else
    {
        if (groups.Contains("HotelManager")) // hotel admin
        {
            result.AddRange(
            await dbContext.QueryAsync<BookingDto>(1, new DynamoDBOperationConfig
            {
                IndexName = "Status-index"
            }).GetRemainingAsync());
        }
    }

    return result;
});

app.MapGet("/", () => true);

app.Run();

