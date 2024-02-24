using System;
using System.Threading;

class TreasureHuntGame
{
    static void PrintPosition(int posX, int posY)
    {
        Console.Write("You find yourself in the");
        if ((posX != 1) && (posY != 1))
        {
            if (posX == 0)      Console.Write(" left");
            else if (posX == 2) Console.Write(" right");
            if (posY == 0)      Console.Write(" upper");
            else if (posY == 2) Console.Write(" lower");
            Console.Write(" corner");
        }
        else if (posX == 1 && posY == 1)
            Console.Write(" middle");
        else
        {
            if (posX == 0)      Console.Write(" left");
            else if (posX == 2) Console.Write(" right");
            if (posY == 0)      Console.Write(" upper");
            else if (posY == 2) Console.Write(" lower");
            Console.Write(" edge");
        }
        Console.WriteLine(" of the map");
    }

    static void PrintPossibilitiesOfMoving(int posX, int posY)
    {
        Console.WriteLine("press one of these keys to change your position: ");
        if (posX > 0)
            Console.WriteLine("a - go left");
        if (posX < 2)
            Console.WriteLine("d - go right");
        if (posY > 0)
            Console.WriteLine("w - go up");
        if (posY < 2)
            Console.WriteLine("s - go down");
        if (posY == 2 && posX == 1)
            Console.WriteLine("s - go down (find exit)");
    }

    static void PrintInventory(int treasures)
    {
        Console.WriteLine("You've found a treasure with a total value of {0} gold.", treasures);
    }

    static void PrintCannotMove()
    {
        Console.WriteLine("This move is not allowed!");
        Thread.Sleep(1000);
    }

    static void Main()
    {
        int[,] treasureMap = new int[3, 3] { { 0, 0, 2 }, { 1, 0, 0 }, { 3, 0, 0 } };
        string playerName;
        int playerPosY, playerPosX;
        int playerInventory = 0;
        int playerMoves = 0;

        Console.WriteLine("Welcome to the Abyss!");
        Console.WriteLine("Tell us who you are");
        playerName = Console.ReadLine();
        Console.WriteLine("Nice to meet you {0}", playerName);
        Console.WriteLine("Your quest is to find treasure with a total value of 6 gold and escape the Abyss. You shall be spawned in a random part of the map, and will need to find your way to the treasure.");

        Random rand = new Random();
        playerPosX = rand.Next(3);
        playerPosY = rand.Next(3);
        Thread.Sleep(4000);

        while (true)
        {
            Console.Clear();
            if (treasureMap[playerPosX, playerPosY] != 0)
            {
                Console.WriteLine("You've found a treasure with value of {0} gold",
                                  treasureMap[playerPosX, playerPosY]);
                playerInventory += treasureMap[playerPosX, playerPosY];
                treasureMap[playerPosX, playerPosY] = 0;
            }

            PrintInventory(playerInventory);
            PrintPosition(playerPosX, playerPosY);
            PrintPossibilitiesOfMoving(playerPosX, playerPosY);

            ConsoleKeyInfo move = Console.ReadKey(true);
            if (move.Key == ConsoleKey.W)
            {
                if (playerPosY > 0)
                    playerPosY--;
                else
                    PrintCannotMove();
            }
            else if (move.Key == ConsoleKey.A)
            {
                if (playerPosX > 0)
                    playerPosX--;
                else
                    PrintCannotMove();
            }
            else if (move.Key == ConsoleKey.S)
            {
                if (playerPosY < 2)
                    playerPosY++;
                else if (playerPosX == 1)
                {
                    if (playerInventory == 6)
                        break;
                    else
                    {
                        Console.WriteLine("You cannot leave the Abyss without all of the Treasure!");
                        Thread.Sleep(1000);
                    }
                }
                else
                    PrintCannotMove();
            }
            else if (move.Key == ConsoleKey.D)
            {
                if (playerPosX < 2)
                    playerPosX++;
                else
                    PrintCannotMove();
            }
            playerMoves++;
        }

        Console.WriteLine("You've found all of the Abyss' Treasures and finished the game in this number of moves:{0}", playerMoves);
    }
}