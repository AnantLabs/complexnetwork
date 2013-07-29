using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using CommonLibrary.Model;
using GenericAlgorithms;
using log4net;

namespace Model.ERModel.Realization
{
    // Реализация контейнера (ER).
    public class ERContainer : AbstractGraphContainer
    {
        // Организация pаботы с лог файлом.
        protected static readonly ILog log = log4net.LogManager.GetLogger(typeof(ERContainer));

        // Число вершин графа.
        private int size = 0;
        // Списки соседей для вершин графа.
        private SortedDictionary<int, List<int>> neighbourship;
        private List<List<KeyValuePair<int, int>>> motifs4Order;
        // Список степеней вершин графа.
        private List<int> degrees;
        private static object syncLock = new object();
        public List<KeyValuePair<int, int>> Edjes = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> NoEdjes = new List<KeyValuePair<int, int>>();
        public SortedDictionary<int, List<int>> Motifs4Order;
        public List<KeyValuePair<int, int>> MotifsEdjes = new List<KeyValuePair<int, int>>();

        // Конструктор по умолчанию для контейнера.
        public ERContainer()
        {
            log.Info("Creating ERContainer default object.");
            neighbourship = new SortedDictionary<int, List<int>>();
            degrees = new List<int>();
            Motifs4Order = new SortedDictionary<int, List<int>>();
        }

        public ERContainer(ERContainer cotainer)
        {
            this.size = cotainer.Size;
            this.neighbourship = cotainer.neighbourship;
            this.Edjes = new List<KeyValuePair<int,int>>(cotainer.Edjes);
            this.NoEdjes = new List<KeyValuePair<int,int>>(cotainer.NoEdjes);
            this.degrees = new List<int>( cotainer.degrees);
        }

        public ERContainer Copy()
        {
            ERContainer other = (ERContainer)this.MemberwiseClone();

            other.neighbourship = new SortedDictionary<int, List<int>>(this.neighbourship);
            foreach (var item in this.neighbourship)
            {
                other.neighbourship[item.Key] = new List<int>(item.Value);
            }

            other.Motifs4Order = new SortedDictionary<int, List<int>>(this.Motifs4Order);
            foreach (var item in this.Motifs4Order)
            {
                other.Motifs4Order[item.Key] = new List<int>(item.Value);
            }

            other.MotifsEdjes = new List<KeyValuePair<int, int>>(this.MotifsEdjes);
           
            other.Edjes = new List<KeyValuePair<int, int>>(this.Edjes);
            other.NoEdjes = new List<KeyValuePair<int, int>>(this.NoEdjes);
            other.degrees = new List<int>(this.degrees);
            return other;
        }

        // Размер контейнера (число вершин в графе).
        public override int Size 
        {
            get { return size; }
            set 
            {
                log.Info("Creating ERContainer object with given size.");
                size = value;                
                for (int i = 0; i < size; ++i)
                {
                    neighbourship[i] = new List<int>();
                }

                for (int i = 0; i < size; ++i)
                {
                    degrees.Add(0);
                }

                for (int i = 0; i < size; i++)
                    for (int j = i + 1; j < size; j++)
                        NoEdjes.Add(new KeyValuePair<int, int>(i, j));
            }
        }

        // Списки соседей для вершин графа.
        public SortedDictionary<int, List<int>> Neighbourship
        {
            get { return neighbourship; }
        }

        // Строится граф на основе матрицы смежности.
        public override void SetMatrix(string fileName)
        {
            ArrayList matrix = MatrixFileReader.MatrixReader(fileName);

            log.Info("Creating ERContainer object from given matrix.");
            size = matrix.Count;
            Size = matrix.Count;
            neighbourship = new SortedDictionary<int, List<int>>();
            ArrayList neighbourshipOfIVertex = new ArrayList();
            for (int i = 0; i < matrix.Count; i++)
            {
                neighbourshipOfIVertex = (ArrayList)matrix[i];
                SetDataToDictionary(i, neighbourshipOfIVertex);
            }
        }

