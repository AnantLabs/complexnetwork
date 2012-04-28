using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelCheck
{
    public enum PossibleAnswers
    {
        Yes,
        No,
        MaybeNo
    }

    public class HierarchicChecker
    {
        private List<int> d;
        private int p;
        private int t;

        public HierarchicChecker(List<int> t)
        {
            d = t;
        }

        public PossibleAnswers Process(List<List<int>> k)
        {
            List<int> subk = new List<int>();
            List<int> subk1 = new List<int>();

            for (int l = 1; l <= t; l++) // makardakner@
            {
                int localLev = (int)Math.Pow(p, t - l);
                int levelElemCount = (int)Math.Pow(p, l);
                int prevLevelElemCount = (int)Math.Pow(p, l - 1);

                for (int h = 1; h <= localLev; h++) // qani hat p-yak ka amen makardakum
                {
                    for (int i = l; i < t; i++)
                    {
                        for (int j = (h - 1) * levelElemCount; j + prevLevelElemCount < h * levelElemCount; j += prevLevelElemCount)
                        {
                            if (k[j][i] != k[j + prevLevelElemCount][i])
                            {
                                return PossibleAnswers.No;
                            }
                        }

                        subk1.Add(k[(h - 1) * levelElemCount][i]);
                    }

                    for (int j = (h - 1) * levelElemCount; j < h * levelElemCount; j += prevLevelElemCount)
                        subk.Add(k[j][l - 1]);

                    if (HavelHakimiCheck(subk))
                    {
                        subk1.Clear();
                        subk.Clear();
                    }
                    else
                    {
                        bool chk = true;
                        if (h > 1)
                        {
                            for (int v = (h - 1) * levelElemCount - levelElemCount; v < (h - 1) * levelElemCount; v++)
                                for (int i = l; i < t; i++)
                                    if (subk1[i - 1] != k[v][i])
                                        chk = false;

                        }
                        if (h < localLev)
                        {
                            for (int v = (h - 1) * levelElemCount + levelElemCount; v < h * levelElemCount + levelElemCount; v++)
                                for (int i = l; i < t; i++)
                                    if (subk1[i - 1] != k[v][i])
                                        chk = false;

                        }
                        if (chk == false)
                            return PossibleAnswers.No;
                        if (localLev == 1 && chk == true)
                            return PossibleAnswers.No;
                        return PossibleAnswers.MaybeNo;
                    }
                }
            }

            return PossibleAnswers.Yes;
        }

        private void CalcParms()
        {
            int k;

            for (p = 2; ; p++)
                for (t = 1; ; t++)
                {
                    k = (int)Math.Pow(p, t);

                    if (k == d.Count)
                        return;

                    if (k > d.Count)
                        break;
                }

        }

        private static int Comparing(List<int> k1, List<int> k2)
        {
            if (k1.Count != k2.Count)
                throw new SystemException();

            for (int i = k1.Count - 1; i >= 0; i--)
                if (k1[i] != k2[i])
                    return k1[i] - k2[i];

            return 0;
        }

        private List<List<int>> Polynom()
        {
            List<List<int>> k = new List<List<int>>();

            for (int i = 0; i < d.Count; i++)
            {
                k.Add(new List<int>(d.Count));
                int degree = d[i];
                for (int j = 0; j < t; j++)
                {
                    k[i].Insert(j, degree % p);
                    degree /= p;
                }
            }
            k.Sort(Comparing);

            return k;

        }

        private static List<int> HavelHakimi(List<int> d)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < d[0]; i++)
                result.Add(d[i + 1] - 1);
            for (int i = d[0] + 1; i < d.Count; i++)
                result.Add(d[i]);
            return result;

        }

        private static bool HavelHakimiCheck(List<int> d)
        {
            List<int> f = new List<int>();
            for (int i = 0; i < d.Count; i++)
                if (d[i] >= d.Count || d[i] < 0)
                    return false;

            for (int i = 0; i < d.Count; i++)
            {
                d.Sort();
                d.Reverse();
                f = HavelHakimi(d);
                d = f;
            }

            for (int i = 0; i < d.Count; i++)
                if (d[i] != 0)
                    return false;
            return true;
        }
    }
}
