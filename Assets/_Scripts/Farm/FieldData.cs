using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FieldData : MonoBehaviour
{
    [Header("Field Identity")]
    [SerializeField] private string fieldName = "Field 01";

    [Header("Soil & Status")]
    [SerializeField] private float soilMoisturePercent = 45f;
    [SerializeField] private float nitrogenLevelKgHa = 120f;
    [SerializeField] private float healthPercent = 100f;
    [SerializeField] private float pestRiskPercent = 0f;

    [Header("3D Visuals")]
    [SerializeField] private float rowSpacing = 1.5f;
    [SerializeField] private float plantSpacing = 1.2f;
    [SerializeField] private float edgeMargin = 1f;

    private CropSO activeCrop;
    private float growthProgressPercent;
    private BoxCollider fieldCollider;
    private readonly List<Transform> spawnedCrops = new List<Transform>();
    private Transform spawnedCrop;

    public CropSO ActiveCrop => activeCrop;
    public string FieldName => fieldName;
    public bool HasActiveCrop => activeCrop != null;
    public string CropType => HasActiveCrop ? activeCrop.CropName : "Fallow (Empty)";
    public string GrowthStage => GetGrowthStageLabel();
    public float GrowthProgress => growthProgressPercent;
    public float SoilMoisture => soilMoisturePercent;
    public float NitrogenLevel => nitrogenLevelKgHa;
    public float Health => healthPercent;
    public float PestRisk => pestRiskPercent;

    private void Awake()
    {
        fieldCollider = GetComponent<BoxCollider>();
    }

    public bool TryPlantCrop(CropSO crop)
    {
        if (HasActiveCrop || crop == null)
            return false;

        activeCrop = crop;
        growthProgressPercent = 0f;
        healthPercent = 100f;
        pestRiskPercent = 0f;

        SpawnCropVisual();
        return true;
    }

    public void ClearField()
    {
        activeCrop = null;
        growthProgressPercent = 0f;

        for (int i = 0; i < spawnedCrops.Count; i++)
        {
            if (spawnedCrops[i] != null)
                Destroy(spawnedCrops[i].gameObject);
        }
        spawnedCrops.Clear();
    }

    public void SimulateWeatherStep(WeatherDataPoint weather, float hoursElapsed = 1f)
    {
        if (weather == null) return;

        float evaporation = Mathf.Max(0f, weather.temprature * 0.05f) * (1f - (weather.humidity / 100f)) * hoursElapsed;
        float absorption = weather.precipitation * 1.5f;
        float cropWaterUse = HasActiveCrop ? activeCrop.WaterConsumptionPerHour * hoursElapsed : 0f;

        soilMoisturePercent = Mathf.Clamp(soilMoisturePercent + absorption - evaporation - cropWaterUse, 0f, 100f);

        if (HasActiveCrop)
        {
            nitrogenLevelKgHa = Mathf.Max(0f, nitrogenLevelKgHa - (activeCrop.NitrogenConsumptionPerHour * hoursElapsed));

            if (weather.humidity > 75f && weather.temprature > 18f)
                pestRiskPercent = Mathf.Clamp(pestRiskPercent + (0.5f * hoursElapsed), 0f, 100f);
            else
                pestRiskPercent = Mathf.Clamp(pestRiskPercent - (0.2f * hoursElapsed), 0f, 100f);

            float growthStep = (hoursElapsed / activeCrop.GrowthDurationHours) * 100f;
            growthProgressPercent = Mathf.Clamp(growthProgressPercent + growthStep, 0f, 100f);
            UpdateCropVisualScale();
        }
    }

    private void SpawnCropVisual()
    {
        if (activeCrop.CropPrefab == null)
            return;

        Bounds bounds = fieldCollider.bounds;
        Vector3 centerPos = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        GameObject instance = Instantiate(activeCrop.CropPrefab, centerPos, Quaternion.identity);
        spawnedCrop = instance.transform;

        TestCropBillboard billboard = instance.GetComponentInChildren<TestCropBillboard>();
        if (billboard != null)
            billboard.Initialize(this);

        UpdateCropVisualScale();
    }

    private void UpdateCropVisualScale()
    {
        if (spawnedCrop == null)
            return;

        float scaleFactor = Mathf.Lerp(0.25f, 1f, growthProgressPercent / 100f);
        spawnedCrop.localScale = Vector3.one * scaleFactor;
    }

    private string GetGrowthStageLabel()
    {
        if (!HasActiveCrop) return "None";
        if (growthProgressPercent < 25f) return "Seedling";
        if (growthProgressPercent < 60f) return "Vegetative";
        if (growthProgressPercent < 100f) return "Flowering";
        return "Ready to Harvest";
    }
}