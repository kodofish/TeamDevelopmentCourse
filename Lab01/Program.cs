using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using HR.SalaryModule;

namespace Lab02
{
    // 注意，他不是 Main
    public class MainApp
    {
        private readonly ISalaryFormula _SalaryFormula;
        public MainApp(ISalaryFormula SalaryFormula)
        {
            _SalaryFormula = SalaryFormula;
        }

        //注意，這裡不是 Console 程式的進入點 Main
        public void Main()
        {
            //一般員工 SalaryFormula
            SalaryCalculator SC = new SalaryCalculator(_SalaryFormula);

            //注意參數完全相同
            float amount = SC.Calculate(8 * 19, 200, 8);
            Console.WriteLine("\n SalaryFormula--->amount:" + amount);
        }
    }

    //這裡才是真正的進入點 main 的類別 Program
    public class Program
    {
        //這裡才是真正的進入點 main
        static void Main(string[] args)
        {
            //讀取appsettings.json中的設定，決定用哪一個類別實作
            IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false)
              .Build();
            //取得類別名稱
            var myServiceType = config.GetSection("MyServiceConfig").GetValue<string>("MyServiceType");

            //建立 DI Services
            var serviceCollection = new ServiceCollection();
            //取得類別型別
            Type serviceType = Type.GetType(myServiceType);

            //註冊 DI物件 (為了未來的注入)
            serviceCollection.AddTransient<MainApp>();
            //使用設定檔中設定的類別動態作注入
            serviceCollection.AddTransient(typeof(ISalaryFormula), serviceType);

            //建立 DI Provider
            var serviceProvider = serviceCollection.BuildServiceProvider();
            //執行 MainApp，這裡會自動注入 ISalaryFormula
            serviceProvider.GetRequiredService<MainApp>().Main();

            Console.WriteLine("press any key to exit...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 計算薪資的類別
    /// </summary>
    class SalaryCalculator
    {
        /// <summary>
        /// 計算薪資的公式物件
        /// </summary>
        private ISalaryFormula _SalaryFormula;
        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="SalaryFormula"></param>
        public SalaryCalculator(ISalaryFormula SalaryFormula)
        {
            _SalaryFormula = SalaryFormula;
        }
        /// <summary>
        /// 實際計算薪資
        /// </summary>
        /// <param name="WorkHours">工時</param>
        /// <param name="HourlyWage">時薪</param>
        /// <param name="PrivateDayOff">請假天數</param>
        /// <returns></returns>
        public float Calculate(float WorkHours, int HourlyWage, int PrivateDayOffHours)
        {
            return _SalaryFormula.Execute(WorkHours, HourlyWage, PrivateDayOffHours);
        }
    }
}