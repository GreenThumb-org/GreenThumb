using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoilType
{
    ClayLoam,
    SandyLoam,
    SiltLoam,
    HeavyClay
}

[Serializable]
public struct CropGrowthStage
{
    public string stageName;
    [Range(0f, 100f)] public float minProgressPercent;
    public float waterDemandMultiplier;
    public float nitrogenDemandMultiplier;
}

[CreateAssetMenu(fileName = "NewCrop", menuName = "Greenthumb/Crop Data")]
public class CropSO : ScriptableObject
{
    [Header("Identity & Visuals")]
    [SerializeField] private string cropName = "Wheat";
    [SerializeField] private GameObject cropPrefab;

    [Header("Planting & Soil Suitability")]
    [SerializeField] private SoilType preferredSoil = SoilType.ClayLoam;
    [SerializeField] private float minPlantingTempC = 5f;
    [SerializeField] private Vector2 optimalMoistureRange = new Vector2(40f, 65f);
    [SerializeField] private float requiredNitrogenKgHa = 120f;

    [Header("Growth & GDD")]
    [SerializeField] private float baseTempC = 4f;
    [SerializeField] private float requiredGDD = 1500f;
    [SerializeField] private List<CropGrowthStage> growthStages = new List<CropGrowthStage>();

    [Header("Resource Consumption (Base / Hour)")]
    [SerializeField] private float waterConsumptionPerHour = 0.20f;
    [SerializeField] private float nitrogenConsumptionPerHour = 0.25f;

    [Header("Stress Thresholds & Yield")]
    [SerializeField] private string climateType = "Temperate";
    [SerializeField] private Vector2 optimalTempRangeC = new Vector2(12f, 22f);
    [SerializeField] private float frostLethalTempC = -4f;
    [SerializeField] private float heatStressTempC = 30f;
    [SerializeField] private float stressHealthPenaltyPerHour = 1.5f;
    [SerializeField] private float baseYieldKgHa = 6500f;

    [TextArea(2, 4)]
    [SerializeField] private string growingRequirements;

    public string CropName => cropName;
    public GameObject CropPrefab => cropPrefab;
    public SoilType PreferredSoil => preferredSoil;
    public float MinPlantingTempC => minPlantingTempC;
    public Vector2 OptimalMoistureRange => optimalMoistureRange;
    public float RequiredNitrogenKgHa => requiredNitrogenKgHa;
    public float BaseTempC => baseTempC;
    public float RequiredGDD => requiredGDD;
    public IReadOnlyList<CropGrowthStage> GrowthStages => growthStages;
    public float WaterConsumptionPerHour => waterConsumptionPerHour;
    public float NitrogenConsumptionPerHour => nitrogenConsumptionPerHour;
    public string ClimateType => climateType;
    public Vector2 OptimalTempRangeC => optimalTempRangeC;
    public float FrostLethalTempC => frostLethalTempC;
    public float HeatStressTempC => heatStressTempC;
    public float StressHealthPenaltyPerHour => stressHealthPenaltyPerHour;
    public float BaseYieldKgHa => baseYieldKgHa;

    public CropGrowthStage GetCurrentStage(float progressPercent)
    {
        if (growthStages == null || growthStages.Count == 0)
            return new CropGrowthStage { stageName = progressPercent >= 100f ? "Mature" : "Growing", waterDemandMultiplier = 1f, nitrogenDemandMultiplier = 1f };

        CropGrowthStage currentStage = growthStages[0];
        for (int i = 0; i < growthStages.Count; i++)
            if (progressPercent >= growthStages[i].minProgressPercent)
                currentStage = growthStages[i];

        return currentStage;
    }

    public string GetStageName(float progressPercent)
    {
        return GetCurrentStage(progressPercent).stageName;
    }

    public string GetFormattedRequirements()
    {
        return $"Climate: {climateType} ({optimalTempRangeC.x:F0}–{optimalTempRangeC.y:F0} °C, Min Sow: {minPlantingTempC:F0} °C)\n" + $"Soil: {preferredSoil} | Moisture: {optimalMoistureRange.x:F0}–{optimalMoistureRange.y:F0}% | Min N: {requiredNitrogenKgHa:F0} kg/ha\n" + $"Maturity: {requiredGDD:F0} GDD | Est. Yield: {baseYieldKgHa:F0} kg/ha\n" + $"Notes: {growingRequirements}";
    }
}