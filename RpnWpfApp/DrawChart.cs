﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RpnWpfApp;

static class PointExtensions
{ 
    public static Point ToMathCoordinates(this Point point, Canvas canvas, double scale)
    {
        return new Point(
            (int)((point.X - canvas.ActualWidth / 2) / scale),
            (int)((canvas.ActualHeight / 2 - point.Y) / scale));
    }
    public static Point ToUiCoordinates(this Point point, Canvas canvas, double scale)
    {
        return new Point(
            (int)(point.X * scale + canvas.ActualWidth / 2),
            (int)(canvas.ActualHeight / 2 - point.Y * scale)
        );
    }
}

class DrawChart
{
    private readonly Canvas _canvas;
    private readonly Brush _defaultStroke = Brushes.Black;
    private readonly int _scaleLength = 5;

    private readonly Point _xAxisStart, _xAxisEnd, _yAxisStart, _yAxisEnd;

    private readonly double _xStart;
    private readonly double _xEnd;
    private readonly double _step;
    private readonly double _scale;

    public DrawChart(Canvas canvas, double xStart, double xEnd, double step, double scale)
    {
        _canvas = canvas;
        _xAxisStart = new Point((int)(_canvas.ActualWidth / 2), 0);
        _xAxisEnd = new Point((int)_canvas.ActualWidth / 2, (int)(_canvas.ActualHeight));

        _yAxisStart = new Point(0, (int)( _canvas.ActualHeight / 2));
        _yAxisEnd = new Point((int)_canvas.ActualWidth, (int)(_canvas.ActualHeight / 2));

        _xStart = xStart;
        _xEnd = xEnd;
        _step = step;
        _scale = scale;
    }

    public void DrawAxis()
    {
        DrawLine(_xAxisStart, _xAxisEnd, _defaultStroke);
        DrawLine(_yAxisStart, _yAxisEnd, _defaultStroke);
        DrawXAxisSegments();
        DrawYAxisSegments();
    }

    private void DrawLine(Point start, Point end, Brush stroke = null, double thickness = 1)
    {
        stroke ??= _defaultStroke;
        
        Line line = new Line()
        {
            Visibility = Visibility.Visible,
            StrokeThickness = thickness,
            Stroke = stroke,
            X1 = start.X,
            Y1 = start.Y,
            X2 = end.X,
            Y2 = end.Y
        };
        
        _canvas.Children.Add(line);
    }

    public void DrawGraph(List<Point> points)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawLine(points[i].ToUiCoordinates(_canvas, _scale), points[i+1].ToUiCoordinates(_canvas, _scale),  Brushes.Red);
            DrawPoint(points[i].ToUiCoordinates(_canvas, _scale), Brushes.DarkRed);
        }
        DrawPoint(points[^1].ToUiCoordinates(_canvas, _scale), Brushes.DarkRed);
    }

    private void DrawXAxisSegments()
    {
        for (double x = _xStart; x <= _xEnd; x += _step)
        {
            var point = new Point(x, 0).ToUiCoordinates(_canvas, _scale);
            DrawLine(new Point(point.X, _canvas.ActualHeight / 2 - _scaleLength / 2), 
                new Point(point.X, _canvas.ActualHeight / 2 + _scaleLength / 2), _defaultStroke);
        }
    }
    
    private void DrawYAxisSegments()
    {
        for (double y = _xStart; y <= _xEnd; y += _step)
        {
            var point = new Point(0, y).ToUiCoordinates(_canvas, _scale);
            DrawLine(new Point(_canvas.ActualWidth / 2 - _scaleLength / 2, point.Y),
                new Point(_canvas.ActualWidth / 2 + _scaleLength / 2, point.Y), _defaultStroke);
        }
    }
    
    private void DrawPoint(Point point, Brush stroke = null, double thickness = 1, double size = 4)
    {
        stroke ??= _defaultStroke;
        
        Ellipse ellipse = new Ellipse()
        {
            Visibility = Visibility.Visible,
            Stroke = stroke,
            StrokeThickness = thickness,
            Fill = stroke,
            Width = size,
            Height = size
        };

        Canvas.SetLeft(ellipse, point.X - size / 2);
        Canvas.SetTop(ellipse, point.Y - size / 2);

        _canvas.Children.Add(ellipse);
    }
}