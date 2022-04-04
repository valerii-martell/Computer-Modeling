using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;

namespace Lab3
{
    public class NobleUnit
    {
        private String name;
        private bool procFree = true;
        public double tau = 0;
        private List<Task> queue = new List<Task>();
        public List<NobleUnit> relationsObjects = new List<NobleUnit>();
        public List<double> realtionsProbabilities = new List<double>();
        private Task current = null;

        public NobleUnit(string n, double t/*, NobleUnit unit, double prob*/)
        {
            name = n;
            //        tau = 1/t;
            tau = t;
            Console.WriteLine(this + " tau = " + tau);
            //        tau = t;
            //        setRelations(unit, prob);
        }

        public void put(Task task)
        {
            if (procFree)
            {
                current = task;
                procFree = false;
            }
            else
            {
                queue.Add(task);
            }
        }

        public void finish()
        {
            if (!queue.Any())
            {
                setCurrent(null);
            }
            else
            {
                setCurrent(queue[0]);
                queue.RemoveAt(0);
            }
        }

        public void setRelations(NobleUnit unit, double p)
        {
            relationsObjects.Add(unit);
            realtionsProbabilities.Add(p);
        }

        public String toString()
        {
            return name;
        }

        public bool isEmpty()
        {
            return procFree;
        }

        public void setCurrent(Task current)
        {
            if (current == null)
            {
                this.current = null;
                procFree = true;
            }
            else
            {
                this.current = current;
                procFree = false;
            }
        }
        public int queueSize()
        {
            return queue.Count;
        }
    }
}
