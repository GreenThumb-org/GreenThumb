using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static Action<FieldData> OnFieldInteracted;
    public static Action OnFieldLookAway;

    [SerializeField] private float interactRange = 4f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMP_Text promptText;

    private Transform cam;

    private FieldData currentTarget;
    private bool isMenuOpen;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        TabletManager.OnTabletToggled += HandleMenuToggle;
    }

    private void OnDisable()
    {
        TabletManager.OnTabletToggled -= HandleMenuToggle;
    }

    private void Update()
    {
        CheckForInteractable();
        HandleInteractionInput();
    }

    private void HandleMenuToggle(bool menuOpen)
    {
        isMenuOpen = menuOpen;

        if (isMenuOpen)
        {
            promptText.gameObject.SetActive(false);
            interactionPrompt.SetActive(false);
        }
    }

    private void CheckForInteractable()
    {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, interactRange, interactableMask))
        {
            if (hit.collider.TryGetComponent(out FieldData fieldData))
            {
                if (currentTarget != null && currentTarget != fieldData)
                    OnFieldLookAway?.Invoke();

                currentTarget = fieldData;

                if (!isMenuOpen)
                {
                    interactionPrompt.SetActive(true);
                    promptText.gameObject.SetActive(true);
                    promptText.text = $"[E] Inspect {currentTarget.FieldName}";
                }
                else
                {
                    interactionPrompt.SetActive(false);
                    promptText.gameObject.SetActive(false);
                }
                return;
            }
        }

        if (currentTarget != null)
        {
            currentTarget = null;
            OnFieldLookAway?.Invoke();
        }

        interactionPrompt.SetActive(false);
        promptText.gameObject.SetActive(false);
    }

    private void HandleInteractionInput()
    {
        if (!isMenuOpen && currentTarget != null && Input.GetKeyDown(KeyCode.E))
            OnFieldInteracted?.Invoke(currentTarget);
    }
}