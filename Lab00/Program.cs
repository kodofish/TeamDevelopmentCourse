namespace Lab00
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SalaryCalculator salaryCalculator = new SalaryCalculator();
            float amount = salaryCalculator.Calculate(8 * 19, 200, 8);
            
            Console.Write("\nSalaryFormula--->amount:" + amount);
            
            //老闆 BossSalaryFormula
            salaryCalculator = new SalaryCalculator(new BossSalaryFormula());
            //注意參數完全相同
            amount = salaryCalculator.Calculate(8 * 19, 200, 8); //即便與員工相同
            //但計算出的結果不同
            Console.Write("\nBoss SalaryFormula--->amount:" + amount);
            
            Console.WriteLine("\n任意按一鍵繼續...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// 計算物件
    /// </summary>
    class SalaryCalculator
    {

        private readonly ISalaryFormula _SalaryFormula;

        public SalaryCalculator()
        {
            _SalaryFormula = new SalaryFormula();
        }
        public SalaryCalculator(ISalaryFormula salaryFormula)
        {
            _SalaryFormula = salaryFormula;
        }

        public float Calculate(float WorkHours, int HourlyWage, int PrivateDayOffHours)
        {
            //薪資=工時*時薪-(事假時數*時薪)
            return _SalaryFormula.Execute(WorkHours, HourlyWage, PrivateDayOffHours);
        }
    }

    internal interface ISalaryFormula
    {
        /// <summary>
        /// 實際計算薪資
        /// </summary>
        /// <param name="WorkHours"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="PrivateDayOffHours"></param>
        /// <returns></returns>
        float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours);
    }

    /// <summary>
    /// 計算公式
    /// </summary>
    class SalaryFormula : ISalaryFormula
    {
        /// <summary>
        /// 實際計算薪資
        /// </summary>
        /// <param name="WorkHours"></param>
        /// <param name="HourlyWage"></param>
        /// <param name="PrivateDayOffHours"></param>
        /// <returns></returns>
        public float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours)
        {
            //薪資=工時*時薪-(事假時數*時薪)
            return WorkHours * HourlyWage - (PrivateDayOffHours * HourlyWage);
        }
    }
    
    public class BossSalaryFormula : ISalaryFormula
    {
        public float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours)
        {
            //老闆請假不扣薪!!!!!!!
            return WorkHours * HourlyWage - (PrivateDayOffHours * HourlyWage * 0);
        }
    }
}