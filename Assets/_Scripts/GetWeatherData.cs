using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Globalization;


public class GetWeatherData
{
    private List<WeatherDataPoint> DataPoints = new List<WeatherDataPoint>();
    public void GetData(string FileName, int SelectedYear)
    {
        FilterWeatherData(GetDataFromFile(FileName), SelectedYear);
    }
    private TextAsset GetDataFromFile(string FileName)
    {
        TextAsset FileData = new TextAsset();
        try
        {
            FileData = Resources.Load<TextAsset>("WeatherData\\" + FileName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error While Reading File:\n" + e);
        }



        return FileData;
    }
    private void FilterWeatherData(TextAsset WeatherData, int Year)
    {
        string[] DatapointsUnfiltered = WeatherData.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < DatapointsUnfiltered.Length; i++)
        {
            if (ContainsCorrectYear(DatapointsUnfiltered[i], Year))
            {
                WeatherDataPoint dataPoint = new WeatherDataPoint();
                string pattern = @"<time=([^>]+)>,<temprature=([^>]+)>,<precipitation=([^>]+)>,<weather=([^>]+)>";

                Match match = Regex.Match(DatapointsUnfiltered[i], pattern);
                if (match.Success)
                {
                    dataPoint.time = match.Groups[1].Value;
                    dataPoint.temprature = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                    dataPoint.precipitation = float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                    dataPoint.weather = match.Groups[4].Value;
                }
                DataPoints.Add(dataPoint);
            }
        }
        WeatherDataManager.Manager.WeatherDataPoints = DataPoints.ToArray();
    }
    private bool ContainsCorrectYear(string DataPoint, int Year)
    {
        if (string.IsNullOrEmpty(DataPoint)) return false;
        string pattern = $@"<time={Year}-";
        return Regex.IsMatch(DataPoint, pattern);
    }
}
