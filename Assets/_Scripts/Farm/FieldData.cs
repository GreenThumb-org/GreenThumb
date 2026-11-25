using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FieldData : MonoBehaviour, IInteractable
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

    [Header("Soil Preparation")]
    [SerializeField] private MeshRenderer groundRenderer;
    [SerializeField] private Material unpreparedMaterial;
    [SerializeField] private Material preparedMaterial;
    [SerializeField] private float preparationDuration = 1.5f;

    private CropSO activeCrop;
    private float growthProgressPercent;
    private BoxCollider fieldCollider;
    private readonly List<Transform> spawnedCrops = new List<Transform>();
    private Transform spawnedCrop;
    private bool isPrepared;


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

    public string TargetName => fieldName;
    public FieldData ParentField => this;
    public bool IsPrepared => isPrepared;

    private void Awake()
    {
        fieldCollider = GetComponent<BoxCollider>();
        if (groundRenderer == null)
            groundRenderer = GetComponent<MeshRenderer>();

        UpdateGroundVisual();
    }

    public bool TryPrepareField()
    {
        if (HasActiveCrop || isPrepared)
            return false;

        isPrepared = true;
        UpdateGroundVisual();
        return true;
    }

    public bool TryPlantCrop(CropSO crop)
    {
        if (!isPrepared || HasActiveCrop || crop == null)
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
        isPrepared = false;
        UpdateGroundVisual();

        if (spawnedCrop != null)
            Destroy(spawnedCrop.gameObject);
    }

    private void UpdateGroundVisual()
    {
        if (groundRenderer == null)
            return;

        Material targetMat = isPrepared ? preparedMaterial : unpreparedMaterial;
        if (targetMat != null)
            groundRenderer.sharedMaterial = targetMat;
    }

    public float GetInteractionDuration(HotbarItemSO equippedItem)
    {
        if (equippedItem != null && equippedItem.ToolType == ToolType.Spade && !HasActiveCrop && !isPrepared)
            return preparationDuration;

        return 0f;
    }

    public void SimulateWeatherStep(WeatherDataPoint weather, float hoursElapsed = 1f)
    {
        if (weather == null)
            return;

        CropGrowthStage currentStage = HasActiveCrop ? activeCrop.GetCurrentStage(growthProgressPercent) : default;
        float waterMultiplier = HasActiveCrop ? currentStage.waterDemandMultiplier : 0f;
        float nitrogenMultiplier = HasActiveCrop ? currentStage.nitrogenDemandMultiplier : 0f;

        float evaporation = Mathf.Max(0f, weather.temprature * 0.05f) * (1f - (weather.humidity / 100f)) * hoursElapsed;
        float absorption = weather.precipitation * 1.5f;
        float cropWaterUse = HasActiveCrop ? activeCrop.WaterConsumptionPerHour * waterMultiplier * hoursElapsed : 0f;

        soilMoisturePercent = Mathf.Clamp(soilMoisturePercent + absorption - evaporation - cropWaterUse, 0f, 100f);

        if (!HasActiveCrop)
            return;

        nitrogenLevelKgHa = Mathf.Max(0f, nitrogenLevelKgHa - (activeCrop.NitrogenConsumptionPerHour * nitrogenMultiplier * hoursElapsed));

        if (weather.humidity > 75f && weather.temprature > 18f)
            pestRiskPercent = Mathf.Clamp(pestRiskPercent + (0.5f * hoursElapsed), 0f, 100f);
        else
            pestRiskPercent = Mathf.Clamp(pestRiskPercent - (0.2f * hoursElapsed), 0f, 100f);

        ApplyStress(weather.temprature, hoursElapsed);

        float stressGrowthFactor = healthPercent <= 0f ? 0f : Mathf.Lerp(0.35f, 1f, healthPercent / 100f);
        float gddEarned = Mathf.Max(0f, weather.temprature - activeCrop.BaseTempC) * (hoursElapsed / 24f);
        float growthStep = (gddEarned / activeCrop.RequiredGDD) * 100f * stressGrowthFactor;
        growthProgressPercent = Mathf.Clamp(growthProgressPercent + growthStep, 0f, 100f);

        UpdateCropVisualScale();
    }

    private void ApplyStress(float currentTempC, float hoursElapsed)
    {
        if (currentTempC <= activeCrop.FrostLethalTempC)
        {
            healthPercent = 0f;
            return;
        }

        bool isStressed = currentTempC > activeCrop.HeatStressTempC || soilMoisturePercent < activeCrop.OptimalMoistureRange.x || soilMoisturePercent > activeCrop.OptimalMoistureRange.y || nitrogenLevelKgHa < activeCrop.RequiredNitrogenKgHa * 0.25f;

        if (isStressed)
            healthPercent = Mathf.Clamp(healthPercent - (activeCrop.StressHealthPenaltyPerHour * hoursElapsed), 0f, 100f);
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
        if (!HasActiveCrop)
            return "None";

        return activeCrop.GetStageName(growthProgressPercent);
    }

    public bool CanInteract(HotbarItemSO equippedItem)
    {
        if (equippedItem == null)
            return false;

        switch (equippedItem.ToolType)
        {
            case ToolType.Spade:
                return !HasActiveCrop && !isPrepared;
            case ToolType.SoilTester:
            case ToolType.WateringCan:
            case ToolType.FertilizerSpreader:
            case ToolType.HandHoe:
                return true;
            case ToolType.SeedPacket:
                return isPrepared && !HasActiveCrop && equippedItem.IsSeed;
            case ToolType.HarvestTool:
                return HasActiveCrop && growthProgressPercent >= 100f;
            default:
                return false;
        }
    }

    public string GetPrompt(HotbarItemSO equippedItem)
    {
        string inspectHint = $"[E] Inspect {fieldName}";

        if (equippedItem == null)
            return inspectHint;

        if (equippedItem.ToolType == ToolType.Spade)
        {
            if (HasActiveCrop)
                return $"Cannot prepare (Active Crop)\n{inspectHint}";
            if (isPrepared)
                return $"Soil Prepared\n{inspectHint}";

            return $"[Hold LMB] Prepare Soil ({equippedItem.ItemName})\n{inspectHint}";
        }

        if (equippedItem.ToolType == ToolType.SeedPacket && !HasActiveCrop && !isPrepared)
            return $"Prepare soil with Spade first\n{inspectHint}";

        if (!CanInteract(equippedItem))
            return inspectHint;

        return $"[LMB] Use {equippedItem.ItemName}\n{inspectHint}";
    }

    public void Interact(InteractionContext context)
    {
        if (context.EquippedItem == null)
            return;

        if (context.EquippedItem.ToolType == ToolType.Spade)
            TryPrepareField();
        else if (context.EquippedItem.ToolType == ToolType.SeedPacket && context.EquippedItem.IsSeed)
            TryPlantCrop(context.EquippedItem.AssociatedCrop);
    }
}