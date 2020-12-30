using System;
using System.Collections.Generic;
using Maze;

namespace Maze
{
    class Pos
    {
        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }



    public class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        private Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3,
        }

        private int _dir = (int) Dir.Up;
        private List<Pos> _points = new List<Pos>();

        public void Initialize(int posX, int posY, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;

            // 현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타냄
            var frontX = new int[] {0, -1, 0, 1};
            var frontY = new int[] {-1, 0, 1, 0};
            var rightX = new int[] {1, 0, -1, 0};
            var rightY = new int[] {0, -1, 0, 1};

            _points.Add(new Pos(PosX, PosY));

            // 목적지에 도착하기 전까지 루프 돌면서 경로 게산
            while (PosX != board.DestX || PosY != board.DestY)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는가
                if (board.Tile[PosX + rightX[_dir], PosY + rightY[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽으로 90도 회전하고 앞으로 한 보 전진
                    _dir = (_dir + 3) % 4;
                    PosX += frontX[_dir];
                    PosY += frontY[_dir];
                    _points.Add(new Pos(PosX, PosY));
                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는가
                else if (board.Tile[PosX + frontX[_dir], PosY + frontY[_dir]] == Board.TileType.Empty)
                {
                    // 앞으로 한 보 전진
                    PosX += frontX[_dir];
                    PosY += frontY[_dir];
                    _points.Add(new Pos(PosX, PosY));
                }
                else
                {
                    // 왼쪽 방향으로 90도 회전
                    _dir = (_dir + 5) % 4;
                }
            }
        }

        const int MOVE_TICK = 10;
        private int _sumTick = 0;
        private int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 0.1초마다 실행할 로직
                PosX = _points[_lastIndex].X;
                PosY = _points[_lastIndex].Y;
                _lastIndex++;
            }
        }
    }
}