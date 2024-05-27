using SplashKitSDK;
using System;

public class ObstacleCar
{
    private Bitmap _obstacleBitmap;
    private int _x, _y; // position of car
    private Window _gameWindow;
    public int X => _x;
    public int Y => _y;
    private int _speed = 5;
    private static Random rnd = new Random();

    public ObstacleCar(Window gameWindow)
    {
        _gameWindow = gameWindow;
        RandomColor();
        _x = rnd.Next(200, gameWindow.Width - _obstacleBitmap.Width - 200);
        _y = -_obstacleBitmap.Height;
    }

    public int Speed 
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public void RandomColor()
    {
        int num = rnd.Next(1, 4);
        string bitmapName = $"ObstacleCar_{num}";
        Console.WriteLine(num);
        switch (num)
        {
            case 1:
                _obstacleBitmap = new Bitmap(bitmapName, "o_car1.png");
                break;
            case 2:
                _obstacleBitmap = new Bitmap(bitmapName, "o_car2.png");
                break;
            case 3:
                _obstacleBitmap = new Bitmap(bitmapName, "o_car3.png");
                break;
            default:
                _obstacleBitmap = new Bitmap(bitmapName, "o_car3.png");
                break;
        }
    }

    public void Update()
    {
        _y += _speed;
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_obstacleBitmap, _x, _y);
    }

    public Bitmap Bitmap => _obstacleBitmap;
}
