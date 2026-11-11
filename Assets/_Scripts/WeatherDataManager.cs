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
    public WeatherDataPoint CurrentDataPoint;
    private void Awake()
    {
        if (Manager != null && Manager != this)
        {
            Destroy(gameObject);
        }
        Manager = this;
        DontDestroyOnLoad(Manager);
    }
    private void Start()
    {
        GetWeatherData getWeatherData = new GetWeatherData();
        getWeatherData.GetData(DataFileName, SelectedYear);
        CurrentDataPoint = Manager.WeatherDataPoints[0];
        StartCoroutine(TimeManager.Manager.StartTime());
    }
    public void PrintCurrentDataPoint()
    {
        Debug.Log("Time: " + CurrentDataPoint.time);
        Debug.Log("Temp: " + CurrentDataPoint.temprature);
        Debug.Log("Precipitation: " + CurrentDataPoint.precipitation);
        Debug.Log("Weather: " + CurrentDataPoint.weather);
        Debug.Log("Humidity: " + CurrentDataPoint.humidity);
    }
}
