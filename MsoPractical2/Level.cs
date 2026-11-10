using System;
using System.Collections.Generic;
using System.Text;

namespace MsoPractical2
{
    public class Level
    {
        public string name;
        public CellType[,] cells;
        public int width;
        public int height;

        public Level(string _name, CellType[,] cells)
        {
            this.name = _name;
            this.cells = cells;
            this.width = cells.GetLength(0);
            this.height = cells.GetLength(1);
        }
    }
    public enum CellType
    {
        Start,
        Free,
        Blocked,
        Finish
    }
}
