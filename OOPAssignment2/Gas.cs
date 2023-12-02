using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextFile;

namespace OOPAssignment2
{
    public abstract class Gas
    {
        protected string id;
        protected double Thickness;
        protected Gas(string id, double Thickness)
        {
            this.id = id;
            this.Thickness = Thickness;
        }
        public double getThick() { return Thickness; }
        public string getID() { return id; }
        public override string ToString()
        {
            return $"{id} {Thickness.ToString("0.00")}";
        }
    }
    class Ozone : Gas
    {
        public Ozone(double Thickness) : base("Z", Thickness) { }        
    }
    class Oxygen : Gas
    {
        public Oxygen(double Thickness) : base("X", Thickness) { }    
    }
    class CarbonD : Gas
    {
        public CarbonD(double Thickness) : base("C", Thickness) { }
    }
    public class GasFactory
    {
        public static Gas CreateGas(string id, double thickness)
        {
            switch (id)
            {
                case "Z":
                    return new Ozone(thickness);
                case "X":
                    return new Oxygen(thickness);
                case "C":
                    return new CarbonD(thickness);
                default:
                    throw new ArgumentException($"Invalid gas type: {id}");
            }
        }
    }
}
