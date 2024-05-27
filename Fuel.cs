using SplashKitSDK;

public abstract class Fuel
{
    private Bitmap _fuelBitmap;
    private int _x, _y;
    private Window _gameWindow;
    private static Random _rnd = new Random();

    public Bitmap Bitmap => _fuelBitmap;
    public int X => _x;
    public int Y => _y;

    public Fuel(Window gameWindow, string bitmapPath, string bitmapName)
    {
        _gameWindow = gameWindow;
        _fuelBitmap = new Bitmap(bitmapName, bitmapPath);
        _x = _rnd.Next(200, _gameWindow.Width - _fuelBitmap.Width - 200);
        _y = -_fuelBitmap.Height;
    }

    public void Update()
    {
        _y += 5; // Move the fuel down the screen
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_fuelBitmap, _x, _y);
    }

    public bool Isoffscreen(Window screen)
    {
        return _y > screen.Height;
    }

    public abstract int RefuelAmount();
}

public class FuelSmall : Fuel
{
    public FuelSmall(Window gameWindow) : base(gameWindow, "fuel_small.png", "FuelSmall")
    {
    }

    public override int RefuelAmount()
    {
        return 5;
    }
}

public class FuelLarge : Fuel
{
    public FuelLarge(Window gameWindow) : base(gameWindow, "fuel_large.png", "FuelLarge")
    {
    }

    public override int RefuelAmount()
    {
        return 10;
    }
}
