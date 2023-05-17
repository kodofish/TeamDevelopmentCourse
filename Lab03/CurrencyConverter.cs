namespace Lab03
{
    public interface ICurrencyConverter
    {
        float Convert(string From, string To);
    }

    public class CurrencyConverter : ICurrencyConverter
    {
        public float Convert(string From, string To)
        {
            HttpClient hc = new HttpClient();
            var ret = hc.GetAsync("https://free.currconv.com/api/v7/convert?q=USD_TWD&compact=ultra&apiKey=54bbaef1017ad8e12f43").Result;
            var JSON = ret.Content.ReadAsStringAsync().Result;

            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(JSON);

            return data.USD_TWD;
        }

    }

    public class Financy
        {
            private readonly ICurrencyConverter _currencyConverter;

            public Financy(ICurrencyConverter currencyConverter)
            {
                _currencyConverter = currencyConverter;
            }
            public double USD2TWD(double amount)
            {
                
                double rate = _currencyConverter.Convert("USD", "TWD");

                return amount * rate;
            }

            public double SplitMoney(double USDAmount, int People)
            {
                
                //使用到外部函式(抓取匯率)
                double rate = _currencyConverter.Convert("USD", "TWD");
                //計算台幣總金額
                double Total = USDAmount * rate;

                //回傳一個人需要付多少錢(台幣)
                return Total / People;
            }
        }
    }