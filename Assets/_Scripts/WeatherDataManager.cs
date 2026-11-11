using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeatherDataManager : MonoBehaviour
{
    [SerializeField]
    public WeatherDataPoint[] WeatherDataPoints;

    [SerializeField]
    [Tooltip("FileName needs to be without .csv ending")]
    private string DataFileName = "";

    [SerializeField]
    [Tooltip("The year that the weather data gets pulled from")]
    private int SelectedYear = 2010;

    public static WeatherDataManager Manager { get; private set; }

    void Awake()
    {
        if (Manager != null && Manager != this)
        {
            Destroy(gameObject);
            return;
        }
        Manager = this;
    }
    void Start()
    {
        GetWeatherData getWeatherData = new GetWeatherData();
        getWeatherData.GetData(DataFileName, SelectedYear);
        Debug.Log("<------>");
        Debug.Log("Time: " + Manager.WeatherDataPoints[0].time);
        Debug.Log("Temp: " + Manager.WeatherDataPoints[0].temprature);
        Debug.Log("Prec: " + Manager.WeatherDataPoints[0].precipitation);
        Debug.Log("Weather: " + Manager.WeatherDataPoints[0].weather);
    }
}
