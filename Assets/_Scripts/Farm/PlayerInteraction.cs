using System;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static Action<FieldData> OnFieldInteracted;

    [SerializeField] private float interactRange = 4f;
    [SerializeField] private LayerMask interactableMask;

    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TextMeshProUGUI promptText;

    private Transform cam;

    private FieldData currentTarget;

    private void Awake()
    {
        promptText.gameObject.SetActive(false);
    }

    private void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        CheckForInteractable();
        HandleInteractionInput();
    }

    private void CheckForInteractable()
    {
        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, interactRange, interactableMask))
        {
            if (hit.collider.TryGetComponent(out FieldData fieldData))
            {
                currentTarget = fieldData;
                promptText.gameObject.SetActive(true);
                interactionPrompt.SetActive(true);
                promptText.text = $"[E] Inspect {currentTarget.FieldName}";
                return;
            }
        }

        currentTarget = null;
        interactionPrompt.SetActive(false);
        promptText.gameObject.SetActive(false);
    }

    private void HandleInteractionInput()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            OnFieldInteracted?.Invoke(currentTarget);
            interactionPrompt.SetActive(false);
            promptText.gameObject.SetActive(false);
        }
    }
}