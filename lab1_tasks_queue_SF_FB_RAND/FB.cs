using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class FB
    {
        public static double lambda = 0.7, mu = 1, tau = 0.5;
        private static double time;
        static double ttask = 0;
        static double tproc = 0;
        public static int n = 0;
        static Properties properties = Properties.Instance;
        static List<Task> queue = new List<Task>();
        static List<List<Task>> extQueue = new List<List<Task>>();
        private static int y = Program.count;

        public static void Start()
        {

            Task next = null;
            int success = 0;
            int i = 0;
            double time, time1;


            extQueue.Add(queue);
            ttask += gen();//task appearance time
            tproc += ttask;
            Console.WriteLine(ttask);
            next = new Task(tproc, id());
            process(next);
            //LOOP

            ttask += gen();
            //        while(success<y){
            for (int counter = 0; counter < y; counter++)
            {

                if (tproc > ttask)
                {
                    while (tproc > ttask)
                    {
                        extQueue[0].Add(new Task(ttask, id()));
                        ttask += gen();
                    }
                    //LOOP

                    //process the task
                    next = (Task)extQueue[0][0];
                    //                extQueue.remove(next);
                    if (process(next)) success++;


                }
                else
                {
                    for (int r = 0; r < extQueue.Count; r++)
                    {
                            if (extQueue[r].Any<Task>())
                            {
                                //                        while(tproc<ttask) {
                                next = (Task)extQueue[r][0];
                            //                            if (rand()==null) System.out.println("BINGO");;
                            if (process(next)) success++;
                            //                        }

                        }
                        if ((r == extQueue.Count - 1) && !extQueue[r].Any<Task>())
                        {
                            if (process(new Task(ttask, id()))) success++;
                            tproc = ttask;
                        }
                    }
                }
            }

                Console.WriteLine("IT'S OVER");
                Console.WriteLine("Tasks solved: {0}" ,success);
                Console.WriteLine("Total time in queue: {0}" , properties.TimeInQueue / y);
                Console.WriteLine("Average processing time: {0}" , properties.TimeProcessed / y);
                Console.WriteLine("Average time in system: {0}" , properties.TimeTotal / y);
                Console.WriteLine("Average relevance {0}" , properties.Relevance / y);
                Console.WriteLine("Failed tasks {0}" , properties.failures);

            }

        static bool process(Task t)
        {
            t.toProc(tproc);
            extQueue[t.queueNumber].Remove(t);
            if (t.relevance == 0) { return false; }
            tproc += tau;
            if (tau > t.timeProcessed)
            {
                properties.Solved();
                t.finish(tproc);
                return true;
            }
            else
            {
                t.failedProcessing();                   //inc queue N and substract processed time from task size
                if (t.queueNumber > (extQueue.Count - 1))
                {//if queue doesn't exist
                    extQueue.Add(queue);
                }
                extQueue[t.queueNumber].Add(t);     //add to next queue
                return false;
            }
        }

                public static double gen()
                {
                    Random rnd = new Random();
                    return -(1 / lambda) * Math.Log(rnd.NextDouble());
                }
                public static int id() { return SF.n++; }
    
}
}
