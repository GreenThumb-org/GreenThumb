using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static Action<FieldData> OnFieldInteracted;
    public static Action OnFieldLookAway;
    public static Action<InteractionContext> OnInteractionPerformed;

    [SerializeField] private float interactRange = 4f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private KeyCode inspectKey = KeyCode.E;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TMP_Text promptText;

    private Transform cam;
    private IInteractable currentTarget;
    private FieldData currentField;
    private RaycastHit currentHit;
    private bool isMenuOpen;

    private void Awake()
    {
        Camera childCam = GetComponentInChildren<Camera>();
        if (childCam != null)
            cam = childCam.transform;
        else if (Camera.main != null)
            cam = Camera.main.transform;
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
            SetPromptVisible(false);
    }

    private void CheckForInteractable()
    {
        if (cam == null)
            return;

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, interactRange, interactableMask))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                FieldData hitField = interactable.ParentField;

                if (currentField != null && currentField != hitField)
                    OnFieldLookAway?.Invoke();

                currentTarget = interactable;
                currentField = hitField;
                currentHit = hit;

                if (!isMenuOpen)
                {
                    HotbarItemSO equipped = HotbarManager.Instance != null ? HotbarManager.Instance.EquippedItem : null;
                    SetPromptVisible(true);
                    if (promptText != null)
                        promptText.text = currentTarget.GetPrompt(equipped);
                }
                else
                    SetPromptVisible(false);

                return;
            }
        }

        if (currentField != null)
            OnFieldLookAway?.Invoke();

        currentTarget = null;
        currentField = null;
        SetPromptVisible(false);
    }

    private void HandleInteractionInput()
    {
        if (isMenuOpen || currentTarget == null)
            return;

        if (currentField != null && Input.GetKeyDown(inspectKey))
            OnFieldInteracted?.Invoke(currentField);

        if (Input.GetKeyDown(interactKey))
        {
            HotbarItemSO equipped = HotbarManager.Instance != null ? HotbarManager.Instance.EquippedItem : null;

            if (!currentTarget.CanInteract(equipped))
                return;

            InteractionContext context = new InteractionContext(
                currentTarget,
                currentField,
                equipped,
                currentHit.point,
                currentHit.normal
            );

            currentTarget.Interact(context);
            OnInteractionPerformed?.Invoke(context);
        }
    }

    private void SetPromptVisible(bool visible)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(visible);

        if (promptText != null)
            promptText.gameObject.SetActive(visible);
    }
}