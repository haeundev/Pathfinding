using System;

namespace Algorithm
{
    public class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        Random _random = new Random();

        private Board _board;

        public void Initialize(int posX, int posY, int destX, int destY, Board board)
        {
            PosX = posX;
            PosY = posY;
            
            _board = board;
        }

        const int MOVE_TICK = 100;
        private int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;
                
                // 0.1초마다 실행할 로직
                int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0: // 상
                        if (_board.Tile[PosX, PosY - 1] == Board.TileType.Empty)
                            PosY = PosY - 1;
                        break;
                    case 1: // 하
                        if (_board.Tile[PosX, PosY + 1] == Board.TileType.Empty)
                            PosY = PosY + 1;
                        break;
                    case 2: // 좌
                        if (_board.Tile[PosX - 1, PosY] == Board.TileType.Empty)
                            PosX = PosX - 1;
                        break;
                    case 3: // 우
                        if (_board.Tile[PosX + 1, PosY] == Board.TileType.Empty)
                            PosX = PosX + 1; 
                        break;
                }
            }
        }
    }
}