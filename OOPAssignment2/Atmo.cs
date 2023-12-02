using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class ListIsEmpty : Exception { }
    abstract class Atmo
    {
        
        protected char name;
        protected Atmo(char name) { this.name = name; }
        public char getName() { return name; }
        public void Change(List<Gas> g, string firstGas, string secondGas, double multiplier)
        {
            if (g.Count == 0) { throw new ListIsEmpty(); }
            for (int i = 0; i < g.Count; i++)
            {
                if (g[i].getID() == firstGas)
                    for (int j = i + 1; j < g.Count; j++)
                    {
                        if (g[j].getID() == secondGas)
                            SecondGasExist(g, firstGas, secondGas, multiplier, i, j);
                        else
                            SecondGasNotExist(g, firstGas, secondGas, multiplier, i, j);
                    }
            }
        }
        private static void SecondGasNotExist(List<Gas> g, string firstGas, string secondGas, double multiplier, int i, int j)
        {
            g.Add(GasFactory.CreateGas(secondGas, g[i].getThick() * multiplier));

            if (g[g.Count - 1].getThick() < 0.5)
                g.Remove(g[g.Count - 1]);

            CreateGas2(g, multiplier, i);
            if (g[i].getThick() < 0.5)
                for (int k = j + 1; k < g.Count; k++)
                {
                    if (g[k].getID() != firstGas)
                        g.RemoveAt(i);
                    else if (g[k].getID() == firstGas)
                    {
                        CreateGas3(g, i, k);
                        g.RemoveAt(i);
                    }
                }   
        }
        private static void SecondGasExist(List<Gas> g, string firstGas, string secondGas, double multiplier, int i, int j)
        { 
            CreateGas1(g, multiplier, i, j);
            CreateGas2(g, multiplier, i);
            if (g[i].getThick() < 0.5)
                for (int k = j + 1; k < g.Count; k++)
                {
                    if (g[k].getID() != firstGas)
                        g.RemoveAt(i);
                    else if (g[k].getID() == firstGas)
                    {
                        CreateGas3(g, i, k);
                        g.RemoveAt(i);
                    }
                }
        }
        private static void CreateGas3(List<Gas> g, int i, int k)
        {
            g[k] = GasFactory.CreateGas(g[k].getID(), g[i].getThick() + g[k].getThick());
        }
        private static void CreateGas2(List<Gas> g, double multiplier, int i)
        {
            g[i] = GasFactory.CreateGas(g[i].getID(), g[i].getThick() - (g[i].getThick() * multiplier));
        }
        private static void CreateGas1(List<Gas> g, double multiplier, int i, int j)
        {
            g[j] = GasFactory.CreateGas(g[j].getID(), g[i].getThick() * multiplier + g[j].getThick());
        }
    }
    class ThunderStorm : Atmo
    { 
        private static ThunderStorm instance;
        private ThunderStorm() : base('T') { }
        public static ThunderStorm Instance()
        {
            if (instance == null)
                instance = new ThunderStorm();
            return instance;
        }
    }
    class SunShine : Atmo
    {
        private static SunShine instance;
        private SunShine() : base('S') { }
        public static SunShine Instance()
        {
            if (instance == null)
                instance = new SunShine();
            return instance;
        }

    }
    class Other : Atmo 
    {
        private static Other instance;
        private Other() : base('O') { }
        public static Other Instance()
        {
            if (instance == null)
                instance = new Other();
            return instance;
        }
    }
}
