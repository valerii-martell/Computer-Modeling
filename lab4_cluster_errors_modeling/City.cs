using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class City
    {
        int n = 6;
        int i = 0;
        int s = 0;
        int min = Int32.MaxValue;
        int count = 1;
        bool found;        //n-количество городов
                                   //i-счетчик
                                   //s-текущая сумма
                                   //min-минимальная сумма
                                   //count-счетчик пройденных городов
                                   //found-найден ли город
        int[,] matrix = new int[6, 6];                //матрица рассояний
        int[] m = new int[6];
        int[] minm = new int[6];            //m-текущий путь
                                            //minm-минимальный путь

        public City(int[,] matrix)                    //ввод данных
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j++)
                    this.matrix[i, j] = matrix[i, j];                //считали матрицу расстояний
        }
        public void output()                    //вывод данных
        {
            if (found)                        //если найден маршрут...
            {
                Console.WriteLine("Lenght of min path = ", min);
                Console.WriteLine("Path : ");
                int c = 1;                    //номер в порядке обхода городов
                for (int i = 1; i <= n; i++)      //пробегаем по всем городам
                {
                    int j = 1;
                    while ((j <= n) &&                //ищем следующий город в порядке обхода    
                               (minm[j] != c)) j++;
                    Console.Write(j+"->");
                    c++;
                }
                Console.WriteLine(minm[1]);    //обход завершается первым городом
            }
            else Console.WriteLine("Path not found!");
        }
        public void search(int x)                //поиск следующего города в порядке 
                                                 //обхода после города с номером Х
        {
            if ((count == n) &&                //если просмотрели все города
                (matrix[x, 1] != 0) &&                //из последнего города есть путь в первый город
                (s + matrix[x, 1] < min))            //новая сумма расстояний меньше минимальной суммы
            {
                found = true;                    //маршрут найден
                min = s + matrix[x, 1];                //изменяем: новая минимальная сумма расстояний
                for (int i = 1; i < matrix.GetLength(0); i++) minm[i] = m[i];//изменяем: новый минимальный путь
            }
            else
            {
                for (int i = 1; i < matrix.GetLength(0); i++)     //из текущего города просматриваем все города
                    if ((i != x) &&                //новый город не совпадает с текущим    
                        (matrix[x, i] != 0) &&            //есть прямой путь из x в i
                            (m[i] == 0) &&            //новый город еще не простотрен
                            (s + matrix[x, i] < min))    //текущая сумма не превышает минимальной
                    {
                        s += matrix[x, i];                //наращиваем сумму
                        count++;                //количество просмотренных городав
                        m[i] = count;                //отмечаем у нового города новый номер в порядке обхода
                        search(i);                //поиск нового города начиная с города i
                        m[i] = 0;                    //возвращаем все назад
                        count--;                //-"-
                        s -= matrix[x, i];                //-"-
                    }
            }
        }
        public void main()
        {
            s = 0;
            for (int i = 1; i < matrix.GetLength(0); i++) m[i] = 0;
            count = 1;
            m[1] = count;                        //считаем что поиск начинается с первого города
            search(1);                            //запуск основного алгоритма
            output();                        //вывод результатов
        }
    }
}
