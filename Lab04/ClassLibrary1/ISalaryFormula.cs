namespace ClassLibrary1
{
    /// <summary>
    /// 計算薪資的公式的介面
    /// </summary>
    public interface ISalaryFormula
    {
        //薪資=工時*時薪-(事假時數*時薪)
        float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours);
    }
}