        // Возвращается матрица смежности, соответсвующая графу.
        public override bool[,] GetMatrix()
        {
            log.Info("Getting matrix from ERContainer object.");
            bool[,] matrix = new bool[neighbourship.Count, neighbourship.Count];

            for (int i = 0; i < neighbourship.Count; i++)
            {
                for (int j = 0; j < neighbourship.Count; j++)
                {
                    matrix[i, j] = false;
                }
            }

            List<int> list = new List<int>();

            for (int i = 0; i < neighbourship.Count; i++)
            {
                list = neighbourship[i];
                for (int j = 0; j < list.Count; j++)
                {
                    matrix[i, list[j]] = true;
                }
            }

            return matrix;
        }

        public override int[][] GetBranches()
        {
            throw new NotImplementedException();
        }

        // Методы не из общего интерфейса.

        // Добавление ребра между данными вершинами.
        public void AddEdge(int i, int j)
        {
            neighbourship[i].Add(j);
            neighbourship[j].Add(i);
            ++degrees[i];
            ++degrees[j];
            var newEdjes = new KeyValuePair<int, int>(i, j);
            Edjes.Add(newEdjes);
            MotifsEdjes.Add(newEdjes);
            NoEdjes.Remove(newEdjes);
        }

        // Закрытая часть класса (не из общего интерфейса).

        private void SetDataToDictionary(int index, ArrayList neighbourshipOfIVertex)
        {
            neighbourship[index] = new List<int>();
            for (int j = 0; j < size; j++)
            {
                if ((bool)neighbourshipOfIVertex[j] == true && index != j)
                {
                    neighbourship[index].Add(j);
                    if(!Edjes.Contains(new KeyValuePair<int,int>(j,index)))
                    {
                        Edjes.Add(new KeyValuePair<int, int>(index, j));
                        MotifsEdjes.Add(new KeyValuePair<int, int>(index, j));
                        NoEdjes.Remove(new KeyValuePair<int, int>(index, j));
                    }
                }
            }
        }

        // Добавление вершины (не имеющий соседей). Не используется.
        private void AddVertex()
        {
            neighbourship.Add(size, new List<int>());
            ++size;
            degrees.Add(0);
        }

        // Проверяет являются ли данные вершины соседями (true - если да). Не используется.
        private bool AreNeighbours(int i, int j)
        {
            return neighbourship[i].Contains(j);
        }

        // Возвращает степень графа (сумма всеь степеней вершин). Не используется.
        private int CountGraphDegree()
        {
            int sum = 0;
            for (int i = 0; i < size; ++i)
                sum += CountVertexDegree(i);

            return sum;
        }

        // Возвращает число соседей данной вершины.
        private int CountVertexDegree(int i)
        {
            return neighbourship[i].Count;
        }

        public void Get4Motifs()
        {
            var isEmpty = false;

            if (Motifs4Order.Count == 0)
            {
                for (int i = 0; i < Edjes.Count; i++)
                {
                    isEmpty = false;
                    var edjes1 = Edjes[i];
                    Motifs4Order.Add(i, new List<int>());
                    for (int j = i + 1; j < Edjes.Count; j++)
                    {
                        var edjes2 = Edjes[j];

                        if (edjes1.Key != edjes2.Value && edjes1.Key != edjes2.Key
                            && edjes1.Value != edjes2.Key && edjes1.Value != edjes2.Value)
                        {
                            if (!((AreNeighbours(edjes1.Key, edjes2.Key) && AreNeighbours(edjes1.Key, edjes2.Value))
                                || (AreNeighbours(edjes1.Value, edjes2.Key) && AreNeighbours(edjes1.Value, edjes2.Value))
                                || (AreNeighbours(edjes1.Key, edjes2.Key) && AreNeighbours(edjes1.Value, edjes2.Key))
                                || (AreNeighbours(edjes1.Value, edjes2.Value) && AreNeighbours(edjes1.Key, edjes2.Value))))
                            {
                                isEmpty = true;
                                Motifs4Order[i].Add(j);
                            }
                        }
                    }

                    if (!isEmpty)
                    {
                        Motifs4Order.Remove(i);
                    }
                }
            }
        }

