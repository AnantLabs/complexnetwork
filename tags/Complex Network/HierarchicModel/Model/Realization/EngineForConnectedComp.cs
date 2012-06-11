using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Model.HierarchicModel.Realization
{
    public class EngineForConnectedComp
    {
        private int N;          // Количество вершин в графе

        private int[][] edges;  // Граф, в котором ищем сильно связные компоненты
        private int[] edges_c;


        private int[][] edgesT; // Транспонированый граф
        private int[] edgesT_c;

        private int[] state;             // Ипользуется в поиске для того, чтобы отмечать посещенные вершины

        private int[] f;        // Список предварительной расстановки вершин
        private int last_f;
        private int c;  // Номер компоненты (увеличиваем его, когда находим новую)

        private Dictionary<int, ArrayList> conn_comp;

        public EngineForConnectedComp()
        {
        }



        private void dfs(int node)
        {
            state[node] = 1;

            for (int i = 0; i < edges_c[node]; i++) //  Самый обыкновенный поиск в глубину.
                if (state[edges[node][i]] == 0)   //  Проходим по всем непосещенным вершинам,
                    dfs(edges[node][i]);         //  заходя в каждую

            f[last_f++] = node;                  //  Предварительная расстановка вершин в списке.
        }

        private void dfsT(int node)
        {
            state[node] = 1;

            for (int i = 0; i < edgesT_c[node]; i++) //  Самый обыкновенный поиск в глубину в транспонированном графе.
                if (state[edgesT[node][i]] == 0)   //  Проходим по всем непосещенным вершинам,
                    dfsT(edgesT[node][i]);        //  заходя в каждую   
            //fill 
            if (conn_comp.ContainsKey(c))
                conn_comp[c].Add(node);
            else
            {
                conn_comp.Add(c, new ArrayList());
                conn_comp[c].Add(node);
            }
        }

        private void scc()
        {  // Strongly Connected Components - функция выделения сильно связных компонент графа
            int i;
            for (i = 0; i < N; i++) state[i] = 0;

            for (i = 0; i < N; i++)                  // 1-ый поиск в глубину
                if (state[i]++ == 0)               // ...
                    dfs(i);                       // Предварительная расстановка вершин.

            for (i = 0; i < N; i++) state[i] = 0;

            for (last_f--; last_f >= 0; last_f--)  // 2-ой поиск в глубину
                if (state[f[last_f]] == 0)
                {        // ...
                    dfsT(f[last_f]);              // Окончательное выделение сильно связных компонент
                    c++;                          // увеличиваем номер следующей компоненты
                }
        }

        public void findConnSGruph(int[][] graph, int countNodes)
        {
            N = countNodes;
            edges = graph;
            edges_c = new int[N];
            edgesT_c = new int[N];
            edgesT = new int[N][];
            for (int i = 0; i < N; i++)
                edgesT[i] = new int[N];
            state = new int[N];
            f = new int[N];
            last_f = 0;
            c = 0;
            conn_comp = new Dictionary<int, ArrayList>();

            for (int i = 0; i < N; i++)
            {
                edges_c[i] = edges[i].Length;
                for (int j = 0; j < edges_c[i]; j++)
                {
                    int to = edges[i][j];
                    edgesT[to][edgesT_c[to]++] = i; // Построение транспонированного графа
                }
            }
            scc(); // Найти и вывести сильно связные компоненты
        }
        public ArrayList getCountConnSGruph(Dictionary<int, ArrayList> graph, int countNodes)
        {
            int[][] grp = new int[countNodes][];
            for (int i = 0; i < countNodes; i++)
            {
                grp[i] = new int[graph[i].Count];
                for (int j = 0; j < grp[i].Length; j++)
                    grp[i][j] = Convert.ToInt32(graph[i][j]);
            }
            findConnSGruph(grp, countNodes);

            ArrayList arr = new ArrayList();
            for (int i = 0; i < conn_comp.Count; i++)
                if (conn_comp[i].Count > 1)
                    arr.Add(conn_comp[i].Count);
            return arr;
        }
    }
}
