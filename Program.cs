using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () =>
{
    int hits = GetHitCount();

    return $"Hello World! I have been seen {hits} times.";
});

await app.RunAsync();

static int GetHitCount()
{
    using var redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
    {
        EndPoints = { "redis:6379" }
    });

    var db = redis.GetDatabase();

    return (int)db.StringIncrement("hits");
}