        public void Update4OrderMotifs(ERContainer transformation, KeyValuePair<int, int> edjes1, KeyValuePair<int, int> edjes2, int index1, int index2)
        {
            var motif = transformation.Motifs4Order[index1][index2];
            var empty1 = false;
            var empty2 = false;
            if (transformation.Motifs4Order.ContainsKey(motif))
            {
                transformation.Motifs4Order.Remove(motif);
            }

            var list = new List<int>();
            var list1 = new List<int>();
            foreach (var dic in transformation.Motifs4Order)
            {
                if (dic.Value.Contains(motif))
                {
                    if (dic.Value.Count <= 1)
                    {
                        list.Add(dic.Key);
                    }
                    else
                    {
                        transformation.Motifs4Order[dic.Key].Remove(motif);
                    }
                }

                if (dic.Value.Contains(index1))
                {
                    if (dic.Value.Count <= 1)
                    {
                        list1.Add(dic.Key);
                    }
                    else
                    {
                        transformation.Motifs4Order[dic.Key].Remove(index1);
                    }
                }
            }

            foreach (var key in list)
            {
                transformation.Motifs4Order.Remove(key);
            }

            foreach (var key in list1)
            {
                transformation.Motifs4Order.Remove(key);
            }


            if (transformation.Motifs4Order.ContainsKey(index1))
            {
                transformation.Motifs4Order.Remove(index1);
            }

            transformation.Motifs4Order.Add(transformation.MotifsEdjes.Count - 1, new List<int>());
            transformation.Motifs4Order.Add(transformation.MotifsEdjes.Count - 2, new List<int>());

            for (int i = 0; i < transformation.MotifsEdjes.Count; i++)
            {
                var edjes = transformation.MotifsEdjes[i];
                if (!edjes.Equals(new KeyValuePair<int, int>()))
                {
                    if (edjes1.Key != edjes.Value && edjes1.Key != edjes.Key
                              && edjes1.Value != edjes.Key && edjes1.Value != edjes.Value)
                    {
                        if (!((AreNeighbours(edjes1.Key, edjes.Key) && AreNeighbours(edjes1.Key, edjes.Value))
                            || (AreNeighbours(edjes1.Value, edjes.Key) && AreNeighbours(edjes1.Value, edjes.Value))
                            || (AreNeighbours(edjes1.Key, edjes.Key) && AreNeighbours(edjes1.Value, edjes.Key))
                            || (AreNeighbours(edjes1.Value, edjes.Value) && AreNeighbours(edjes1.Key, edjes.Value))))
                        {
                            empty1 = true;
                            transformation.Motifs4Order[transformation.MotifsEdjes.Count - 1].Add(i);
                        }
                    }

                    if (edjes2.Key != edjes.Value && edjes2.Key != edjes.Key
                            && edjes2.Value != edjes.Key && edjes2.Value != edjes.Value)
                    {
                        if (!((AreNeighbours(edjes2.Key, edjes.Key) && AreNeighbours(edjes2.Key, edjes.Value))
                            || (AreNeighbours(edjes2.Value, edjes.Key) && AreNeighbours(edjes2.Value, edjes.Value))
                            || (AreNeighbours(edjes2.Key, edjes.Key) && AreNeighbours(edjes2.Value, edjes.Key))
                            || (AreNeighbours(edjes2.Value, edjes.Value) && AreNeighbours(edjes2.Key, edjes.Value))))
                        {
                            transformation.Motifs4Order[transformation.MotifsEdjes.Count - 2].Add(i);
                            empty2 = true;
                        }
                    }
                }
            }
            if (!empty1)
            {
                transformation.Motifs4Order.Remove(transformation.MotifsEdjes.Count - 1);
            }

            if (!empty2)
            {
                transformation.Motifs4Order.Remove(transformation.MotifsEdjes.Count - 2);
            }

        }

        internal void CopyMotifs(ERContainer other)
        {
            other.Motifs4Order = new SortedDictionary<int, List<int>>(this.Motifs4Order);
            foreach (var item in this.Motifs4Order)
            {
                other.Motifs4Order[item.Key] = new List<int>(item.Value);
            }
        }
    }
}