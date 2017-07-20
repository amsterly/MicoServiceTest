using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPRequestConsole
{
   public  class TaskTest
    {
       public  static void doTask(string[] args)
        {

            //1000000000这个数字会抛出System.AggregateException

            Task<Int32> t = new Task<Int32>(n => Sum((Int32)n), 1000000000);

            //可以现在开始，也可以以后开始 

            t.Start();

            //Wait显式的等待一个线程完成

            t.Wait();

            Console.WriteLine("The Sum is:" + t.Result);
        }

        private static Int32 Sum(Int32 i)
        {
            Int32 sum = 0;
            for (; i > 0; i--)
                checked { sum += i; }
            return sum;
        }

        public static void testWaits()
        {
            createATask();
            createBTask();
        }
        private static void createATask()
        {
            //创建task并运行
            Task<string>task = Task.Factory.StartNew<string>(() =>
            {
                Console.WriteLine("A Task is running!"); Thread.Sleep(3000);
                return "A Task done";
            });
           
            Console.WriteLine(task.Result);
            Task.WaitAny(task);
        }

        private static void createBTask()
        {
            //创建task手动运行
            Task<string> task = new Task<string>(() =>
            {
                Console.WriteLine("B Task is running!"); Thread.Sleep(3000);
                return "B Task done";
            });
            task.Start();
   
            Console.WriteLine(task.Result);
            Task.WaitAny(task);
        }
    }

}
