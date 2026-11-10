using System;
using System.Collections.Generic;
using System.Text;

namespace MsoPractical2
{
    public class Level
    {
        public string name;
        public Grid grid;
        
        public Level(string _name, Grid _grid) 
        {
            this.name = _name;
            this.grid = _grid;
        }
    }
}
