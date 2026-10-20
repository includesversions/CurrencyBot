using System.Globalization;
using System.Text;
using Telegram.Bot;
using CurrencyBot.Api;

WelcomeMessage("Started Bot");

TelegramBotClient telegramBot = new(Environment.GetEnvironmentVariable("BOT-TOKEN"));
IranMarket iranMarket = new IranMarket();

while (true)
{
    CurrencyResponse? current = await iranMarket.GetCurrency();

    string res = ConvertCurrencyText(current ?? new());

    await telegramBot.SendMessage(Environment.GetEnvironmentVariable("CHANNEL_LINK"), $"{res}"); // Don't forget to add an '@' symbol before the channel link.
    await Task.Delay(TimeSpan.FromMinutes(29));
}



string ConvertCurrencyText(CurrencyResponse currencyResponse)
{

    StringBuilder text = new($"{GetNow()}");

    string[] keys = ["USD_IRR_FREE", "EUR_IRR_FREE", "GBP_IRR_FREE", "GOLD_18K_IRR", "COIN_EMAMI_IRR", "USDT_IRR"];

    foreach (string key in keys)
    {
        Currency currency = currencyResponse?.Data.Find(n => n.Symbol == key) ?? throw new NullReferenceException();

        string change = currency.Change > 0 ? $"🔺 %{currency.ChangePct!.Value.ToString(CultureInfo.InvariantCulture)}+" : $"🔻 %{currency.ChangePct!.Value.ToString(CultureInfo.InvariantCulture)}-";
        text.AppendLine($"{ConvertShortName(currency.NameFa)}: {currency.Price}تومان, {change}");
    }

    return text.ToString();
}
string GetNow() => $"-- {DateTime.Now.ToString("yyy/MM/dd | h:mm", new CultureInfo("fa"))} --\n";
void WelcomeMessage(string text)
{
string equals = new('=', 5);
string startedMessage = $"{equals} {text} {equals}";
Console.WriteLine(startedMessage);
Console.WriteLine($"{new('_', startedMessage.Length)}");
}
string ConvertShortName(string name) => name switch
{
    "دلار آمریکا (بازار آزاد)" => "دلار",
    "یورو (بازار آزاد)" => "یورو",
    "پوند انگلیس (بازار آزاد)" => "پوند",
    "طلای ۱۸ عیار (هر گرم)" => "طلا 18 عیار",
    "تتر (ریالی)" => "تتر",
    _ => name
};
