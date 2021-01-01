using System.Collections.Generic;

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

        private enum Dir
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

            BFS();
        }

        public void BFS()
        {
            int[] deltaX = new int[] { 0, -1, 0, 1};
            int[] deltaY = new int[] { -1, 0, 1, 0};
            
            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosX, PosY));
            found[PosX, PosY] = true;
            parent[PosX, PosY] = new Pos(PosX, PosY); // 시작점은 그냥 자기 자신이 parent.

            while (q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowX = pos.X;
                int nowY = pos.Y;

                for (int i = 0; i < 4; i++)
                {
                    int nextX = nowX + deltaX[i];
                    int nextY = nowY + deltaY[i];
                    
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    if (_board.Tile[nextX, nextY] == Board.TileType.Wall)
                        continue;
                    if (found[nextX, nextY])
                        continue;
                    
                    q.Enqueue(new Pos(nextX, nextY));
                    found[nextX, nextY] = true;
                    parent[nextX, nextY] = new Pos(nowX, nowY);
                }
            }

            int x = _board.DestX;
            int y = _board.DestY;

            while (parent[x, y].X != x || parent[x, y].Y != y)
            {
                _points.Add(new Pos(x, y));
                Pos pos = parent[x, y];
                x = pos.X;
                y = pos.Y;
            }
            _points.Add(new Pos(x, y)); // 시작점만 예외적으로 챙겨줌.
            _points.Reverse();
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

        public void RightHand()
        {
            // 현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타냄
            var frontX = new int[] {0, -1, 0, 1};
            var frontY = new int[] {-1, 0, 1, 0};
            var rightX = new int[] {1, 0, -1, 0};
            var rightY = new int[] {0, -1, 0, 1};

            _points.Add(new Pos(PosX, PosY));

            // 목적지에 도착하기 전까지 루프 돌면서 경로 게산
            while (PosX != _board.DestX || PosY != _board.DestY)
            {
                // 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는가
                if (_board.Tile[PosX + rightX[_dir], PosY + rightY[_dir]] == Board.TileType.Empty)
                {
                    // 오른쪽으로 90도 회전하고 앞으로 한 보 전진
                    _dir = (_dir + 3) % 4;
                    PosX += frontX[_dir];
                    PosY += frontY[_dir];
                    _points.Add(new Pos(PosX, PosY));
                }
                // 2. 현재 바라보는 방향을 기준으로 전진할 수 있는가
                else if (_board.Tile[PosX + frontX[_dir], PosY + frontY[_dir]] == Board.TileType.Empty)
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
    }
}