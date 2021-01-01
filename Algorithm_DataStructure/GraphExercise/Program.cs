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