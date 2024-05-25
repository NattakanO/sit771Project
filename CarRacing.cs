using SplashKitSDK;
using System.Collections.Generic;

public class CarRacing
{
    private Car _car;
    private Window _gameWindow;
    private List<ObstacleCar> _obstacles;
    private List<Fuel> _fuels;
    private SplashKitSDK.Timer _timeRemaining;
    private bool _gameOver;

    public CarRacing(Window gameWindow)
    {
        _car = new Car(gameWindow);
        _gameWindow = gameWindow;
        _obstacles = new List<ObstacleCar>();
        _fuels = new List<Fuel>();
        _timeRemaining = SplashKit.CreateTimer("game_timer");
        SplashKit.StartTimer(_timeRemaining);
        _gameOver = false;

        for (int i = 0; i < 5; i++)
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
            if (_car.CollidedWithCar(obstacle))
            {
                _gameOver = true;
            }
        }

        foreach (var fuel in _fuels)
        {
            fuel.Update();
            if (_car.CollidedWithFuel(fuel))
            {
                _car.Refuel();
            }
        }
    }

    public void Draw()
    {
        SplashKit.ClearScreen(Color.White);
        _car.Draw();

        foreach (var obstacle in _obstacles)
        {
            obstacle.Draw();
        }

        foreach (var fuel in _fuels)
        {
            fuel.Draw();
        }

        SplashKit.RefreshScreen(60);
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
