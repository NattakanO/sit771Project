using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Car Racing Game", 800, 600);
        CarRacing game = new CarRacing(gameWindow);

        while (!game.Quit() && !game.IsGameOver())
        {
            SplashKit.ProcessEvents();
            game.HandleInput();
            game.Update();
            game.Draw();
        }

        gameWindow.Close();
    }
}
