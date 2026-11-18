using UnityEngine;
using UnityEngine.EventSystems;

public class LocationOverviewHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PinRegionSelection owner;

    public void SetOwner(PinRegionSelection pinOwner)
    {
        owner = pinOwner;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (owner != null)
        {
            owner.NotifyOverviewEnter();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (owner != null)
        {
            owner.NotifyOverviewExit();
        }
    }
}