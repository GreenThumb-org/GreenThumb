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
    [SerializeField] private float temperatureC = 22f;
    [SerializeField] private float rainfallMm = 5f;

    public string FieldName => fieldName;
    public string CropType => cropType;
    public string GrowthStage => growthStage;
    public float SoilMoisture => soilMoisturePercent;
    public float NitrogenLevel => nitrogenLevelKgHa;
    public float Health => healthPercent;
    public float PestRisk => pestRiskPercent;
    public float Temperature => temperatureC;
    public float Rainfall => rainfallMm;
}