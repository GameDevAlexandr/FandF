
using System;
using System.Globalization;

public static class MyString
{
    private static string[] _unit = new string[] { "", "k", "m", "b", "t", "aa", "ab", "ac", "ad", 
        "ae", "af", "ag", "ah", "ai", "aj","ak","al","am","an","ao","ap", "aq","ar","as","at","au",
        "av","aw","ax","ay","az"};
    public static string Suffixation(double value)
    {
        string result = "";
        int nCount = value.ToString("F").Length - 4;
        int period = nCount / 3;
        if (period > 0 && period < _unit.Length)
        {
            result = (value / System.Math.Pow(10, period * 3)).ToString("0.00", CultureInfo.GetCultureInfo("en-US")) + _unit[period];
        }
        else
        {
            result = value.ToString();
        }
        return result;
    }
    public static string GetHMS(double data)
    {
        TimeSpan ts = TimeSpan.FromSeconds(data);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
    }
    public static string GetMS(double data)
    {
        TimeSpan ts = TimeSpan.FromSeconds(data);
        return string.Format("{0:D2}:{1:D2}", ts.Minutes, ts.Seconds);
    }
}
