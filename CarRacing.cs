using SplashKitSDK;
public class CarRacing
{
    private Car _car;
    private Window _gameWindow;
    private Checkpoint _checkpoint;
    private List<ObstacleCar> _obstacles;
    private List<Fuel> _fuels;
    private SplashKitSDK.Timer _gameTimer;
    private SplashKitSDK.Timer _obstacleTimer;
    private SplashKitSDK.Timer _fuelTimer;
    private SplashKitSDK.Timer _checkpointTimer;
    private SplashKitSDK.Timer _carFuelTimer;
    private bool _gameOver;
    private string _gameOverText;
    private Bitmap _background;
    private int _score;
    private int _obstacleSpawnInterval;
    private int _fuelSpawnInterval;
    private Bitmap _gameoverBitmap;
    private int _remainingTime = 15;

    public CarRacing(Window gameWindow, string carType)
    {
        _gameWindow = gameWindow;
        _background = new Bitmap("background", "bg1.png");
        _car = new Car(gameWindow, carType);
        _checkpoint = null;
        _obstacles = new List<ObstacleCar>();
        _fuels = new List<Fuel>();
        _gameTimer = SplashKit.CreateTimer("game_timer");
        _obstacleTimer = SplashKit.CreateTimer("obstacle_timer");
        _fuelTimer = SplashKit.CreateTimer("fuel_timer");
        _checkpointTimer = SplashKit.CreateTimer("checkpoint_timer");
        _carFuelTimer = SplashKit.CreateTimer("carFuel_timer");
        SplashKit.StartTimer(_gameTimer);
        SplashKit.StartTimer(_obstacleTimer);
        SplashKit.StartTimer(_fuelTimer);
        SplashKit.StartTimer(_checkpointTimer);
        SplashKit.StartTimer(_carFuelTimer);
        _gameOver = false;
        _score = 0;
        _obstacleSpawnInterval = 1000; // Initial spawn interval
        _fuelSpawnInterval = 2500;
        _gameOverText = "";
    }

    public void HandleInput()
    {
        _car.HandleInput();
    }

    public void Update()
    {
        if (_gameOverText != "")
        {
            switch (_gameOverText)
            {
                case "Time out":
                    _gameoverBitmap = new Bitmap("gameover", "time_out.png");
                    break;
                case "Game over":
                    _gameoverBitmap = new Bitmap("gameover", "game_over.png");
                    break;
                default:
                    break;
            }

            SplashKit.CurrentWindow().DrawBitmap(_gameoverBitmap, 0, 0);
            _gameWindow.Refresh(60);
            SplashKit.Delay(2000);
            _gameOver = true;
        }
        else
        {
            _car.StayOnWindow(_gameWindow);
            if (SplashKit.TimerTicks(_obstacleTimer) > _obstacleSpawnInterval)
            {
                _obstacles.Add(new ObstacleCar(_gameWindow));
                SplashKit.ResetTimer(_obstacleTimer);
            }
            if (SplashKit.TimerTicks(_fuelTimer) > _fuelSpawnInterval)
            {
                _fuels.Add(RandomFuel());
                SplashKit.ResetTimer(_fuelTimer);
            }

            foreach (var obstacle in _obstacles)
            {
                obstacle.Update();
                if (obstacle.Speed < 30 && SplashKit.TimerTicks(_obstacleTimer) > _obstacleSpawnInterval)
                {
                    obstacle.Speed += 3;
                }

                if (_car.CollidedWithCar(obstacle) || _car.Fuel == 0)
                {
                    _gameOverText = "Game over";
                    break;
                }
            }
            _obstacles.RemoveAll(o => o.Y > _gameWindow.Height);

            foreach (var fuel in _fuels)
            {
                fuel.Update();
                if (_car.CollidedWithFuel(fuel))
                {
                    _car.Refuel(fuel.RefuelAmount());
                    _fuels.Remove(fuel);
                    break;
                }
            }
            _fuels.RemoveAll(f => f.Y > _gameWindow.Height);

            if (_checkpoint == null)
            {
                if (Convert.ToInt32(_gameTimer.Ticks / 1000) % 10 == 0 && Convert.ToInt32(_gameTimer.Ticks / 1000) > 5)
                {
                    _checkpoint = new Checkpoint(_gameWindow);
                }
            }
            else
            {
                _checkpoint.Update();
                if (_checkpoint.IsOffScreen())
                {
                    _checkpoint = null;
                }
                else if (_car.CollidedWithCheckpoint(_checkpoint) && !_checkpoint.Scored)
                {
                    _remainingTime += 10;
                    _score += 50;
                    _checkpoint.MarkAsScored();
                }
            }

            if (SplashKit.TimerTicks(_gameTimer) / 1000 > _remainingTime)
            {
                _gameOverText = "Time out";
            }
            if (_carFuelTimer.Ticks  /1000  >= 1) 
            {
                _car.Fuel = Math.Max(0, _car.Fuel - 2); 
                _carFuelTimer.Reset();

            }
        }
    }

    public Fuel RandomFuel()
    {
        float rnd = SplashKit.Rnd() * 100;
        return rnd <= 50 ? new FuelSmall(_gameWindow) : new FuelLarge(_gameWindow);
    }

    public void Draw()
    {
        SplashKit.DrawBitmap(_background, 0, 0); 
        _checkpoint?.Draw(); // Draw the checkpoint before the car
        _car.Draw();

        foreach (var obstacle in _obstacles)
        {
            obstacle.Draw();
        }

        foreach (var fuel in _fuels)
        {
            fuel.Draw();
        }

        DrawStatus();
        SplashKit.RefreshScreen(60);
    }

    public void DrawStatus()
    {
        int timeLeft = _remainingTime - (int)(SplashKit.TimerTicks(_gameTimer) / 1000);
        int fuelLeft = _car.Fuel - (int)(SplashKit.TimerTicks(_gameTimer) / 1000);
        _score = Convert.ToInt32(_gameTimer.Ticks / 1000);
        SplashKit.DrawText("SCORE: " + _score, Color.Black, "Arial", 20, _gameWindow.Width - 100, 10);
        SplashKit.DrawText("Fuel: " + _car.Fuel, Color.Black, "Arial", 20, _gameWindow.Width - 100, 30);
        SplashKit.DrawText("Time: " + timeLeft, Color.Black, "Arial", 20, _gameWindow.Width - 100, 50);
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
