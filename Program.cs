using SplashKitSDK;
using System;

public class Program
{
    public static void Main()
    {
        string selectedCar = ReadUserOption();
        
        if (!string.IsNullOrEmpty(selectedCar))
        {
            Window gameWindow = new Window("Car Racing Game", 840, 650);
            CarRacing game = new CarRacing(gameWindow, selectedCar);

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

    public static string ReadUserOption()
    {
        int option;
        Console.WriteLine("----------------");
        Console.WriteLine("| 1 | Red    |");
        Console.WriteLine("| 2 | White  |");
        Console.WriteLine("| 3 | Black  |");
        Console.WriteLine("| 4 | Pink   |");
        Console.WriteLine("----------------");

        do
        {
            Console.Write("Choose your car's color [1-4]: ");
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please input a number between 1 and 4.");
                option = -1;
            }
        } while (option < 1 || option > 4);

        switch (option)
        {
            case 1:
                return "Red";
            case 2:
                return "White";
            case 3:
                return "Black";
            case 4:
                return "Pink";
            default:
                return "Red";
        }
    }
}