using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class NobleUnitFactory
    {
        private NobleUnit CPU, VPU, GPU, Router, RAM, NBridge, SBridge;
        private List<NobleUnit> nobleUnits = new List<NobleUnit>();


        public NobleUnitFactory()
        {
            //TAU
            CPU = new NobleUnit("CPU", 100);
            VPU = new NobleUnit("VPU", 10);
            GPU = new NobleUnit("GPU", 50);
            RAM = new NobleUnit("RAM", 80);
            NBridge = new NobleUnit("North Bridge", 100.0);


            SBridge = new NobleUnit("South Bridge", 95);
            Router = new NobleUnit("Router", 1);

            assembleUnits();
            //        System.out.println(nobleUnits);
        }
        private void assembleUnits()
        {
            nobleUnits.Add(getCPU());
            nobleUnits.Add(getVPU());
            nobleUnits.Add(getGPU());
            nobleUnits.Add(getRAM());
            nobleUnits.Add(getNBridge());
            nobleUnits.Add(getSBridge());
            nobleUnits.Add(getRouter());
        }

        private NobleUnit getCPU()
        {
            CPU.setRelations(NBridge, 1.0);
            //        CPU.setRelations(VPU, 0.5);
            //        CPU.setRelations(GPU, 1);
            //        CPU.setRelations(NBridge, 0.4);
            //        CPU.setRelations(CPU, 0.2);
            return CPU;
        }
        private NobleUnit getVPU()
        {
            VPU.setRelations(NBridge, 1.0);
            //        VPU.setRelations(CPU, 0.5);
            //        VPU.setRelations(GPU, 1);
            return VPU;
        }
        private NobleUnit getGPU()
        {
            GPU.setRelations(NBridge, 1.0);
            //        GPU.setRelations(VPU, 0.5);
            //        GPU.setRelations(CPU, 0.5);
            return GPU;
        }
        private NobleUnit getRouter()
        {
            Router.setRelations(SBridge, 1.0);
            return Router;
        }
        private NobleUnit getRAM()
        {
            RAM.setRelations(NBridge, 1.0);
            return RAM;
        }
        private NobleUnit getNBridge()
        {
            NBridge.setRelations(RAM, 0.2);
            NBridge.setRelations(CPU, 0.2);
            NBridge.setRelations(GPU, 0.39);
            NBridge.setRelations(VPU, 0.01);
            NBridge.setRelations(SBridge, 0.2);
            //        NBridge.setRelations(CPU, 0.8);
            //        NBridge.setRelations(GPU, 0.2);
            return NBridge;
        }
        private NobleUnit getSBridge()
        {
            SBridge.setRelations(NBridge, 0.9);
            SBridge.setRelations(Router, 0.1);
            return SBridge;
        }

        public List<NobleUnit> getNobleUnits() { return nobleUnits; }
    }
}
