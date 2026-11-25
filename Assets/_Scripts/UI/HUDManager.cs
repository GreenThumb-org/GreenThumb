using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private void OnEnable()
    {
        TabletManager.OnTabletToggled += HandleMenuToggle;
    }

    private void OnDisable()
    {
        TabletManager.OnTabletToggled -= HandleMenuToggle;
    }

    private void HandleMenuToggle(bool isMenuOpen)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform c = transform.GetChild(i);
            if (c != null)
                c.gameObject.SetActive(!isMenuOpen);
        }
    }
}