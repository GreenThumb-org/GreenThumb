using System;
using UnityEngine;
using UnityEngine.UI;

public class TabletManager : MonoBehaviour
{
    public static Action<bool> OnTabletToggled;

    [SerializeField] private GameObject tabletPanel;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseTablet);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tabletPanel.activeSelf)
                CloseTablet();
            else
                OpenTablet();
        }
    }

    private void OpenTablet()
    {
        tabletPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnTabletToggled?.Invoke(true);
    }

    public void CloseTablet()
    {
        tabletPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnTabletToggled?.Invoke(false);
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(CloseTablet);
    }
}