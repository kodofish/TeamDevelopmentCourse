namespace ClassLibrary1;

public class MySalaryFormula : ISalaryFormula
{
    public float Execute(float WorkHours, int HourlyWage, int PrivateDayOffHours)
    {
        //老闆請假不扣薪!!!!!!!
        return WorkHours * HourlyWage - (PrivateDayOffHours * HourlyWage * 0);
    }
}