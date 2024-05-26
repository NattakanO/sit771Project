using SplashKitSDK;

public class ObstacleCar
{
    private Bitmap _obstacleBitmap;
    private int _x, _y;//position of car
    private Window _gameWindow;
    public int X => _x;
    public int Y => _y;
    private int _speed = 1;
    private static Random rnd = new Random();

    public ObstacleCar(Window gameWindow)
    {
        _gameWindow = gameWindow;
        RandomColor();
        // Random rnd = new Random();
        _x = rnd.Next(200, gameWindow.Width - _obstacleBitmap.Width - 200);
        _y = -_obstacleBitmap.Height;
    }

    public int Speed {
        get{ return _speed;}
        set{ _speed = value;}
    }
    public void RandomColor(){
        //  Random rnd = new Random();
         
         int num = rnd.Next(1, 4);
         Console.WriteLine(num);
        switch (num)
        {
            case 1:
                _obstacleBitmap = new Bitmap("ObstacleCar", "o_car1.png");
                break;
            case 2:
                _obstacleBitmap = new Bitmap("ObstacleCar", "o_car2.png");
                break;
            case 3:
                _obstacleBitmap = new Bitmap("ObstacleCar", "o_car3.png");
                break;
            default:
                _obstacleBitmap = new Bitmap("ObstacleCar", "o_car3.png");
                break;
        }
    }
    public void Update()
    {
        _y += _speed;
        if (_y > _gameWindow.Height)
        {
            _y = -_obstacleBitmap.Height;
            // Random rnd = new Random();
            _x = rnd.Next(200, _gameWindow.Width - _obstacleBitmap.Width-200);
        }
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_obstacleBitmap, _x, _y);
    }

    public Bitmap Bitmap => _obstacleBitmap;
    
}

