using System.CommandLine;

namespace Aqara.API.TestConsole.Commands;

public class MainCommand : RootCommand
{
    public MainCommand()
    {
        Add(new Argument<string>("name")
        {
            Description = "Test name"
        });

        Add(new Option<string?>("--about")
        {
            Description = "Test program",
            Aliases = { "-a" }
        });

        Add(new Option<bool>("--verbose")
        {
            Description = "vvv",
            Aliases = { "-v" }
        });
    }
}