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

    private FieldData activeField;

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
        if (!dashboardPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseDashboard();
            return;
        }

        if (activeField != null)
            RefreshUI();
    }
    private void OpenDashboard(FieldData data)
    {
        activeField = data;
        RefreshUI();

        dashboardPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        TabletManager.OnTabletToggled?.Invoke(true);
    }

    public void CloseDashboard()
    {
        activeField = null;
        dashboardPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        TabletManager.OnTabletToggled?.Invoke(false);
    }

    private void RefreshUI()
    {
        fieldNameText.text = activeField.FieldName;
        cropInfoText.text = $"Crop: {activeField.CropType}\nStage: {activeField.GrowthStage}\nHealth: {activeField.Health:F1}%";
        soilStatsText.text = $"Moisture: {activeField.SoilMoisture:F1}%\nNitrogen: {activeField.NitrogenLevel:F0} kg/ha";

        WeatherDataPoint currentWeather = WeatherDataManager.Manager != null ? WeatherDataManager.Manager.CurrentDataPoint : null;

        if (currentWeather != null)
            weatherStatsText.text = $"Condition: {currentWeather.weather}\nTemp: {currentWeather.temprature:F1} °C\nPrecip: {currentWeather.precipitation:F1} mm\nHumidity: {currentWeather.humidity:F0}%";
        else
            weatherStatsText.text = "Weather Data Unavailable";

        if (activeField.PestRisk > 30f)
        {
            warningText.text = $"WARNING: Pest Risk High ({activeField.PestRisk:F0}%)";
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