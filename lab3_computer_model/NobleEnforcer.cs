using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Lab3
{
    static class DeepEquals
    {
        public static bool IsBinaryEqualTo(this object obj, object obj1)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                if (obj == null || obj1 == null)
                {
                    if (obj == null && obj1 == null)
                        return true;
                    else
                        return false;
                }

                BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(memStream, obj);
                byte[] b1 = memStream.ToArray();
                memStream.SetLength(0);

                binaryFormatter.Serialize(memStream, obj1);
                byte[] b2 = memStream.ToArray();

                if (b1.Length != b2.Length)
                    return false;

                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                        return false;
                }

                return true;
            }
        }
    }
    public class NobleEnforcer
    {
        private const int N = 6;
        private static int[] emptyNode = { 0, 0, 1 }, occupiedNode = { 0, 1, 0 };
        private static NobleUnitFactory factory = new NobleUnitFactory();
        private static List<GraphVertex> graph = new List<GraphVertex>();
        private static List<NobleUnit> system = factory.getNobleUnits();
        private static List<int[][]> resultArray = new List<int[][]>();
        private static List<double> loadFactor = new List<double>();
        private static List<int> check = new List<int>();
        private static int[][] initialState;
        private static double[] sols;

        public static void Start()
        {
            //PLACING TASKS
            for (int w = 0; w < N; w++)
            {
                system[0].put(new Task());
            }

            //SETTING INITIAL STATE
            setInitialState();
            resultArray.Add(initialState);

            //BUILDING A TREE
            treeBuilder(initialState);

            //BUILDING A GRAPH
            graphBuilder();


            //EQUATIONS


            buildEquations();

            //CALCULATING LOAD
            calculateLoad();

            //PRINT PLEASE

            //        for (int[][] it:resultArray){
            //
            //            printPlease(it);
            //        }
            Console.WriteLine("Size: " + resultArray.Count);
            foreach(NobleUnit it1 in system)
            {
                Console.WriteLine(it1 + " " + it1.tau + " | ");
            }
            //        System.out.println();
            //        for(int i = 0; i<graph.size(); i++){
            //
            graph[0].print();
            //        }

            //        System.out.println("ins");
            //        for (int i = 0;i<graph.get(0).ins.size();i++){
            //            graph.get(0).ins.get(i).print();
            //        }
            //        System.out.println("outs");
            //        for (int i = 0; i<graph.get(0).outs.size(); i++){
            //            graph.get(0).outs.get(i).print();
            //        }

            for (int i = 0; i < system.Count; i++)
            {
                //            double tmp = 0.0;
                //            if (loadFactor.get(i)>0.0001){
                //                tmp = loadFactor.get(i);
                //            }
                Console.WriteLine(system[i] + " load is " + loadFactor[i] + " ( " + check[i] + " )");
                //            Console.WriteLine(sols.getEntry(i));
                //            Console.WriteLine( "%.2f", loadFactor.get(i));
            }
        }

        private static void calculateLoad()
        {
            Console.WriteLine("calculations");
            for (int i = 0; i < system.Count; i++)
            {
                double load = 0;
                int c = 0;
                for (int k = 0; k < graph.Count; k++)
                {
                    if (graph[k].state[i][1] == 1)
                    {
                        load += sols[k];
                        c++;
                    }
                }
                //            Console.WriteLine(load);
                //            Console.WriteLine(sols.getEntry(i));
                loadFactor.Add(load);
                check.Add(c);
            }
        }

        public static void graphBuilder()
        {

            for (int i = 0; i < graph.Count; i++)
            {       //going through invalid graph vertexes

                //SETTING OUT RELATIONS
                for (int j = 0; j < graph[i].relationState.Count; j++)
                {      //adding GV objects to 'outs'
                       //identical values in 'relationState' and 'state'
                    for (int k = 0; k < graph.Count; k++)
                    {                       //by looking for GV'system in 'graph' with

                        if (graph[i].relationState[j].IsBinaryEqualTo(graph[k].state))
                        {
                            graph[i].outs.Add(graph[k]);
                        }
                    }
                }
                //SETTING IN RELATIONS
                for (int m = 0; m < graph.Count; m++)
                {                                //looking for another GV'system
                    for (int n = 0; n < graph[m].relationState.Count; n++)
                    {      //with adress of current GV in their
                        if (graph[i].state.IsBinaryEqualTo(graph[m].relationState[n]))
                        {//outer outs
                            graph[i].ins.Add(graph[m]);
                        }
                    }
                }
            }
            //REMOVE CYCLIC RELATIONS
            for (int i = 0; i < graph.Count; i++)
            {
                GraphVertex tmp = graph[i];
                for (int j = 0; j < tmp.outs.Count; j++)
                {

                    if (tmp.outs[j] == tmp)
                    {
                        tmp.outs.RemoveAt(j);
                        tmp.intensities.RemoveAt(j);
                        tmp.probabilities.RemoveAt(j);
                        tmp.relationState.RemoveAt(j);
                        tmp.finalOutIntensities.RemoveAt(j);
                    }
                }
                for (int j = 0; j < tmp.ins.Count; j++)
                {
                    if (tmp.ins[j] == tmp)
                    {
                        tmp.ins.RemoveAt(j);
                    }
                }
                tmp.settingIns();
            }

        }


        private static void buildEquations()
        {
            int asd = 2;
            double[,] matrix = new double[graph.Count,graph.Count];//for my algorithm or for apaches'
            double[] sSolutions = new double[graph.Count];
            for (int i = 0; i < graph.Count; i++)
            {//line dedicated to i-vertex

                GraphVertex current = graph[i];
                if (i == asd) continue;
                for (int j = 0; j < graph.Count; j++)
                {
                    matrix[i,j] = 0;

                    if (i == j)
                    {
                        double tmp1 = 0;
                        for (int k = 0; k < current.finalOutIntensities.Count; k++)
                        {
                            tmp1 -= current.finalOutIntensities[k];                   //negative outer intensity
                        }
                        //                   System.out.println(tmp1);
                        matrix[i,j] = tmp1;
                    }
                }

                for (int m = 0; m < current.ins.Count; m++)
                {
                    int k = graph.IndexOf(current.ins[m]);//index of cell where i should write inner intensity
                    matrix[i,k] = current.finalInIntensities[m];
                }
                sSolutions[i] = 0;
                //ಠ‿↼
            }
            sSolutions[asd] = 1;        //filling solution array

            for (int i = 0; i < graph.Count; i++)
            {
                matrix[asd,i] = 1;

            }
            //        for (int i = 0; i < matrix.length; i++){
            //            sSolutions[i] *= 1000;
            //            for (int j = 0; j < matrix[0].length; j++){
            //                matrix[i][j] *=1000;
            //            }
            //        }

            //        Console.WriteLine(matrix.length);
            //        Console.WriteLine(matrix[0].length);

            //        p2(matrix);
            //        Console.WriteLine(sSolutions.toString());

            //USING APACHE.COM.MATH LIBRARY
            matrix.Solve(sSolutions, false);
        }


        public static void treeBuilder(int[][] inn)
        {
            GraphVertex vertex = new GraphVertex();
            vertex.state = inn;
            int[][] array = inn.Copy();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][1] == 1)
                {
                    build(i, array, vertex);
                }
            }
            graph.Add(vertex);
        }

        public static void build(int i, int[][] z, GraphVertex graphVertex)
        {
            int[][] l = copyPlease(z);

            List<NobleUnit> transitions = system[i].relationsObjects;

            for (int q = 0; q < transitions.Count; q++)
            {
                int[][] tmp = copyPlease(l);
                //            int [][] tmp = l.clone();
                //            tmp[0][0]=666666666;
                //            System.out.println("L now is ");
                //            printPlease(l);
                int w = system.IndexOf(transitions[q]);

                if (tmp[i][0] == 0)
                {
                    tmp[i] = emptyNode;
                }
                else
                {                                     //FROM
                    tmp[i][0]--;
                }

                if (tmp[w][1] == 0)
                {
                    tmp[w] = occupiedNode;                  //TO
                }
                else
                {
                    //                Console.WriteLine("~~~~~~~~~~~~~~~");
                    //                Console.WriteLine(tmp[w][0]);
                    tmp[w][0]++;
                    //                Console.WriteLine(tmp[w][0]);
                }

                graphVertex.addRelations(tmp);      ///MIGHT BE TROUBLING
                //            graphVertex.addProbabilities(system.get(i).realtionsProbabilities.get(q));
                //            graphVertex.addIntensities(system.get(i).tau);
                Console.WriteLine(system[i].realtionsProbabilities[q]);
                graphVertex.addFinalIntesities(system[i].realtionsProbabilities[q], system[i].tau);


                if (!contains(tmp))
                {
                    resultArray.Add(tmp);
                    treeBuilder(tmp);
                }

            }
        }

       

        private static bool contains(int[][] tmp)
        {
            for (int p = 0; p < resultArray.Count; p++)
            {
                int[][] tmp1 = resultArray[p];
                if (tmp.IsBinaryEqualTo(tmp1))
                {
                    return true;
                }
            }
            return false;
        }





        private static void setInitialState()
        {
            initialState = new int[system.Count][];
            for (int i = 0; i < initialState.Length; i++)
            {
                if (system[i]!=null)
                {
                    int[] tmp1 = { 0, 0, 1 };
                    initialState[i] = tmp1;
                }
                else
                {

                    int[] tmp2 = { system[i].queueSize(), 1, 0 };
                    initialState[i] = tmp2;
                }
            }
        }

        public static void p2(double[][] a)
        {
            Console.WriteLine();
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < a[0].Length; j++)
                {
                    Console.Write(a[i][j] + " ");
                }
            }
            Console.WriteLine();

        }

        public static void printPlease(int[][] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(" <");
                for (int j = 0; j < a[0].Length; j++)
                {
                    Console.Write(a[i][j]);
                }
                Console.Write("");
                Console.Write(">");
            }
            Console.WriteLine();

        }

        public static int[][] copyPlease(int[][] a)
        {
            int[][] r = new int[a.Length][];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    r[i][j] = a[i][j];
                }
            }
            return r;
        }

        public static bool checker(int[][] a)
        {
            //checks if the state is valid
            int taskCounter = 0;
            for (int i = 0; i < system.Count; i++)
            {
                taskCounter += a[i][0] + a[i][1];
                if ((a[i][0] > 0 && a[i][2] > 0) || (a[i][0] > 0 && a[i][1] == 0) || (a[i][1] > 0 && a[i][2] > 0))
                {
                    return false;
                }
            }
            //        System.out.println(taskCounter);
            if (taskCounter != N) return false;
            return true;
        }

    }
}
