using IssueTrackerApi;
using IssueTrackerApi.Services;
using Marten;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var connectionString = builder.Configuration.GetConnectionString("issues")
    ?? throw new Exception("No connection string for Issues!");

var apiUrl = builder.Configuration.GetValue<string>("api")
    ?? throw new Exception("No API url found!");

builder.Services.AddHttpClient<BusinessClockHttpService>(client =>
{
    client.BaseAddress = new Uri(apiUrl);
})
    .AddPolicyHandler(BasicSrePolicies.GetDefaultRetryPolicy())
    .AddPolicyHandler(BasicSrePolicies.GetDefaultCircuitBreaker());

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
