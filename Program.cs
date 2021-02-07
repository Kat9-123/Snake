// Snake | By: Kat9_123

using System;
using System.Threading;
using System.Collections.Generic;

namespace Snake
{
    class Program
    {
        const int VIEW_SIZE_X = 20;
        const int VIEW_SIZE_Y = 10;


        static int applePosX = 0;
        static int applePosY = 0;

        static int playerPosX = 0;
        static int playerPosY = 0;

        static int playerLength = 4;

        static Random random = new Random();

        static List<int[]> tail = new List<int[]>();

        static ConsoleKeyInfo input;

        static int direction = 1;
        static char[,] view = new char[VIEW_SIZE_Y,VIEW_SIZE_X];
        static int score = 0;


        static void Main()
        {
            Console.CursorVisible = false;
            Console.Title = "Snake | By: Kat9_123";
            Thread inputThread = new Thread(Input);
            inputThread.Start();
            
            GenerateNewApple(view);
            int count = 0;

            while (true)
            {       
                // INPUT
                GetInput();

                // UPDATE
                view = GenerateBackground();
                
                
                RenderSnake();
                RenderApple();
                
                if(count >= 15)
                {
                  
                    Move();
                    count = 0;
                }



                // DRAW
                Draw(view);


                count++;
                Thread.Sleep(10);
            }
        }


        static void RenderSnake()
        {
            foreach(int[] i in tail)
            {
                view[i[1],i[0]] = 'O';
            }
            view[playerPosY,playerPosX] ='0';
        }

        static void Collision()
        {
            if(view[playerPosY,playerPosX] == 'O') Environment.Exit(1);
            if(view[playerPosY,playerPosX] == '@') 
            {
                GenerateNewApple(view);
                playerLength++;
                score++;
            }
        }

        static void RenderApple()
        {
            view[applePosY,applePosX] = '@';
        }
        static void GetInput()
        {
            switch (input.Key)
            {
                
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    direction = 0;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    direction = 1;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    direction = 2;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    direction = 3;
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(1);
                    break;                 
            }
            input = new ConsoleKeyInfo();
        }

        static void Move()
        {
            switch (direction)
            {
                case 0:
                    playerPosY--;
                    if (playerPosY < 0) playerPosY = VIEW_SIZE_Y-1;
                    break;
                case 1:
                    playerPosY++;
                    if(playerPosY == VIEW_SIZE_Y) playerPosY = 0;
                    break;
                case 2:
                    playerPosX--;
                    if (playerPosX < 0) playerPosX = VIEW_SIZE_X-1;
                    break;
                case 3:
                    playerPosX++;
                    if(playerPosX == VIEW_SIZE_X) playerPosX = 0;
                    break;                        
                    
            }

            Collision();

            if(tail.Count == playerLength) tail.RemoveAt(0);
            int[] x = {playerPosX,playerPosY};
            tail.Add(x);
        }

        static char[,] GenerateBackground()
        {
            char[,] view = new char[VIEW_SIZE_Y,VIEW_SIZE_X];
            for (int y = 0; y < VIEW_SIZE_Y; y++)
            {
                for (int x = 0; x < VIEW_SIZE_X; x++)
                {
                    view[y,x] = '-';
                }
            }
            return view;
        }

        static void Input()
        {
            while (true)
            {
                input = Console.ReadKey(true);
            }
        }

        static void GenerateNewApple(char[,] view)
        {
            
           
            applePosX = random.Next(0,VIEW_SIZE_X);
            applePosY = random.Next(0,VIEW_SIZE_Y);

            while (view[applePosY,applePosX] == 'O' || view[applePosY,applePosX] == '0')
            {
                applePosX = random.Next(0,VIEW_SIZE_X);
                applePosY = random.Next(0,VIEW_SIZE_Y);
            }
            
        }
        static void Draw(char[,] view)
        {
            Console.WriteLine("\nScore: " + score + "\n");
            
            for (int y = 0; y < VIEW_SIZE_Y; y++)
            {
                string line = "";
                for (int x = 0; x < VIEW_SIZE_X; x++)
                {
                    line += view[y,x];
                }
                Console.WriteLine(line);
            }
            Console.SetCursorPosition(0,0);
        }
    }
}
