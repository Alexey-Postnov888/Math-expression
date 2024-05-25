using System.Windows;
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
    private double _axisThickness = 1;
    private readonly Brush _defaultStroke = Brushes.Black;
    private int _scaleLenth = 5;

    private readonly Point _xAxisStart, _xAxisEnd, _yAxisStart, _yAxisEnd;

    private double _xStart;
    private double _xEnd;
    private double _step;
    private double _scale;

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
        }
    }
}