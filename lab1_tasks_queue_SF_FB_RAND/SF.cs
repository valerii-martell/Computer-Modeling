using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class SF
    {
        private static double lambda = 0.7, mu = 1;
        private static double time;
        static double ttask = 0;
        static double tproc = 0;
        public static int n = 0, success = 0, counter = Program.count;

        static Properties properties = Properties.Instance;

        public static List<Task> queue = new List<Task>();

        public static void Start()
        {
            Task next = null;

            ttask += gen();//task appearance time
            Console.WriteLine(ttask);
            next = new Task(ttask, id());
            tproc = ttask + next.timeProcessed; //task processing time

            //LOOP

            ttask += gen();
            //while(success<1000){
            for (int jj = 0; jj < counter; jj++)
            {
                //System.out.println(ttask);
                if (tproc > ttask)
                {
                    while (tproc > ttask)
                    {
                        queue.Add(new Task(ttask, id()));
                        ttask += gen();
                    }
                    //LOOP

                    //process the task
                    next = min();
                    if (process(next)) inc();
                    queue.Remove(next);


                }
                else
                {
                    if (queue.Any<Task>())
                    {
                        //while(tproc<ttask) {
                        next = min();
                        //if (rand()==null) System.out.println("BINGO");;
                        if (process(next)) inc();
                        queue.Remove(next);
                        //}

                    }
                    else
                    {
                        tproc = ttask;
                        if (process(new Task(ttask, id()))) inc();
                    }
                }
            }

            Console.WriteLine("Tasks solved: {0}", success);
            Console.WriteLine("Total time in queue: {0}", properties.TimeInQueue / counter);
            Console.WriteLine("Average processing time: {0}", properties.TimeProcessed / counter);
            Console.WriteLine("Average time in system: {0}", properties.TimeTotal / counter);
            Console.WriteLine("Average relevance {0}", properties.Relevance / counter);
            Console.WriteLine("Failed tasks {0}", properties.failures);
        }

        public static Task min()
        {
            Task n = null;
            double min = Double.MaxValue;
            if (!queue.Any<Task>())
                Console.WriteLine("QUEUE IS EMPTY");
            foreach (Task it in queue)
            {
                if (it.timeProcessed < min)
                {
                    min = it.timeProcessed;
                    n = it;
                }
            }
            return n;
        }

        static bool process(Task t)
        {
            t.toProc(tproc);
            if (t.relevance != 0)
            {
                tproc += t.timeProcessed;
                properties.Solved();
                t.finish(tproc);
                return true;
            }
            return false;
        }
        public static double gen()
        {
            Random rnd = new Random();
            return -(1 / lambda) * Math.Log(rnd.NextDouble());
        }
        public static int id() { return SF.n++; }
        public static void inc()
        {
            success++;
            if (success == counter) Console.WriteLine("it's all ogre now");
        }

    }
}
