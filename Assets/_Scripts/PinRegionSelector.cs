using UnityEngine;
using System.Collections.Generic;

public class PinRegionSelection : MonoBehaviour
{
    [SerializeField] private float hoverScaleMultiplier = 1.3f;
    [SerializeField] private GameObject locationOverview;
    [SerializeField] private float scaleSpeed = 10f;
    [SerializeField] private float hoverExitDelay = 0.05f;

    private static HashSet<PinRegionSelection> hoveredPins = new HashSet<PinRegionSelection>();
    public static bool IsAnyPinHovered => hoveredPins.Count > 0;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool isHovered;
    private bool isOverviewHovered;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;

        if (locationOverview != null)
        {
            LocationOverviewHover hover = locationOverview.GetComponent<LocationOverviewHover>();
            if (hover == null)
            {
                hover = locationOverview.AddComponent<LocationOverviewHover>();
            }

            hover.SetOwner(this);
            locationOverview.SetActive(false);
        }
    }

    private void Update()
    {
        float smoothing = 1f - Mathf.Exp(-scaleSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, smoothing);
    }

    private void OnMouseEnter()
    {
        CancelInvoke(nameof(ClearHover));
        isHovered = true;
        UpdateHoverState();
        targetScale = originalScale * hoverScaleMultiplier;

        if (locationOverview != null)
        {
            locationOverview.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        CancelInvoke(nameof(ClearHover));
        isHovered = false;
        Invoke(nameof(ClearHover), hoverExitDelay);
    }

    public void NotifyOverviewEnter()
    {
        CancelInvoke(nameof(ClearHover));
        isOverviewHovered = true;
        UpdateHoverState();
    }

    public void NotifyOverviewExit()
    {
        CancelInvoke(nameof(ClearHover));
        isOverviewHovered = false;
        Invoke(nameof(ClearHover), hoverExitDelay);
    }

    private void UpdateHoverState()
    {
        if (isHovered || isOverviewHovered)
        {
            hoveredPins.Add(this);
        }
        else
        {
            hoveredPins.Remove(this);
        }
    }

    private void ClearHover()
    {
        if (isHovered || isOverviewHovered)
        {
            return;
        }

        hoveredPins.Remove(this);
        targetScale = originalScale;

        if (locationOverview != null)
        {
            locationOverview.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ClearHover));
        isHovered = false;
        isOverviewHovered = false;
        hoveredPins.Remove(this);

        if (locationOverview != null)
        {
            locationOverview.SetActive(false);
        }
    }
}