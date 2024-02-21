using BusinessClockApi.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISystemTime, SystemTime>();
builder.Services.AddSingleton<IProvideBusinessClock, StandardBusinessClock>();

// Above line is 'interna' configuration
var app = builder.Build();
// After this line is middleware configuration


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/support-info", (
    [FromServices] IProvideBusinessClock clock
) =>
{
    if (clock.IsOpen())
    {
        return new SupportInfoResponse("Graham", "555-1212");
    }
    else
    {
        return new SupportInfoResponse("TechSupportPros", "800-STUF-BROKE");
    }
});

app.Run();

public record SupportInfoResponse(string Name, string Phone);

public interface IProvideBusinessClock
{
    bool IsOpen();
}

public partial class Program { }