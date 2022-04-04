using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Task
    {
        private const double mu = 1;
        public Properties properties = Properties.Instance;
        public double timeProcessed;
        public double timeEnteredQueue;
        public double timeInQueue;
        public double timeTotal;
        public int number = 0;
        public int queueNumber = 0;
        public double lambda = FB.lambda;
        public double tau = FB.tau;
        public double relevance = 1;
        

        public Task(double time, int n)
        {
            Random rnd = new Random();
            this.number = n;
            this.timeProcessed = -(1 / mu) * Math.Log(rnd.NextDouble());
            this.timeEnteredQueue = time;
            Console.WriteLine("Task-{0} is in queue. Time: {1}", number, time);
            Console.WriteLine("Processed time = {0}",this.timeProcessed);
            //        number++;
            properties.TimeProcessed = timeProcessed;
        }


        public void toProc(double time)
        {
            //TIME IN QUEUE CALCULATION
            this.timeInQueue = time - timeEnteredQueue;
            if (timeInQueue < 0)
                Console.WriteLine("Ooops! Time in queue < 0");

            //RELEVANCE CALCULATION
            if (timeInQueue <= 2)
            {
                this.relevance = 1;
            }

            if (timeInQueue > 4)
            {
                this.relevance = 0;
                properties.Unsolved();
            }
            else
            {
                this.relevance = 1 - 0.5 * (timeInQueue - 2);
            }

            //Properties
            properties.Relevance=this.relevance;
            properties.TimeInQueue=timeInQueue;
            properties.TimeTotal = timeInQueue + timeProcessed;


        }

        public void inc()
        {
        }
        public void finish(double time)
        {
            Console.WriteLine("Task-{0} is  processed: {1}", number, time);
        }
        public void failedProcessing()
        {
            this.queueNumber++;
            this.timeProcessed -= tau;
        }
    }
}
