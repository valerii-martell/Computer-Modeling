using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Task
    {
        private double time = 0;
        private Unit currentPlace = null;

        public void AddTime(double value)
        {
            Time += value;
        }
        public double Time
        {
            get { return this.time; }
            set { this.time = value; }
        }
        public Unit CurrentPlace
        {
            get { return this.currentPlace; }
            set { this.currentPlace = value; }
        }
    }
}
