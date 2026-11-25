using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FieldDashboardManager : MonoBehaviour
{
    [Header("Panel & Core Controls")]

    [SerializeField] private GameObject dashboardPanel;
    [SerializeField] private Button closeButton;

    [Header("Telemetry UI")]

    [SerializeField] private TMP_Text fieldNameText;
    [SerializeField] private TMP_Text cropInfoText;
    [SerializeField] private TMP_Text soilStatsText;
    [SerializeField] private TMP_Text weatherStatsText;
    [SerializeField] private TMP_Text warningText;

    [Header("Planting Controls")]

    private List<CropSO> availableCrops = new List<CropSO>();
    [SerializeField] private List<CropSO> excludedCrops = new List<CropSO>();
    [SerializeField] private TMP_Text cropRequirementsText;
    [SerializeField] private TMP_Dropdown cropDropdown;
    [SerializeField] private Button plantButton;
    private TMP_Text plantButtonText;

    private bool openedThisFrame;
    private FieldData activeField;

    private string resourcesFolderName = "Crops";

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseDashboard);
        plantButton.onClick.AddListener(OnPlantButtonClicked);
        cropDropdown.onValueChanged.AddListener(OnCropDropdownChanged);
        LoadDataFromResources();
        PopulateCropDropdown();
        plantButtonText = plantButton.GetComponentInChildren<TMP_Text>();
    }

    private void OnCropDropdownChanged(int index)
    {
        if (activeField != null && !activeField.HasActiveCrop)
            RefreshUI();
    }

    private void LoadDataFromResources()
    {
        CropSO[] loadedData = Resources.LoadAll<CropSO>(resourcesFolderName);

        foreach (CropSO c in loadedData)
        {
            if (excludedCrops.Contains(c))
                continue;

            availableCrops.Add(c);
        }
    }

    private void OnEnable()
    {
        PlayerInteraction.OnFieldInteracted += OpenDashboard;
        PlayerInteraction.OnFieldLookAway += HandleLookAway;
    }

    private void OnDisable()
    {
        PlayerInteraction.OnFieldInteracted -= OpenDashboard;
        PlayerInteraction.OnFieldLookAway -= HandleLookAway;
    }

    private void Update()
    {
        if (!dashboardPanel.activeSelf)
            return;

        if (openedThisFrame)
        {
            openedThisFrame = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            CloseDashboard();
            return;
        }

        if (activeField != null)
            RefreshUI();
    }

    private void HandleLookAway()
    {
        if (dashboardPanel.activeSelf)
            CloseDashboard();
    }

    private void PopulateCropDropdown()
    {
        cropDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < availableCrops.Count; i++)
            options.Add(availableCrops[i].CropName);

        cropDropdown.AddOptions(options);
        cropDropdown.RefreshShownValue();
    }

    private void OpenDashboard(FieldData data)
    {
        activeField = data;
        openedThisFrame = true;
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

    private void OnPlantButtonClicked()
    {
        if (activeField == null || activeField.HasActiveCrop || availableCrops.Count == 0) return;

        CropSO selectedCrop = availableCrops[cropDropdown.value];
        if (activeField.TryPlantCrop(selectedCrop))
            RefreshUI();
    }

    private void RefreshUI()
    {
        fieldNameText.text = activeField.FieldName;
        CropSO displayedCrop = null;

        if (activeField.HasActiveCrop)
        {
            displayedCrop = activeField.ActiveCrop;
            cropInfoText.text = $"Crop: {activeField.CropType}\nStage: {activeField.GrowthStage} ({activeField.GrowthProgress:F0}%)\nHealth: {activeField.Health:F1}%";
            plantButton.interactable = false;
            cropDropdown.interactable = false;
            plantButtonText.text = "Field Occupied";
        }
        else if (!activeField.IsPrepared)
        {
            if (availableCrops.Count > 0)
                displayedCrop = availableCrops[cropDropdown.value];

            cropInfoText.text = "Crop: Unprepared Soil\nStage: Till with Spade first\nHealth: --";
            plantButton.interactable = false;
            cropDropdown.interactable = availableCrops.Count > 0;
            plantButtonText.text = "Soil Not Prepared";
        }
        else
        {
            if (availableCrops.Count > 0)
                displayedCrop = availableCrops[cropDropdown.value];

            cropInfoText.text = "Crop: Fallow (Prepared)\nStage: Ready for Seeding\nHealth: --";
            plantButton.interactable = availableCrops.Count > 0;
            cropDropdown.interactable = availableCrops.Count > 0;
            plantButtonText.text = "Plant Selected Crop";
        }

        if (displayedCrop != null)
            cropRequirementsText.text = displayedCrop.GetFormattedRequirements();
        else
            cropRequirementsText.text = "No crop requirements available.";

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
        plantButton.onClick.RemoveListener(OnPlantButtonClicked);
        cropDropdown.onValueChanged.RemoveListener(OnCropDropdownChanged);
    }
}