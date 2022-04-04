using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class UnitFactory
    {
        private Unit CPU, VPU, GPU, Router, RAM, NorthBridge, SouthBridge;
        private List<Unit> units = new List<Unit>();

        public List<Unit> Units
        {
            get { return this.units; }
            set { this.units = value; }
        }
        public UnitFactory()
        {
            //TAU
            CPU = new Unit(1, "CPU");
            VPU = new Unit(100, "VPU");
            GPU = new Unit(50, "GPU");
            RAM = new Unit(20, "RAM");
            NorthBridge = new Unit(1, "North Bridge");
            SouthBridge = new Unit(5, "South Bridge");
            Router = new Unit(100, "Router");

            assembleUnits();
            //        Console.WriteLine(units);
        }

        private void assembleUnits()
        {
            units.Add(getCPU());
            units.Add(getVPU());
            units.Add(getGPU());
            units.Add(getRAM());
            units.Add(getNBridge());
            units.Add(getSBridge());
            units.Add(getRouter());
        }

        public Unit getCPU()
        {
            CPU.link.Add(NorthBridge, 1.0);
            return CPU;
        }
        public Unit getVPU()
        {
            VPU.link.Add(CPU, 1.0);
            return VPU;
        }
        public Unit getGPU()
        {
            GPU.link.Add(NorthBridge, 1.0);
            return GPU;
        }
        public Unit getRouter()
        {
            Router.link.Add(SouthBridge, 1.0);
            return Router;
        }
        public Unit getRAM()
        {
            RAM.link.Add(CPU, 0.1);
            RAM.link.Add(NorthBridge, 0.9);
            return RAM;
        }
        public Unit getNBridge()
        {
            NorthBridge.link.Add(RAM, 0.96);
            NorthBridge.link.Add(CPU, 0.01);
            NorthBridge.link.Add(GPU, 0.01);
            NorthBridge.link.Add(VPU, 0.01);
            NorthBridge.link.Add(SouthBridge, 0.01);
            return NorthBridge;
        }
        public Unit getSBridge()
        {
            SouthBridge.link.Add(SouthBridge, 0.78);
            SouthBridge.link.Add(Router, 0.22);
            //        SouthBridge.link.put(Router,1.0);
            return SouthBridge;
        }
    }
}
