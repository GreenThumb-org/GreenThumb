using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image holdProgressBar;

    private Transform cam;
    private IInteractable currentTarget;
    private FieldData currentField;
    private RaycastHit currentHit;
    private bool isMenuOpen;
    private float holdTimer;
    private HotbarItemSO holdItem;

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
        {
            ResetHold();
            SetPromptVisible(false);
        }
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
                if (currentTarget != interactable)
                    ResetHold();

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

        ResetHold();
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

        HotbarItemSO equipped = HotbarManager.Instance != null ? HotbarManager.Instance.EquippedItem : null;

        if (!currentTarget.CanInteract(equipped))
        {
            ResetHold();
            return;
        }

        float requiredDuration = currentTarget.GetInteractionDuration(equipped);

        if (requiredDuration <= 0f)
        {
            if (Input.GetKeyDown(interactKey))
                ExecuteInteraction(equipped);

            return;
        }

        if (Input.GetKey(interactKey))
        {
            if (holdItem != equipped)
            {
                holdItem = equipped;
                holdTimer = 0f;
            }

            holdTimer += Time.deltaTime;
            UpdateHoldUI(holdTimer / requiredDuration);

            if (holdTimer >= requiredDuration)
            {
                ExecuteInteraction(equipped);
                ResetHold();
            }
        }
        else if (holdTimer > 0f)
            ResetHold();
    }

    private void ExecuteInteraction(HotbarItemSO equipped)
    {
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

    private void ResetHold()
    {
        holdTimer = 0f;
        holdItem = null;
        UpdateHoldUI(0f);
    }

    private void UpdateHoldUI(float normalizedProgress)
    {
        if (holdProgressBar == null)
            return;

        bool showBar = normalizedProgress > 0f;
        holdProgressBar.gameObject.SetActive(showBar);
        holdProgressBar.fillAmount = Mathf.Clamp01(normalizedProgress);
    }

    private void SetPromptVisible(bool visible)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(visible);

        if (promptText != null)
            promptText.gameObject.SetActive(visible);

        if (!visible && holdProgressBar != null)
            holdProgressBar.gameObject.SetActive(false);
    }
}