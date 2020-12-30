using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board.Size - 2, board.Size - 2, board);

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            const int WAIT_TICK = 1000 / 30; /*milisecond이기 때문에 1000 곱하기*/
            int lastTick = 0;
            while (true)
            {
                #region 프레임 관리

                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                var deltaTick = currentTick - lastTick;
                lastTick = currentTick;

                #endregion

                Console.SetCursorPosition(0, 0);
                
                // 로직
                player.Update(deltaTick);
                
                // 렌더링
                board.Render();
            }
        }
    }
}
