using SplashKitSDK;

public class Checkpoint
{
    private Bitmap _checkpointBitmap;
    private int _x, _y;
    private Window _gameWindow;
    private bool _scored;

    public Bitmap Bitmap => _checkpointBitmap;
    public int X => _x;
    public int Y => _y;
    public bool Scored => _scored;

    public Checkpoint(Window gameWindow)
    {
        _gameWindow = gameWindow;
        _checkpointBitmap = new Bitmap("Checkpoint", "checkpoint.png");
        _x = 0;
        _y = -_checkpointBitmap.Height; 
        _scored = false;
    }

    public void Update()
    {
        _y += 3; 
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_checkpointBitmap, _x, _y);
    }

    public bool IsOffScreen()
    {
        return _y > _gameWindow.Height;
    }

    public void MarkAsScored()
    {
        _scored = true;
    }
}
