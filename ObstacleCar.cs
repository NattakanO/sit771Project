using SplashKitSDK;

public class ObstacleCar
{
    private Bitmap _obstacleBitmap;
    private int _x, _y;//position of car
    private Window _gameWindow;
    public int X => _x;
    public int Y => _y;
    private int _speed = 1;

    public ObstacleCar(Window gameWindow)
    {
        _gameWindow = gameWindow;
        _obstacleBitmap = new Bitmap("ObstacleCar", "obstacle_car.png");
        Random rnd = new Random();
        _x = rnd.Next(200, gameWindow.Width - _obstacleBitmap.Width - 200);
        _y = -_obstacleBitmap.Height;
    }

    public int Speed {
        get{ return _speed;}
        set{ _speed = value;}
    }

    public void Update()
    {
        _y += _speed;
        if (_y > _gameWindow.Height)
        {
            _y = -_obstacleBitmap.Height;
            Random rnd = new Random();
            _x = rnd.Next(200, _gameWindow.Width - _obstacleBitmap.Width-200);
        }
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_obstacleBitmap, _x, _y);
    }

    public Bitmap Bitmap => _obstacleBitmap;
    
}

