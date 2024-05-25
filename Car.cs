using SplashKitSDK;

public class Car
{
    private Bitmap _carBitmap;
    private double _x, _y;
    private int _width, _height;
    private int _fuel;
    private bool _quit;
    private int _score;

    public Car(Window gameWindow)
    {
        _carBitmap = new Bitmap("car", "car.png");
        _x = gameWindow.Width / 2 - _carBitmap.Width / 2;
        _y = gameWindow.Height - _carBitmap.Height - 10;
        _width = _carBitmap.Width;
        _height = _carBitmap.Height;
        _fuel = 100;
        _quit = false;
        _score = 0;
    }

    public void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.LeftKey)) _x -= 5;
        if (SplashKit.KeyDown(KeyCode.RightKey)) _x += 5;
        if (SplashKit.KeyDown(KeyCode.UpKey)) _y -= 5;
        if (SplashKit.KeyDown(KeyCode.DownKey)) _y += 5;
        if (SplashKit.KeyDown(KeyCode.EscapeKey)) _quit = true;
    }

    public void StayOnWindow(Window gameWindow)
    {
        if (_x < 0) _x = 0;
        if (_x + _width > gameWindow.Width) _x = gameWindow.Width - _width;
        if (_y < 0) _y = 0;
        if (_y + _height > gameWindow.Height) _y = gameWindow.Height - _height;
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_carBitmap, _x, _y);
    }

    public bool CollidedWithCar(ObstacleCar obstacle)
    {
        return SplashKit.BitmapCollision(_carBitmap, _x, _y, obstacle.CarBitmap, obstacle.X, obstacle.Y);
    }

    public bool CollidedWithFuel(Fuel fuel)
    {
        return SplashKit.BitmapCollision(_carBitmap, _x, _y, fuel.FuelBitmap, fuel.X, fuel.Y);
    }

    public bool Quit() { return _quit; }
    public void Refuel() { _fuel += 10; }
}
