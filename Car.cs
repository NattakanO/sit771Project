using SplashKitSDK;

public class Car
{
    private Bitmap _carBitmap;
    private Window _gameWindow;
    private int _x, _y;
    private int _fuel;

    public Car(Window gameWindow, String carType)
    {
        _gameWindow = gameWindow;
        switch (carType)
        {
            case "Black":
                _carBitmap = new Bitmap("BlackCar", "black_car.png");
                break;
            case "Pink":
                _carBitmap = new Bitmap("PinkCar", "pink_car.png");
                break;
            case "Red":
                _carBitmap = new Bitmap("RedCar", "red_car.png");
                break;
            case "White":
                _carBitmap = new Bitmap("WhiteCar", "white_car.png");
                break;
        }
        _x = (gameWindow.Width - _carBitmap.Width) / 2;
        _y = gameWindow.Height - _carBitmap.Height - 10;
        _fuel = 100;
    }

    public int Fuel{
        get {return _fuel;}
        set {_fuel = value;}
    }
    public void HandleInput()
    {
        if (SplashKit.KeyDown(KeyCode.LeftKey) && _x > 0)
        {
            _x -= 7;
        }
        if (SplashKit.KeyDown(KeyCode.RightKey) && _x < _gameWindow.Width - _carBitmap.Width)
        {
            _x += 7;
        }
        
    }

    public void StayOnWindow(Window gameWindow)
    {
        if (_x < 180) _x = 180;
        if (_x > gameWindow.Width - _carBitmap.Width - 180) _x = gameWindow.Width - _carBitmap.Width - 180;
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_carBitmap, _x, _y);
    }

    public bool CollidedWithCar(ObstacleCar obstacle)
    {
        return _carBitmap.BitmapCollision(_x, _y, obstacle.Bitmap, obstacle.X, obstacle.Y);
    }

    public bool CollidedWithFuel(Fuel fuel)
    {
        return _carBitmap.BitmapCollision(_x, _y, fuel.Bitmap, fuel.X, fuel.Y);
    }

    public void Refuel(int amount)
    {
        Fuel += amount;
    }

    public bool Quit()
    {
        return SplashKit.KeyDown(KeyCode.EscapeKey);
    }
}