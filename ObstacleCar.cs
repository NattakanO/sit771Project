using SplashKitSDK;

public class ObstacleCar
{
    private Bitmap _carBitmap;
    private double _x, _y;
    private int _width, _height;

    public ObstacleCar(Window gameWindow)
    {
        _carBitmap = new Bitmap("obstacle_car", "obstacle_car.png");
        _x = SplashKit.Rnd(gameWindow.Width - _carBitmap.Width);
        _y = 0;
        _width = _carBitmap.Width;
        _height = _carBitmap.Height;
    }

    public void Update()
    {
        _y += 5; 
        if (_y > SplashKit.ScreenHeight()) _y = -_height; 
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_carBitmap, _x, _y);
    }

    public Bitmap CarBitmap { get { return _carBitmap; } }
    public double X { get { return _x; } }
    public double Y { get { return _y; } }
}
