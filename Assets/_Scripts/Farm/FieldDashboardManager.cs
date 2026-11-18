using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldDashboardManager : MonoBehaviour
{
    [SerializeField] private GameObject dashboardPanel;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI fieldNameText;
    [SerializeField] private TextMeshProUGUI cropInfoText;
    [SerializeField] private TextMeshProUGUI soilStatsText;
    [SerializeField] private TextMeshProUGUI weatherStatsText;
    [SerializeField] private TextMeshProUGUI warningText;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseDashboard);
    }

    private void OnEnable()
    {
        PlayerInteraction.OnFieldInteracted += OpenDashboard;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnFieldInteracted -= OpenDashboard;
    }

    private void Update()
    {
        if (dashboardPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDashboard();
        }
    }

    private void OpenDashboard(FieldData data)
    {
        PopulateData(data);
        
        dashboardPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        TabletManager.OnTabletToggled?.Invoke(true);
    }

    public void CloseDashboard()
    {
        dashboardPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        TabletManager.OnTabletToggled?.Invoke(false);
    }

    private void PopulateData(FieldData data)
    {
        fieldNameText.text = data.FieldName;
        cropInfoText.text = $"Crop: {data.CropType}\nStage: {data.GrowthStage}\nHealth: {data.Health:F1}%";
        soilStatsText.text = $"Moisture: {data.SoilMoisture:F1}%\nNitrogen: {data.NitrogenLevel:F0} kg/ha";
        weatherStatsText.text = $"Temp: {data.Temperature:F1} °C\nRain 24h: {data.Rainfall:F1} mm";
        
        if (data.PestRisk > 30f)
        {
            warningText.text = $"WARNING: Pest Risk High ({data.PestRisk:F0}%)";
            warningText.color = Color.red;
        }
        else
        {
            warningText.text = "Status: Optimal";
            warningText.color = Color.green;
        }
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(CloseDashboard);
    }
}