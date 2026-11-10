using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using LevelGrid = MsoPractical2.Grid;

namespace MsoPractical2.GUI
{
    internal class GridPainter
    {
        private readonly double cellSize;

        private static readonly IBrush FreeBrush = Brushes.White;
        private static readonly IBrush BlockedBrush = Brushes.DimGray;
        private static readonly IBrush FinishBrush = Brushes.LightGreen;
        private static readonly IBrush LineBrush = Brushes.Gray;

        public GridPainter(double _cellSize = 20) 
        {
            cellSize = _cellSize;
        }

        public void Paint(Canvas canvas, Level level) {

            canvas.Children.Clear();
            canvas.Width =  level.width * cellSize;
            canvas.Height = level.height * cellSize;

            for (int y = 0; y < level.height; y++) {
                for (int x = 0; x < level.width; x++) {
                    var cell = new Rectangle {
                        Width = cellSize,
                        Height = cellSize,
                        Fill = GetBrush(level.cells[x, y]),
                        Stroke = LineBrush,
                        StrokeThickness = 1
                    };

                    Canvas.SetLeft(cell, x * cellSize);
                    Canvas.SetTop(cell, y * cellSize);
                    canvas.Children.Add(cell);
                }
            }
        }

        private static IBrush GetBrush(CellType type) => type switch {
            CellType.Blocked => Brushes.DimGray,
            CellType.Start => Brushes.LightBlue,
            CellType.Finish => Brushes.LightGreen,
            _ => Brushes.White
        };

    }
}
