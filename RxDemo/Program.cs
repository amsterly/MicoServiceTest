using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace RxDemo
{
    //nuget :Install-Package System.Reactive -Version 3.0.0
    //.NETFramework 4.5  
    //System.Reactive.Core (>= 3.0.0)
    //System.Reactive.Interfaces (>= 3.0.0)
    //System.Reactive.Linq (>= 3.0.0)
    //System.Reactive.PlatformServices (>= 3.0.0)
    //System.Reactive.Windows.Threading (>= 3.0.0)以上使用

    class Program
    {
        static void Main(string[] args)
        {
            // Observable对象  
            Observable.Range(1, 5).Subscribe(x =>  Console.WriteLine(x));
            Console.Read();
        }
    }
}
