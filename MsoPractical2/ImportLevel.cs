using Microsoft.VisualBasic;
using MsoPractical2.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsoPractical2
{
    public class LevelImporter (IDictionary<string, Level> savedLevels)
    {
        private static readonly Dictionary<char, CellType> CharMap = new() {
            ['.'] = CellType.Free,
            ['#'] = CellType.Blocked,
            ['S'] = CellType.Start,
            ['F'] = CellType.Finish,
        };
        public string ImportLevel(string filePath, bool readOnly = false) {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Level file was not found.", filePath);
            string name = Path.GetFileNameWithoutExtension(filePath);
            var level = new Level(name, (Parse(File.ReadAllLines(filePath))));
            // Check if savedLevels already contains a program with the same name, and if so, throw an exception
            if (savedLevels.ContainsKey(name)) {
                throw new InvalidOperationException($"A Level with named '{name}' already exists.");
            }
            savedLevels.Add(name, level);
            return name;
        }

        // Parser that turns a list of lines into a Cell Grid
        private static CellType[,] Parse(string[] lines) 
        {
            lines = lines.Where(l => l.Length > 0).ToArray();

            if (lines.Length == 0)
                throw new FormatException("Level file is empty.");

            int width = lines[0].Length;
            int height = lines.Length;
            var cells = new CellType[width, height];

            for (int y = 0; y < height; y++) {
                if (lines[y].Length != width)
                    throw new FormatException(
                        $"Line {y + 1} has length {lines[y].Length}, expected {width}.");

                for (int x = 0; x < width; x++) {
                    char c = lines[y][x];
                    if (!CharMap.TryGetValue(c, out var type))
                        throw new FormatException(
                            $"Unknown character '{c}' at line {y + 1}, column {x + 1}.");

                    cells[x, y] = type;
                }
            }
            return cells;
        }
    }
}
