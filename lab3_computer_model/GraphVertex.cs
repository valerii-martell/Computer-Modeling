using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class GraphVertex
    {
        public int[][] state;
        public List<GraphVertex> outs = new List<GraphVertex>();
        public List<GraphVertex> ins = new List<GraphVertex>();
        public List<int[][]> relationState = new List<int[][]>();

        public List<double> probabilities = new List<double>();
        public List<double> intensities = new List<double>();
        public List<double> finalOutIntensities = new List<double>();
        public List<double> finalInIntensities = new List<double>();


        //    public GraphVertex(int[,] s, List<NobleUnit> r, double[] p, int[] i ){
        //        state = s;
        //        outs = r;
        //        probabilities = p;
        //        intensities = i;
        //
        //    }
        public void settingIns()
        {
            for (int i = 0; i < ins.Count; i++)
            {
                GraphVertex tmp = ins[i];
                for (int j = 0; j < tmp.outs.Count; j++)
                {
                    if (tmp.outs[j].state == this.state)
                    {
                        finalInIntensities.Add(tmp.finalOutIntensities[j]);
                        break;
                    }
                }
            }
        }
        public void print()
        {
            Console.WriteLine();
            Console.Write("State : ");
            printA(state);
            Console.WriteLine();
            for (int y = 0; y < ins.Count; y++)
            {
                Console.Write(" from ");
                printA(ins[y].state);
                Console.WriteLine("with intensity " + finalInIntensities[y]);

            }
            for (int x = 0; x < outs.Count; x++)
            {
                //                                                                 ERROR
                Console.Write(" to   ");
                printA(outs[x].state);
                Console.WriteLine(" with intensity " + finalOutIntensities[x]);
                //            System.out.println(" with p = "+probabilities.get(x)+" and intensity "+intensities.get(x));
            }
            //           return null;
        }


        public void addRelations(int[][] s)
        {
            relationState.Add(s);
        }
        public void addFinalIntesities(double prob, double inten)
        {
            intensities.Add(inten);
            probabilities.Add(prob);
            finalOutIntensities.Add(prob * inten);
        }
        private void printA(int[][] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(" <");
                for (int j = 0; j < a[i].Length; j++)
                {
                    Console.Write(a[i][j]);
                }
                Console.Write(">");
            }
        }

        public void addIntensities(double tau)
        {
            intensities.Add(tau);
        }

        public void addProbabilities(Double aDouble)
        {
            probabilities.Add(aDouble);
        }
    }
}
