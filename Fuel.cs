using SplashKitSDK;

public class Fuel
{
    private Bitmap _fuelBitmap;
    private double _x, _y;
    private int _width, _height;

    public Fuel(Window gameWindow)
    {
        _fuelBitmap = new Bitmap("fuel", "fuel.png");
        _x = SplashKit.Rnd(gameWindow.Width - _fuelBitmap.Width);
        _y = 0;
        _width = _fuelBitmap.Width;
        _height = _fuelBitmap.Height;
    }

    public void Update()
    {
        _y += 3; // Move down
        if (_y > SplashKit.ScreenHeight()) _y = -_height; // Reset to top
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_fuelBitmap, _x, _y);
    }

    public Bitmap FuelBitmap { get { return _fuelBitmap; } }
    public double X { get { return _x; } }
    public double Y { get { return _y; } }
}
