using UnityEngine;

[CreateAssetMenu(fileName = "NewCrop", menuName = "Greenthumb/Crop Data")]
public class CropSO : ScriptableObject
{
    [SerializeField] private string cropName = "Wheat";
    [SerializeField] private GameObject cropPrefab;
    [SerializeField] private float growthDurationHours = 72f;
    [SerializeField] private float nitrogenConsumptionPerHour = 0.2f;
    [SerializeField] private float waterConsumptionPerHour = 0.3f;

    public string CropName => cropName;
    public GameObject CropPrefab => cropPrefab;
    public float GrowthDurationHours => growthDurationHours;
    public float NitrogenConsumptionPerHour => nitrogenConsumptionPerHour;
    public float WaterConsumptionPerHour => waterConsumptionPerHour;

    [Header("Climate, Soil & Growing Requirements")]
    [SerializeField] private string climateType = "Temperate";
    [SerializeField] private Vector2 optimalTempRangeC = new Vector2(12f, 22f);
    [SerializeField] private string preferredSoil = "Clay Loam";
    [SerializeField] private Vector2 optimalMoistureRange = new Vector2(40f, 65f);
    [SerializeField] private float requiredNitrogenKgHa = 110f;
    [TextArea(2, 4)]
    [SerializeField] private string growingRequirements = "Full sun, dry conditions preferred during grain ripening.";

    public string ClimateType => climateType;
    public Vector2 OptimalTempRangeC => optimalTempRangeC;
    public string PreferredSoil => preferredSoil;
    public Vector2 OptimalMoistureRange => optimalMoistureRange;
    public float RequiredNitrogenKgHa => requiredNitrogenKgHa;
    public string GrowingRequirements => growingRequirements;

    public string GetFormattedRequirements() =>
         $"Climate: {climateType} ({optimalTempRangeC.x:F0}–{optimalTempRangeC.y:F0} °C)\n" + $"Soil: {preferredSoil} | Moisture: {optimalMoistureRange.x:F0}–{optimalMoistureRange.y:F0}% | Min N: {requiredNitrogenKgHa:F0} kg/ha\n" + $"Growing: {growingRequirements}";
}