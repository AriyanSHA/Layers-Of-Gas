using System.ComponentModel;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using TextFile;

namespace OOPAssignment2
{

    public class Program
    {

        public class EmptyFileError : Exception { }
        private static List<Gas> Layers; 
        private static List<Atmo> atmoVars;

        static void Main(string[] args)
        {
            atmoVars = new List<Atmo>();
            Layers = new List<Gas>();
            string inputAtmo = "";
            bool l;
            try
            {
                TextFileReader reader = new TextFileReader("input.txt");
                try
                {
                    reader.ReadLine(out string line); int n = int.Parse(line);
                    l = reader.ReadString(out string id);
                    l = reader.ReadDouble(out double Thickness) && l;
                    while (l)
                    {
                        Layers.Add(GasFactory.CreateGas(id, Thickness));

                        l = reader.ReadString(out id) && l;
                        if (id != "X" && id != "Z" && id != "C") inputAtmo += id;
                        l = reader.ReadDouble(out Thickness) && l;
                    }
                    Console.WriteLine(n);
                    Print();
                    Console.WriteLine(inputAtmo);
                    Console.WriteLine("-----------");
                }
                catch { throw new EmptyFileError(); }
            }
            catch { throw new FileNotFoundException(); }

            foreach (char e in inputAtmo)
            {
                if (e == 'O')
                    atmoVars.Add(Other.Instance());
                else if (e == 'S')
                    atmoVars.Add(SunShine.Instance());
                else if (e == 'T')
                    atmoVars.Add(ThunderStorm.Instance());
            }

            bool check = true;
            while (check)
            {
                for (int i = 0; i < atmoVars.Count; i++) //OOOOSSTSSOO
                {
                    if (CheckThunder(atmoVars[i].getName(), ref check))
                        atmoVars[i].Change(Layers, "X", "Z", 0.5);

                    else if (CheckSunshine(atmoVars[i].getName(), ref check))
                    {
                        atmoVars[i].Change(Layers, "X", "Z", 0.05);
                        atmoVars[i].Change(Layers, "C", "X", 0.05);
                    }
                    else if (CheckOther(atmoVars[i].getName(), ref check))
                    {
                        atmoVars[i].Change(Layers, "Z", "X", 0.05);
                        atmoVars[i].Change(Layers, "X", "C", 0.1);
                    }
                    if (i ==  atmoVars.Count - 1)
                        i = 0;
                    else
                    {
                        checkLayerStatus(ref check);
                        Print();
                        Console.WriteLine("-----------");
                        if (check == false)
                        {
                            Console.WriteLine("A layer of gas has perished from atmosphere!");
                            break;
                        }
                    }
                    
                }
            }
        }
        private static void Print()
        {
            for (int j = 0; j < Layers.Count; j++)
                Console.WriteLine(Layers[j].ToString());
        }
        private static void checkLayerStatus(ref bool check)
        {
            if (!Layers.OfType<Ozone>().Any())
                check = false;
            if (!Layers.OfType<Oxygen>().Any())
                check = false;
            if (!Layers.OfType<CarbonD>().Any())
                check = false;
        }

        //THE FOLLOWING REGIONS ARE USED JUST FOR THE TESTS!
        #region checkGas 
        public static bool CheckOzone(string id)
        {
            return id == "Z";
        }
        public static bool CheckCarbonD(string id)
        {
            return id == "C";
        }
        public static bool CheckOxygen(string id)
        {
            return id == "X";
        }
        #endregion checkGas

        #region checkAtmos
        public static bool CheckThunder(char atmoVar, ref bool check)
        {
            return atmoVar == 'T' && check;
        }
        public static bool CheckSunshine(char atmoVar, ref bool check)
        {
            return atmoVar == 'S' && check;
        }
        public static bool CheckOther(char atmoVar, ref bool check)
        {
            return atmoVar == 'O' && check;
        }
        #endregion checkAtmos
    }
}