using System;
using System.Collections.Generic;
using System.Text;

namespace MsoPractical2
{
    public class Level
    {
        public string name;
        public CellType[,] cells;

        public Level(string _name, CellType[,] cells)
        {
            this.name = _name;
            this.cells = cells;
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
