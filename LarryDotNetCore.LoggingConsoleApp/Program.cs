// See https://aka.ms/new-console-template for more information
using Serilog;

Console.WriteLine("Hello, World!");

// Serilog Logging

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

Log.Information("Hello New World!");

int a = 10, b = 0;

try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error("Something went wrong!");
}
finally
{
    await Log.CloseAndFlushAsync();
}