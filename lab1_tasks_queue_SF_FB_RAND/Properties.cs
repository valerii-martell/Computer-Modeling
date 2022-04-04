using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Properties
    {
        private double timeProcessed = 0;
        private double timeAppeared = 0;
        private double timeInQueue = 0;
        private double timeTotal = 0;
        private double relevance = 0;
        private int number;
        private static Properties instance = new Properties();

        public int solved = 0;
        public int failures = 0;

        public double TimeProcessed
        {
            get { return this.timeProcessed; }
            set { this.timeProcessed += value; }
        }
        public double TimeAppeared
        {
            get { return this.timeAppeared; }
        }
        public double TimeInQueue
        {
            get { return this.timeInQueue; }
            set { this.timeInQueue += value; }
        }
        public double TimeTotal
        {
            get { return this.timeTotal; }
            set { this.timeTotal += value; }
        }
        public double Relevance
        {
            get { return this.relevance; }
            set { this.relevance += value; }
        }

        public static Properties Instance
        {
            get { return instance; }
        }

        public int Number
        {
            get
            {
                this.number++;
                return this.number;
            }
        }
        public void Solved()
        {
            this.solved++;
        }
        public void Unsolved()
        {
            this.failures++;
        }
    }
}
