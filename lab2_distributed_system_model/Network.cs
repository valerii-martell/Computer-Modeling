using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Network
    {
        private const int N = 6;
        private const int t = 10000;
        private static UnitFactory factory = new UnitFactory();
        double g = 0;

        private static List<Unit> unitArray = factory.Units;


        public static void Start()
        {
            Console.WriteLine("NUMBER OF TASKS: {0}", N);
            for (int w = 0; w < N; w++)
            {
                //            int r=new Random().nextInt(unitArray.size());
                //placing tasks in random nodes
                //            Console.WriteLine(unitArray.size());
                //            Console.WriteLine(r);
                unitArray[0].place(new Task(), 0);
                //            Console.WriteLine("Task is placed on "+unitArray.get(r).toString()+" : tau = "+unitArray.get(r).getTau());
                //            unitArray.get(r).place(new Task(),0);
            }


            //        Console.WriteLine(unitArray.size());
            for (int i = 0; i < t; i++)
            {
                foreach (Unit it in unitArray)
                {
                    it.setAbsoluteTime(i);//on each iteration we pass Unit absolute time
                    if (!it.isEmpty() && it.getReleaseTime() == i)
                        it.freeTheOpressed();

                }
            }
            foreach (Unit it in unitArray)
            {
                it.interfere(t);
            }


            int sum = 0;
            Console.WriteLine("END");
            foreach (Unit it in unitArray)
            {
                Console.Write(it.ToString() + " : tau = " + it.getTau() + ". Load time ");
                Console.WriteLine((double)it.getLoad() / t + "%"/*+". Was used "+it.getCoreUsage()+" times"*/);

                sum += it.getLoad();
            }
            Console.WriteLine(sum + " out of " + t * N);

        }
    }
}
