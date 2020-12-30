using System;
using System.Collections.Generic;

namespace Maze
{
    public class Board
    {
        const char SQUARE = '■';

        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }
        private Player _player;
        
        public enum TileType
        {
            Empty, Wall
        }
        
        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0) return;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            
            Size = size;
            _player = player;
            Tile = new TileType[Size, Size];

            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {
            // 길을 막는 작업
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[x, y] = TileType.Wall;
                    else
                        Tile[x, y] = TileType.Empty;
                }
            }
            // 초록색 점에 대해 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // SideWinder Algorithm
            Random rand = new Random();
            for (int x = 0; x < Size; x++)
            {
                int count = 1;
                for (int y = 0; y < Size; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                 
                    // 벽에 대한 처리
                    if (x == Size - 2 && y == Size - 2)
                        continue;
                    
                    if (x == Size - 2)
                    {
                        Tile[x, y + 1] = TileType.Empty;
                        continue;
                    }
                    if (y == Size - 2)
                    {
                        Tile[x + 1, y] = TileType.Empty;
                        continue;
                    }
                    
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[x + 1, y] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        var indexX = x - randomIndex * 2;
                        if (indexX < 0) continue;
                        Tile[indexX, y + 1] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
        
        private void GenerateByBinaryTree()
        {
            // 길을 막는 작업
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[x, y] = TileType.Wall;
                    else
                        Tile[x, y] = TileType.Empty;
                }
            }
            // 초록색 점에 대해 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            // Binary Tree Algorithm
            Random rand = new Random();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    // 벽에 대한 처리
                    if (x == Size - 2 && y == Size - 2)
                        continue;
                    
                    if (x == Size - 2)
                    {
                        Tile[x, y + 1] = TileType.Empty;
                        continue;
                    }
                    if (y == Size - 2)
                    {
                        Tile[x + 1, y] = TileType.Empty;
                        continue;
                    }
                    
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[x, y + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[x + 1, y] = TileType.Empty;
                    }
                }
            }
        }
        
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[x, y]);
                    
                    Console.Write(SQUARE);
                    Console.Write("  ");
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }

        private ConsoleColor GetTileColor(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.DarkRed;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
