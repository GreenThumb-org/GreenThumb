using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUIManager : MonoBehaviour
{
    [Header("Visual Feedback")]
    [SerializeField] private Color defaultSlotColor = Color.white;
    [SerializeField] private Color selectedSlotColor = new Color(0.65f, 0.65f, 0.65f, 1f);
    [SerializeField] private TMP_Text equippedItemInfoText;

    private readonly List<Image> slotImages = new List<Image>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Image slotImage = transform.GetChild(i).GetComponent<Image>();
            if (slotImage != null)
                slotImages.Add(slotImage);
        }
    }

    private void OnEnable()
    {
        HotbarManager.OnSlotChanged += UpdateHotbarVisuals;
    }

    private void OnDisable()
    {
        HotbarManager.OnSlotChanged -= UpdateHotbarVisuals;
    }

    private void Start()
    {
        if (HotbarManager.Instance != null && HotbarManager.Instance.CurrentSlotIndex >= 0)
            UpdateHotbarVisuals(HotbarManager.Instance.CurrentSlotIndex, HotbarManager.Instance.EquippedItem);
    }

    private void UpdateHotbarVisuals(int selectedIndex, HotbarItemSO equippedItem)
    {
        for (int i = 0; i < slotImages.Count; i++)
            slotImages[i].color = i == selectedIndex ? selectedSlotColor : defaultSlotColor;

        if (equippedItemInfoText != null)
            equippedItemInfoText.text = equippedItem != null ? equippedItem.GetDisplayLabel() : "Empty";
    }
}