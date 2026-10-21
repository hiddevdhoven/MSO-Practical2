namespace MsoPractical2;

public class Input
{
    // Prompt the user for a command and return the action and target as a tuple
    public (string Action, string Target) ReadCommand() {
        while (true) {
            Console.Write("Which command do you want to use?\nUse import (i), run (r), metrics (m), or exit (e).\n> ");
            string[] input = Console.ReadLine()!.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            Console.Write('\n');

            // Ensure a command was provided
            if (input.Length < 1) {
                Console.WriteLine("Please enter a command.");
                continue;
            }

            // Normalise action
            string action = input[0].ToLower();
            if (action == "import" || action == "i") {
                action = "import";
            } else if (action == "run" || action == "r") {
                action = "run";
            } else if (action == "metrics" || action == "m") {
                action = "metrics";
            } else if (action == "exit" || action == "e") {
                return ("exit", String.Empty);
            } else {
                Console.WriteLine("Invalid command. Please enter import (i), run (r), metrics (m), or exit (e).");
                continue;
            }

            // Ensure there is a second argument for all actions but exit
            if (input.Length < 2 && action != "exit") {
                if (action == "import") {
                    Console.WriteLine($"Please provide a path for the {action} command.");
                } else {
                    Console.WriteLine($"Please provide a Program name for the {action} command.");
                }
                continue;
            }

            // Paths and Program names are case sensitive so return without parsing
            return (action, input[1]);
        }
    }
}
