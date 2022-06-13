using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace Aqara.API.TestConsole.Commands;

public class MainCommand : RootCommand
{
    public MainCommand()
    {
        Add(new Argument<string>("name", "Test name"));
        Add(new Option<string?>(new []{ "--about", "-a" }, "Test program"));
        Add(new Option<string?>(new []{ "--verbose", "-v" }, "vvv"));

        Handler = CommandHandler.Create(OnExecute);
    }

    private void OnExecute(string Name, string? Greeting, bool Verbose, IConsole Console)
    {
        
        Console.WriteLine($"{Greeting} Hello wold");
    }
}