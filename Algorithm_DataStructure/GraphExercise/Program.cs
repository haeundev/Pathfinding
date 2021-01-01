using System;
using System.Collections.Generic;

namespace GraphExercise
{
    class Graph
    {
        // 그래프 표시 방법 1.
        private int[,] adj = new int[6, 6]
        {
            {0, 1, 0, 1, 0, 0},
            {1, 0, 1, 1, 0, 0},
            {0, 1, 0, 0, 0, 0},
            {1, 1, 0, 0, 1, 0},
            {0, 0, 0, 1, 0, 1},
            {0, 0, 0, 0, 1, 0}
        };

        // 그래프 표시 방법 2.
        private List<int>[] adj2 = new List<int>[]
        {
            new List<int>() {1, 3},
            new List<int>() {0, 2, 3},
            new List<int>() {1},
            new List<int>() {0, 1, 4},
            new List<int>() {3, 5},
            new List<int>() {4},
        };
        
        // 다익스트라를 위한 그래프
        private int[,] adj3 = new int[6, 6]
        {
            {-1, 15, -1, 35, -1, -1},
            {15, -1, 05, 10, -1, -1},
            {-1, 05, -1, -1, -1, -1},
            {35, 10, -1, -1, 05, -1},
            {-1, -1, -1, 05, -1, 05},
            {-1, -1, -1, -1, 05, -1}
        };


        private bool[] _visited = new bool[6];
        public void DFS1(int now)
        {
            Console.WriteLine(now);
            
            //(1) now 를 방문하고,
            _visited[now] = true;

            for (int next = 0; next < adj.GetLength(0); next++)
            {
                //(2) now와 연결된 정점들을 하나씩 확인해서
                if (adj[now, next] == 0)
                    continue;
                if (_visited[next])
                    continue;

                //(3) 아직 방문하지 않은 상태라면 방문한다.
                DFS1(next);
            }
        }

        public void DFS2(int now)
        {
            Console.WriteLine(now);
            
            //(1) now 를 방문하고,
            _visited[now] = true;

            foreach (var next in adj2[now])
            {
                if (_visited[next])
                    continue;
                DFS2(next);
            }
        }

        public void SearchAll()
        {
            _visited = new bool[6]; // 다 false 로 다시 초기화

            for (int now = 0; now < 6; now++)
            {
                if (_visited[now] == false)   
                    DFS1(now);
            }
        }

        public void BFS(int start)
        {
            bool[] found = new bool[6];
            
            // 추가 정보. 필요하다면 이런 식으로 넘겨줌.
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0) // 인접하지 않았으면 skip.
                        continue;
                    if (found[next]) // 이미 발견한 애라면 skip.
                        continue;
                    q.Enqueue(next);
                    found[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
            }
        }

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;

            while (true)
            {
                // 가장 가까이 있는 후보 찾기
                
                // 가장 유력한 후보의 거리와 번호 저장
                int closest = Int32.MaxValue;
                int now = -1;
                
                for (int i = 0; i < 6; i++)
                {
                    // 이미 방문했다면 skip
                    if (visited[i])
                        continue;
                    // 아직 예약된 적이 없거나, 기존 후보보다 멀리 있으면 skip.
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;
                    // 여태껏 발견한 가장 좋은 후보
                    closest = distance[i];
                    now = i;
                }
                
                // 다음 후보가 하나도 없다 --> 종료
                if (now == -1)
                    break;
                
                // 제일 좋은 후보를 찾았으니 방문하기
                visited[now] = true;
                
                // 방문한 정점과 인접한 정점을 조사해서, 상황에 따라 발견한 최단거리 갱신
                for (int next = 0; next < 6; next++)
                {
                    // 연결되지 않은 정점 skip
                    if (adj3[now, next] == -1)
                        continue;
                    // 이미 방문한 정점 skip
                    if (visited[next] == true)
                        continue;
                    // 새로 조사된 정점의 최단거리 계산
                    int nextDist = distance[now] + adj3[now, next];
                }
            }

        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // 그래프 순회 방법 1. DFS
            Graph graph = new Graph();
            graph.BFS(0);
            
            
            // 그래프 순회 방법 1. BFS
        }
    }
}