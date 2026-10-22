using System.Globalization;
using System.Diagnostics;
using System.Text;
using Telegram.Bot;
using CurrencyBot.Api;
using CurrencyBot;

WelcomeMessage("Started Bot");

TelegramBotClient telegramBot = new(Environment.GetEnvironmentVariable("BOT-TOKEN")!);
IranMarket iranMarket = new IranMarket();

Cache cache = new();

while (true)
{
    CurrencyResponse? current = await iranMarket.GetCurrency();
    if (current == null) continue;

    CacheBalancer(current);

    string res = ConvertCurrencyText(cache.Data ?? new());
    Console.WriteLine("Get currency.");
    try
    {
        await telegramBot.SendMessage(Environment.GetEnvironmentVariable("CHANNEL_LINK")!, $"{res}"); // Don't forget to add an '@' symbol before the channel link.
    }
    catch { await Task.Delay(5000); continue; }

    await Task.Delay(TimeSpan.FromMinutes(59));
}

string ConvertCurrencyText(List<Currency> data)
{
    string[] keys = ["USD_IRR_FREE", "EUR_IRR_FREE", "GBP_IRR_FREE", "GOLD_18K_IRR", "COIN_EMAMI_IRR", "USDT_IRR"];

    StringBuilder text = new("📊 گزارش لحظه‌ای بازار ارز و طلا");

    text.AppendLine(Environment.NewLine);
    text.AppendLine(ConvertDateTimeToText());

    foreach (string key in keys)
    {
        Currency currency = data.Find(c => c.Symbol == key) ?? throw new NullReferenceException();

        string change = AverageCalculation(currency.Change, currency.ChangePct);
        text.AppendLine($"{ConvertShortName(currency.NameFa)}: {currency.Price:#,0}تومان, {change}");
    }   

    text.AppendLine(Environment.NewLine);
    text.AppendLine("━");

    return text.ToString();
}

string AverageCalculation(decimal? average, decimal? pct) => average switch
{
    0 => "بدون تغییر",
    > 0 => $"🔺 %{pct!.Value.ToString(CultureInfo.InvariantCulture)}+",
    < 0 => $"🔻 %{pct!.Value.ToString(CultureInfo.InvariantCulture)}-",
    _ => ""
};

void CacheBalancer(CurrencyResponse currency)
{
    if (cache.Data == null)
    {
        cache = new() { Data = currency.Data };
        return;
    }

    for (int counter = 0; counter < currency.Data.Count; counter++)
    {
        currency.Data[counter].Change = currency.Data[counter].Price - cache.Data[counter].Price;
        currency.Data[counter].ChangePct = cache.Data[counter].Price == 0 ? 0 : currency.Data[counter].Change / cache.Data[counter].Price * 100;
    }

    cache = new() { Data = currency.Data };
}

string ConvertDateTimeToText()
{
    TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
        OperatingSystem.IsWindows() ? "Iran Standard Time" : "Asia/Tehran"
    );

    DateTime tehranNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tehranTimeZone);

    StringBuilder text = new();
    text.AppendLine($"📅 {ConvertWeekdayes(tehranNow.DayOfWeek)} | {tehranNow.ToString("dd MMM yyyy", new CultureInfo("fa-IR"))}");
    text.AppendLine($"🕘 ساعت {tehranNow:HH:mm}");

    return text.ToString();
}

void WelcomeMessage(string text)
{
    string equals = new('=', 5);
    string startedMessage = $"{equals} {text} {equals}";
    Console.WriteLine(startedMessage);
    Console.WriteLine($"{new('_', startedMessage.Length)}");
}
string ConvertShortName(string name) => name switch
{
    "دلار آمریکا (بازار آزاد)" => "💵دلار",
    "یورو (بازار آزاد)" => "💶یورو",
    "پوند انگلیس (بازار آزاد)" => "💷پوند",
    "طلای ۱۸ عیار (هر گرم)" => "🥇طلای 18 عیار",
    "تتر (ریالی)" => "💲تتر",
    _ => $"💰{name}"
};

string ConvertWeekdayes(DayOfWeek day) => day switch
{
    DayOfWeek.Saturday => "شنبه",
    DayOfWeek.Sunday => "یکشنبه",
    DayOfWeek.Monday => "دوشنبه",
    DayOfWeek.Tuesday => "سه‌شنبه",
    DayOfWeek.Wednesday => "چهارشنبه",
    DayOfWeek.Thursday => "پنج‌شنبه",
    DayOfWeek.Friday => "جمعه",
    _ => day.ToString()
};