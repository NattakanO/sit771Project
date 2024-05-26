using SplashKitSDK;

public class Fuel
{
    private Bitmap _fuelBitmap;
    private int _x, _y;
    private Window _gameWindow;

    public Fuel(Window gameWindow)
    {
        _gameWindow = gameWindow;
        _fuelBitmap = new Bitmap("Fuel", "fuel.png");
        Random rnd = new Random();
        _x = rnd.Next(200, gameWindow.Width - _fuelBitmap.Width - 200);
        _y = -_fuelBitmap.Height;
    }

    public void Update()
    {
        _y += 5;
        if (_y > _gameWindow.Height)
        {
            _y = -_fuelBitmap.Height;
            Random rnd = new Random();
            _x = rnd.Next(200, _gameWindow.Width - _fuelBitmap.Width - 200);
        }
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_fuelBitmap, _x, _y);
    }

    public Bitmap Bitmap => _fuelBitmap;
    public int X => _x;
    public int Y => _y;
}