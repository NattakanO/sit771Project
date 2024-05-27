using SplashKitSDK;
public class CarRacing
{
    private Car _car;
    private Window _gameWindow;
    private List<ObstacleCar> _obstacles;
    private List<Fuel> _fuels;
    private SplashKitSDK.Timer _timeRemaining;
    private SplashKitSDK.Timer _obstacleTimer;
    private SplashKitSDK.Timer _fuelTimer;
    private bool _gameOver;
    private String _gameOverText;
    private Bitmap _background;
    private int _score;
    private int _obstacleSpawnInterval;
    private int _fuelSpawnInterval;
    private Bitmap _gameoverBitmap;
    private const int InitialTime = 15;

    public CarRacing(Window gameWindow, string carType)
    {
        _gameWindow = gameWindow;
        _background = new Bitmap("background", "bg1.png");
        _car = new Car(gameWindow, carType);
        _obstacles = new List<ObstacleCar>();
        _fuels = new List<Fuel>();
        _timeRemaining = SplashKit.CreateTimer("game_timer");
        _obstacleTimer = SplashKit.CreateTimer("obstacle_timer");
        _fuelTimer = SplashKit.CreateTimer("fuel_timer");
        SplashKit.StartTimer(_timeRemaining);
        SplashKit.StartTimer(_obstacleTimer);
        SplashKit.StartTimer(_fuelTimer);
        _gameOver = false;
        _score = 0;
        _obstacleSpawnInterval = 2000; // Initial spawn interval
        _fuelSpawnInterval = 5000;
        _gameOverText = "";
    }

    public void HandleInput()
    {
        _car.HandleInput();
    }

    public void Update()
    {
        if(_gameOverText != ""){
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
        
        SplashKit.CurrentWindow().DrawBitmap(_gameoverBitmap,0,0);
        _gameWindow.Refresh(60);
        SplashKit.Delay(2000);
        _gameOver = true;
      }else{
         _car.StayOnWindow(_gameWindow);

        // Spawn a new obstacle car at regular intervals
        if (SplashKit.TimerTicks(_obstacleTimer) > _obstacleSpawnInterval )
        {
            _obstacles.Add(new ObstacleCar(_gameWindow));
            SplashKit.ResetTimer(_obstacleTimer);
        }
        if (SplashKit.TimerTicks(_fuelTimer) > _fuelSpawnInterval )
        {
            _fuels.Add(RandomFuel());
            SplashKit.ResetTimer(_fuelTimer);
        }

        foreach (var obstacle in _obstacles)
        {
            obstacle.Update();
            if (obstacle.Speed < 15 && SplashKit.TimerTicks(_obstacleTimer) > _obstacleSpawnInterval)
            {
                obstacle.Speed += 1;
                _obstacleSpawnInterval = 2000 + (obstacle.Speed * 500); 
            }

            if (_car.CollidedWithCar(obstacle))
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
                _score += 10;
                _fuels.Remove(fuel);
                break; 
            }
        }
        _fuels.RemoveAll(f => f.Y > _gameWindow.Height);

        if (SplashKit.TimerTicks(_timeRemaining) / 1000 > InitialTime)
        {
            _gameOverText = "Time out";
        }
      }
       
    }
    public Fuel RandomFuel(){
      Fuel fuelSmall = new FuelSmall(_gameWindow);
      Fuel fuelLarge = new FuelLarge(_gameWindow);
      float rnd = SplashKit.Rnd()*100;
      Console.WriteLine(rnd);
      if(rnd <= 50){
        return fuelSmall;
      }
      else {
        return fuelLarge;
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
        int timeLeft = InitialTime - (int)(SplashKit.TimerTicks(_timeRemaining) / 1000);
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
