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

        public void Paint(Canvas canvas) {
            int tempGridWidth = 10;
            int tempGridHeight = 10;
            canvas.Children.Clear();
            canvas.Width =  tempGridWidth * cellSize;
            canvas.Height = tempGridHeight * cellSize;

            for (int y = 0; y < tempGridHeight; y++) {
                for (int x = 0; x < tempGridWidth; x++) {
                    var cell = new Rectangle {
                        Width = cellSize,
                        Height = cellSize,
                        Fill = FreeBrush,
                        Stroke = LineBrush,
                        StrokeThickness = 1
                    };

                    Canvas.SetLeft(cell, x * cellSize);
                    Canvas.SetTop(cell, y * cellSize);
                    canvas.Children.Add(cell);
                }
            }
        }
        /*
        private static IBrush GetBrush(LevelGrid grid, int x, int y) {

            if (level.grid(x, y) == Blocked) return BlockedBrush;
            if (level.grid(x, y) == End)  return FinishBrush;
            return FreeBrush;
        }
        */
    }
}
