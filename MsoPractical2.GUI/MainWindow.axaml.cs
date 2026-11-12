using Avalonia.Controls;
using Avalonia.Interactivity;
using MsoPractical2;
using System;
using System.Diagnostics;
using System.Linq;

namespace MsoPractical2.GUI;

public partial class MainWindow : Window
{
    private readonly GridPainter painter = new();
    private readonly Main app = new();

    public MainWindow() {
        InitializeComponent();
        var level = app.savedLevels.Values.FirstOrDefault();
        painter.Paint(GridCanvas, level);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        Debug.WriteLine("Click!");
        
    }
}