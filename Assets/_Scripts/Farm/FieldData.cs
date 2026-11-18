using UnityEngine;

public class FieldData : MonoBehaviour
{
    [SerializeField] private string fieldName = "Field 01";
    [SerializeField] private string cropType = "Wheat";
    [SerializeField] private string growthStage = "Seedling";
    [SerializeField] private float soilMoisturePercent = 45f;
    [SerializeField] private float nitrogenLevelKgHa = 120f;
    [SerializeField] private float healthPercent = 95f;
    [SerializeField] private float pestRiskPercent = 10f;

    public string FieldName => fieldName;
    public string CropType => cropType;
    public string GrowthStage => growthStage;
    public float SoilMoisture => soilMoisturePercent;
    public float NitrogenLevel => nitrogenLevelKgHa;
    public float Health => healthPercent;
    public float PestRisk => pestRiskPercent;

    public void SimulateWeatherStep(WeatherDataPoint weather, float hoursElapsed = 1f)
    {
        if (weather == null) return;

        float evaporation = Mathf.Max(0f, weather.temprature * 0.05f) * (1f - (weather.humidity / 100f)) * hoursElapsed;
        float absorption = weather.precipitation * 1.5f;

        soilMoisturePercent = Mathf.Clamp(soilMoisturePercent + absorption - evaporation, 0f, 100f);

        if (weather.humidity > 75f && weather.temprature > 18f)
            pestRiskPercent = Mathf.Clamp(pestRiskPercent + (0.5f * hoursElapsed), 0f, 100f);
        else
            pestRiskPercent = Mathf.Clamp(pestRiskPercent - (0.2f * hoursElapsed), 0f, 100f);
    }
}