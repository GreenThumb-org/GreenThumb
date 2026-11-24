using UnityEngine;

public enum ToolType
{
    None,
    Spade,
    SoilTester,
    CropInspector,
    SeedPacket,
    WateringCan,
    FertilizerSpreader,
    HandHoe,
    HarvestTool
}

[CreateAssetMenu(fileName = "NewHotbarItem", menuName = "Greenthumb/Hotbar Item")]
public class HotbarItemSO : ScriptableObject
{
    [SerializeField] private string itemName = "Spade";
    [SerializeField] private ToolType toolType = ToolType.Spade;
    [SerializeField] private GameObject heldPrefab;
    [SerializeField] private Sprite icon;
    [SerializeField] private CropSO associatedCrop;
    [SerializeField] private string operationDescription = "Till and prepare soil";

    public string ItemName => itemName;
    public ToolType ToolType => toolType;
    public GameObject HeldPrefab => heldPrefab;
    public Sprite Icon => icon;
    public CropSO AssociatedCrop => associatedCrop;
    public string OperationDescription => operationDescription;
    public bool IsSeed => toolType == ToolType.SeedPacket && associatedCrop != null;

    public string GetDisplayLabel()
    {
        if (IsSeed)
            return $"{itemName} ({associatedCrop.CropName}) - {operationDescription}";

        return $"{itemName} - {operationDescription}";
    }
}