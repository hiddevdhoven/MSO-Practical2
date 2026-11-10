using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using MsoPractical2;

namespace MsoPractical2.GUI;

public partial class MainWindow : Window
{
    private readonly GridPainter painter = new();

    public MainWindow() {
        InitializeComponent();

        painter.Paint(GridCanvas);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        Debug.WriteLine("Click!");
        
    }
}