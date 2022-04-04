using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Map
    {
        private int[,] matrix = new int[7, 7];
        private int[] rowConsts = new int[6];
        private int[] colConsts = new int[6];
        private int[] coords = new int[3];
        private int[] trueCoords = new int[2];
        private string way = "";
        //private int bottomLine = 0;

        public Map(int[,] matrix)
        {
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                this.matrix[0, i] = i;
                this.matrix[i, 0] = i;
            }
            for (int i = 1; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 1; j < this.matrix.GetLength(0); j++)
                {
                    this.matrix[i, j] = matrix[i-1, j-1];
                }
                Console.WriteLine();
            }
        }

        public int[,] Matrix
        {
            get { return matrix; }
            set { this.matrix = value; }
        }

        private int[] GetMinInRow()
        {
            int[] result = new int[matrix.GetLength(0)];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                int min = Int32.MaxValue;
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < min && matrix[i, j] != -1) min = matrix[i, j];
                }
                result[i]= min;
            }
            return result;
        }
        private int[] GetMinInCol()
        {
            int[] result = new int[matrix.GetLength(0)];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                int min = Int32.MaxValue;
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[j, i] < min && matrix[j, i] != -1) min = matrix[j, i];
                }
                result[i] = min;
            }
            return result;
        }

        private void DecInRow()
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] != -1) matrix[i, j] -= rowConsts[i];
                }
            }
        }

        private void DecInCol()
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[j, i] != -1) matrix[j, i] -= colConsts[i];
                }
            }
        }

        private int[] GetCoords()
        {
            int[] result = new int[3];
            int max = -1;
            List<int[]> subsets = new List<int[]>();
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        matrix[i, j] = -1;
                        int[] thisRowConsts = GetMinInRow();
                        int[] thisColConsts = GetMinInCol();
                        if (thisRowConsts[i] + thisColConsts[j] >= max)
                        {
                            max = thisRowConsts[i] + thisColConsts[j];
                            result[0] = i;
                            result[1] = j;
                            result[2] = thisRowConsts.Sum() + thisColConsts.Sum();
                            trueCoords[0] = matrix[0, j];
                            trueCoords[1] = matrix[i, 0];
                            subsets.Add(result);
                        }
                        matrix[i, j] = 0;
                    }    
                }
            }
            max = subsets[0][2];
            foreach(int[] subset in subsets)
            {
                if (subset[2]>max)
                {
                    result[0] = subset[0];
                    result[1] = subset[1];
                    result[2] = subset[2];
                }
            }
            matrix[result[0], result[1]] = -1;
            matrix[result[1], result[0]] = -1;

            Console.WriteLine(result[0]+" "+result[1]+" "+result[2]);
            subsets.Clear();
            return result;
        }

        private int[,] DeleteByCoords(int[] coords)
        {
            int[,] result = new int[matrix.GetLength(0)-1, matrix.GetLength(1) - 1];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                 for (int j = 0; j < result.GetLength(1); j++)
                 {
                    if (coords[1] > j && coords[0] > i)
                        result[i, j] = matrix[i, j];
                    else if (coords[1] > j && coords[0] <= i)
                        result[i, j] = matrix[i+1, j];
                    else if (coords[1] <= j && coords[0] > i)
                        result[i, j] = matrix[i, j+1];
                    else if (coords[1] <= j && coords[0] <= i)
                        result[i, j] = matrix[i + 1, j+1];
                }
            }
            

            return result;
        }
        public void Reduction()
        {

            Print(matrix);

            rowConsts = GetMinInRow();
            
            DecInRow();
            Console.WriteLine("--------------");
            Print(matrix);

            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + trueCoords[1] + ";" + trueCoords[0] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);
            Console.WriteLine(way);
            rowConsts = GetMinInRow();

            DecInRow();
            Console.WriteLine("--------------");
            Print(matrix);

            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + trueCoords[1] + ";" + trueCoords[0] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);
            Console.WriteLine(way);

            Console.WriteLine("--------------");
            rowConsts = GetMinInRow();
            DecInRow();
            Print(matrix);
            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + coords[0] + ";" + coords[1] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);
            Console.WriteLine(way);

            Console.WriteLine("--------------");
            rowConsts = GetMinInRow();
            DecInRow();
            Print(matrix);
            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + coords[0] + ";" + coords[1] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);
            Console.WriteLine(way);

            Console.WriteLine("--------------");
            rowConsts = GetMinInRow();
            DecInRow();
            Print(matrix);
            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + coords[0] + ";" + coords[1] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);
            Console.WriteLine(way);
            /*
            Console.WriteLine("--------------");
            rowConsts = GetMinInRow();
            DecInRow();
            Print(matrix);
            colConsts = GetMinInCol();
            Console.WriteLine();
            DecInCol();
            Console.WriteLine();
            Print(matrix);
            coords = GetCoords();
            Print(matrix);
            Console.WriteLine();
            //coords = GetCoords();
            way += "(" + coords[0] + ";" + coords[1] + ")";
            matrix = DeleteByCoords(coords);
            Print(matrix);*/
            Console.WriteLine(way);
        }

        public void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i,j]!=-1)
                    Console.Write("+"+matrix[i, j]+" ");
                    else
                        Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
