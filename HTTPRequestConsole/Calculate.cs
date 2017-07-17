using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTPRequestConsole
{
    public class Calculate
    {
        public Thread threadAdd;
        public Thread threadSub;
        public Calculate()
        {
            threadAdd = new Thread(new ThreadStart(Add));
            threadSub = new Thread(new ThreadStart(Sub));
        }

        //加法运算
        public void Add()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("进入加法计算");
            Thread.Sleep(6000);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("加法运算结果: x={0} y={1} x+y={2}", 1, 2, 1 + 2);
        }

        //新增减法运算 
        public void Sub()
        {
            //主要是这里
            bool b = threadAdd.Join(2000);
            //这里等待ADD线程2秒 如果ADD执行完毕 则返回true 如果ADD未执行完毕返回false
            Console.ForegroundColor = ConsoleColor.Red;
            if (b)
            {
                Console.WriteLine("加法运算已经完成,进入减法法计算");
            }
            else
            {
                Console.WriteLine("加法运算超时,先进入减法法计算");
            }

            //Thread.Sleep(2000);
            Console.WriteLine("进入减法运算");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("减法运算结果: x={0} y={1} x-y={2}", 10, 2, 10 - 2);
        }
    }
}
