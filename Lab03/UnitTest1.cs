using NSubstitute;
using Shouldly;

namespace Lab03;

public class FinanceTests
{
    [Test]
    public void SplitMoney()
    {
        var converter = NSubstitute.Substitute.For<ICurrencyConverter>();
        converter.Convert("USD", "TWD").Returns(30);
        var finance = new Financy(converter);
        var actual = finance.SplitMoney(10, 10);
        
        actual.ShouldBe(30);
    }
}