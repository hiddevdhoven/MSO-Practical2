namespace MsoPractical2;

public class Main
{
    // Stores all programs loaded by the application
    private readonly Dictionary<string, Program> savedPrograms = new();
    private readonly Output output;
    private readonly Input input;
    // Imports programs from .txt files
    private readonly ProgramImporter programImporter;

    // Create the application and load predefined programs
    public Main() {
        output = new ConsoleOutput();
        input = new Input();
        programImporter = new ProgramImporter(savedPrograms);
        CreatePredefinedPrograms();
    }

    // Logic loop, runs until the user exits the application
    public void Run() {
        while (true) {
            var sortedKeys = savedPrograms.Keys.ToArray();
            Array.Sort(sortedKeys);
            output.Display("Available programs: " + String.Join(", ", sortedKeys) + "\n");
            var (action, target) = input.ReadCommand();

            switch (action) {
                case "run":
                    RunProgram(target);
                    break;
                case "metrics":
                    ShowMetrics(target);
                    break;
                case "import":
                    TryImport(target);
                    break;
                case "exit":
                    output.Display("Exiting application.\n");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown action: {action}");
            }
            output.Display("\n");
        }
    }

    // Runs a program and shows its log
    private void RunProgram(string programName) {
        if (!savedPrograms.TryGetValue(programName, out var selectedProgram)) {
            output.Display($"Program {programName} not found.");
            return;
        }
        var execution = new ExecutionState(selectedProgram);
        output.Display($"Running {programName}...");
        execution.Execute();
        output.Display(String.Join(", ", execution.ActionLog) + ".");
        output.Display(execution.GetEndState());
    }

    // Shows metrics about a program
    private void ShowMetrics(string programName) {
        if (!savedPrograms.TryGetValue(programName, out var selectedProgram)) {
            output.Display($"Program {programName} not found.");
            return;
        }
        var metrics = new MetricsState(selectedProgram);
        output.Display($"Calculated metrics for {programName}.");
        output.Display($"Total commands:  {metrics.TotalCommands}");
        output.Display($"Maximum nesting: {metrics.MaxNesting}");
        output.Display($"Total repeats:   {metrics.TotalRepeats}");
    }

    // Attempt to import a program and catch errors
    private void TryImport(string filePath) {
        try {
            string name = programImporter.ImportProgram(filePath);
            output.Display($"Imported {name}.");
        } catch (FileNotFoundException ex) {
            output.Display($"Program path was invalid: {ex.Message}");
            output.Display($"Current directory: {AppContext.BaseDirectory}");
        } catch (Exception ex) {
            output.Display($"Failed to import program from {filePath}.");
            output.Display($"Error: {ex.Message}");
        }
    }

    // Imports all predefined programs in the PreDefinedPrograms folder
    private void CreatePredefinedPrograms() {
        string folder = Path.Combine(AppContext.BaseDirectory, "PreDefinedPrograms");
        foreach (string filePath in Directory.GetFiles(folder, "*.txt")) {
            try {
                programImporter.ImportProgram(filePath, true);
            } catch (FileNotFoundException ex) {
                output.Display($"Default Program file not found: {ex.Message}");
            } catch (Exception ex) {
                output.Display($"Error importing default Program file: {ex.Message}");
            }
        }
    }
}

// Entry point for the application because Main cannot contain a Main method
public static class Start
{
    public static void Main() => new Main().Run();
}
