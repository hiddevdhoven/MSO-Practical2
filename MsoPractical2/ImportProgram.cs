using MsoPractical2.Commands;
using System.Text.RegularExpressions;

namespace MsoPractical2;

// Converts program text files into Program objects
public partial class ProgramImporter(IDictionary<string, Program> savedPrograms)
{
    // Use GeneratedRegexAttribute to create regex patterns for command parsing
    [GeneratedRegex(@"^Move (\d+)$")]
    private static partial Regex MovePattern();
    [GeneratedRegex(@"^Turn (left|right)$")]
    private static partial Regex TurnPattern();
    [GeneratedRegex(@"^Repeat (\d+) times$")]
    private static partial Regex RepeatPattern();

    // Imports a text file, and stores it under its file name in savedPrograms (same Dictionary as in Main)
    public string ImportProgram(string filePath, bool readOnly = false) {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Program file was not found.", filePath);
        string name = Path.GetFileNameWithoutExtension(filePath);
        var program = new Program(readOnly, Parse(File.ReadAllLines(filePath), name));
        // Check if savedPrograms already contains a program with the same name, and if so, throw an exception
        if (savedPrograms.ContainsKey(name)) {
            throw new InvalidOperationException($"A program with named '{name}' already exists.");
        }
        savedPrograms.Add(name, program);
        return name;
    }

    // Parser that turns a list of lines into a list of Command objects
    private static List<Command> Parse(string[] lines, string name) {
        var cmdList = new List<Command>();
        // Use a stack to manage the current indentation level and the latest Repeat command
        var stack = new Stack<(int Indent, Repeat Repeat)>();

        for (int l = 0; l < lines.Length; l++) {
            string rawLine = lines[l];
            string errorLoc = name + ':' + (l + 1);
            if (string.IsNullOrWhiteSpace(rawLine)) continue;

            int indent = GetIndent(rawLine, errorLoc);
            var command = ParseCommand(rawLine.Trim(), errorLoc);

            // If indent is less than or equal, pop until empty or we find a parent Repeat command (with indent -1)
            while (stack.Count > 0 && indent <= stack.Peek().Indent)
                stack.Pop();

            // On the base level, always add the command
            if (stack.Count == 0)
                cmdList.Add(command);
            // If not on the base level, there should be a parent Repeat commend with indent -1, and we add the command to its instructions
            else if (indent == stack.Peek().Indent + 1)
                // This same Repeat command was already added to cmdList or to its parent Repeat command
                stack.Peek().Repeat.Instructions.Add(command);
            else
                throw new FormatException($"Unexpected indentation! {errorLoc}");

            // If the command is a Repeat command, push it onto the stack
            // It has already been added to cmdList or to its parent Repeat command
            if (command is Repeat repeat)
                stack.Push((indent, repeat));
        }

        return cmdList;
    }

    // Calculates the indentation level of one command line
    private static int GetIndent(string line, string errorLoc) {
        int spaces = 0;
        while (spaces < line.Length && line[spaces] == ' ')
            spaces++;
        int tabs = line.TakeWhile(character => character == '\t').Count();
        if (tabs > 0 && spaces > 0 || spaces % 4 != 0)
            throw new FormatException($"Indentation must use tabs or groups of four spaces! {errorLoc}");
        return tabs > 0 ? tabs : spaces / 4;
    }

    // Converts one text line into a command.
    private static Command ParseCommand(string line, string errorLoc) {
        var move = MovePattern().Match(line);
        if (move.Success)
            return new Move(int.Parse(move.Groups[1].Value));

        var turn = TurnPattern().Match(line);
        if (turn.Success)
            return new Turn(Enum.Parse<TurnDirection>(turn.Groups[1].Value, true));

        var repeat = RepeatPattern().Match(line);
        if (repeat.Success)
            return new Repeat(int.Parse(repeat.Groups[1].Value));

        throw new FormatException($"Invalid command '{line}'! {errorLoc}");
    }
}
