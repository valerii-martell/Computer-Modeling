using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Unit
    {
        private bool processorState = true;        //is processor empty?
        private int tau, releaseTime, load = 0, coreUsage = 0, absoluteTime = 0;
        public static List<Task> queue = new List<Task>();
        public Dictionary<Unit, double> link = new Dictionary<Unit, double>();
        private Task current;
        private String name;


        public Unit(int m, String n)
        {
            tau = m;
            name = n;
        }



        public override string ToString()
        {
            return name;
        }

        public void place(Task t, int dieZeit)
        {
            //        Console.WriteLine("task placed on "+this.toString());
            //        coreUsage++;
            t.CurrentPlace=this;
            if (processorState)
            {
                load += tau;
                coreUsage++;
                current = t;
                releaseTime = dieZeit + tau;
                //            t.addTime(tau);
                t.Time=absoluteTime;
                invade();
            }
            else
            {
                queue.Add(t);
                //            Console.WriteLine(t.toString()+"trapped in queue");
            }
        }
        public double getReleaseTime()
        {
            return releaseTime;
        }


        public void freeTheOpressed()
        {
            Unit next = nowWhere();
            //        System.out.println(next);
            next.place(current, releaseTime);

            if (queue.Any())
            {
                load += tau;
                coreUsage++;

                current = queue[0];
                queue.RemoveAt(0);
                current.Time=absoluteTime;
                releaseTime += tau;
                invade();
            }
            else
            {
                release();
            }
        }
        public Unit nowWhere()
        {                      //LETS US KNOW WHERE THE TASK WILL BE CONVEYED
            double probability = 0;
            double r = new Random().NextDouble();

            foreach(KeyValuePair<Unit, double> kvp in link)
            {
                probability += kvp.Value;
                //            Console.WriteLine(entry.getKey());
                if (r <= probability)
                    return kvp.Key;
            }
            Console.WriteLine("Wrong");
            return null;

            //CANCER ALERT
            //        ArrayList<Double> c= (ArrayList<Double>) link.values();
            //        Iterator it = set.iterator();
            //        while (it.hasNext()){
            //        }
            //        double []a=new double[c.size()];
            //        for (int i=0;i<c.size();i++){   //check if sum is 1
            //            a[i]=c.get(i);//may add 1
            //            p+=a[i];
            //
            //        }
            //        if (p!=1) {
            //            Console.WriteLine("fuckup");
            //            return null;
            //        }
            //        return  null;

        }

        public void theTimeHasCome()
        {

        }

        public bool isEmpty()
        {
            return processorState;
        }
        public void release() { if (!queue.Any()) processorState = true; }
        public void invade()
        {
            processorState = false;
        }
        public int getLoad()
        {
            return load;
        }
        public void interfere(int step)
        {
            if (!isEmpty())
            {
                load += step - (int)current.Time - tau;
            }
        }
        public void inc()
        {

        }

        public int getTau()
        {
            return tau;
        }
        public int getCoreUsage() { return coreUsage; }

        public void setAbsoluteTime(int absoluteTime)
        {
            this.absoluteTime = absoluteTime;
        }
    }
}
