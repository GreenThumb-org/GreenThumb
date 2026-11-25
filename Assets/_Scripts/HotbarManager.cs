using System;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance { get; private set; }
    public static Action<int, HotbarItemSO> OnSlotChanged;

    [Header("Hotbar Configuration")]
    private List<HotbarItemSO> availableHotbarSlots = new List<HotbarItemSO>();
    [SerializeField] private List<HotbarItemSO> excludedHotbarSlots = new List<HotbarItemSO>();
    [SerializeField] private int defaultSlotIndex = 0;

    [Header("First-Person Equip")]
    [SerializeField] private Transform itemHoldPoint;

    private int currentSlotIndex = -1;
    private GameObject currentEquippedObject;
    private bool isInputBlocked;

    public int CurrentSlotIndex => currentSlotIndex;
    public int SlotCount => availableHotbarSlots.Count;
    public HotbarItemSO EquippedItem => IsValidSlot(currentSlotIndex) ? availableHotbarSlots[currentSlotIndex] : null;
    public ToolType EquippedToolType => EquippedItem != null ? EquippedItem.ToolType : ToolType.None;
    public CropSO EquippedSeedCrop => EquippedItem != null && EquippedItem.IsSeed ? EquippedItem.AssociatedCrop : null;

    private string resourcesFolderName = "HotbarSlots";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadDataFromResources();
    }

    private void LoadDataFromResources()
    {
        HotbarItemSO[] loadedData = Resources.LoadAll<HotbarItemSO>(resourcesFolderName);

        foreach (HotbarItemSO c in loadedData)
        {
            if (excludedHotbarSlots.Contains(c))
                continue;

            availableHotbarSlots.Add(c);
        }
    }

    private void OnEnable()
    {
        TabletManager.OnTabletToggled += HandleMenuToggle;
    }

    private void OnDisable()
    {
        TabletManager.OnTabletToggled -= HandleMenuToggle;
    }

    private void Start()
    {
        if (availableHotbarSlots.Count > 0)
            SelectSlot(Mathf.Clamp(defaultSlotIndex, 0, availableHotbarSlots.Count - 1));
    }

    private void Update()
    {
        if (isInputBlocked || availableHotbarSlots.Count == 0)
            return;

        HandleNumberKeys();
        HandleScrollWheel();
    }

    private void HandleMenuToggle(bool isMenuOpen)
    {
        isInputBlocked = isMenuOpen;
    }

    private void HandleNumberKeys()
    {
        int maxKeys = Mathf.Min(availableHotbarSlots.Count, 9);

        for (int i = 0; i < maxKeys; i++)
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectSlot(i);
    }

    private void HandleScrollWheel()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Approximately(scroll, 0f))
            return;

        int direction = scroll < 0f ? 1 : -1;
        int nextIndex = (currentSlotIndex + direction + availableHotbarSlots.Count) % availableHotbarSlots.Count;
        SelectSlot(nextIndex);
    }

    public void SelectSlot(int index)
    {
        if (!IsValidSlot(index) || index == currentSlotIndex)
            return;

        currentSlotIndex = index;
        EquipVisual(availableHotbarSlots[currentSlotIndex]);
        OnSlotChanged?.Invoke(currentSlotIndex, availableHotbarSlots[currentSlotIndex]);
    }

    public bool HasEquippedTool(ToolType requiredType)
    {
        return EquippedToolType == requiredType;
    }

    private void EquipVisual(HotbarItemSO item)
    {
        if (currentEquippedObject != null)
            Destroy(currentEquippedObject);

        if (item == null || item.HeldPrefab == null || itemHoldPoint == null)
            return;

        currentEquippedObject = Instantiate(item.HeldPrefab, itemHoldPoint);
        currentEquippedObject.transform.localPosition = Vector3.zero;
        currentEquippedObject.transform.localRotation = Quaternion.identity;
    }

    private bool IsValidSlot(int index)
    {
        return index >= 0 && index < availableHotbarSlots.Count;
    }
}