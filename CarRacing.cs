using SplashKitSDK;


public class CarRacing
{
    private Car _car;
    private Window _gameWindow;
    private List<ObstacleCar> _obstacles;
    private List<Fuel> _fuels;
    private SplashKitSDK.Timer _timeRemaining;
    private bool _gameOver;
    private Bitmap _background;
    private int _score;

    public CarRacing(Window gameWindow)
    {
        _gameWindow = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow), "Game window cannot be null");
        _background = new Bitmap("background", "bg1.png");
        _car = new Car(gameWindow);
        _obstacles = new List<ObstacleCar>();
        _fuels = new List<Fuel>();
        _timeRemaining = SplashKit.CreateTimer("game_timer");
        SplashKit.StartTimer(_timeRemaining);
        _gameOver = false;
        _score = 0;

        for (int i = 0; i < 1; i++)
        {
            _obstacles.Add(new ObstacleCar(gameWindow));
            _fuels.Add(new Fuel(gameWindow));
        }
    }

    public void HandleInput()
    {
        _car.HandleInput();
    }

    public void Update()
    {
        _car.StayOnWindow(_gameWindow);
        foreach (var obstacle in _obstacles)
        {
            obstacle.Update();
            if(obstacle.Speed < 15){
                obstacle.Speed += 1;
            }
            
            if (_car.CollidedWithCar(obstacle))
            {
                _gameOver = true;
                break; // Exit loop if game over
            }
        }

        foreach (var fuel in _fuels)
        {
            fuel.Update();
            if (_car.CollidedWithFuel(fuel))
            {
                _car.Refuel();
                _score += 10; // Increase score when fuel is collected
                _fuels.Remove(fuel); // Remove the collected fuel
                break; // Exit loop after refueling
            }
        }
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_background, 0, 0); // Draw background image
        _car.Draw();

        foreach (var obstacle in _obstacles)
        {
            obstacle.Draw();
        }

        foreach (var fuel in _fuels)
        {
            fuel.Draw();
        }

        DrawStatus(); // Draw the score
        SplashKit.RefreshScreen(60);
    }

    public void DrawStatus()
    {
        SplashKit.DrawText("SCORE: " + _score, Color.Black, "Arial", 20, _gameWindow.Width - 100, 10);
        SplashKit.DrawText("Fuel: " + _car.Fuel, Color.Black, "Arial", 20, _gameWindow.Width - 100, 30);
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public bool Quit()
    {
        return _car.Quit();
    }
}
