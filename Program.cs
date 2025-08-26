// Check if a name was provided as a command line argument
string greeting;
if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
{
    greeting = $"Hello, {args[0]}!";
}
else
{
    greeting = "Hello!";
}

// Print the greeting
Console.WriteLine(greeting);

// Print the current date and time
Console.WriteLine($"Current date and time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
