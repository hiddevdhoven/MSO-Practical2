using Avalonia.Controls;
using Avalonia.Interactivity;
using MsoPractical2;
using System;
using System.Diagnostics;

namespace MsoPractical2.GUI;

public partial class MainWindow : Window
{
    private readonly GridPainter painter = new();

    public MainWindow() {
        InitializeComponent();
        painter.Paint(GridCanvas, level);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        Debug.WriteLine("Click!");
        
    }
}