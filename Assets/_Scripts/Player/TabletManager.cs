using System;
using UnityEngine;
using UnityEngine.UI;

public class TabletManager : MonoBehaviour
{
    public static Action<bool> OnTabletToggled;

    [SerializeField] private GameObject tabletPanel;
    [SerializeField] private Button closeButton;

    private bool externalMenuOpen;
    private int suppressEscapeFrame = -1;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseTablet);
    }

    private void Update()
    {
        if (externalMenuOpen || suppressEscapeFrame == Time.frameCount)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tabletPanel.activeSelf)
            {
                CloseTablet();
                StartCoroutine(EnsureCursorLockedNextFrame());
            }
            else
                OpenTablet();
        }
    }

    private System.Collections.IEnumerator EnsureCursorLockedNextFrame()
    {
        yield return null;

        if (!tabletPanel.activeSelf && !externalMenuOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

    private void OnEnable()
    {
        OnTabletToggled += HandleMenuToggled;
    }

    private void OnDisable()
    {
        OnTabletToggled -= HandleMenuToggled;
    }

    private void HandleMenuToggled(bool isOpen)
    {
        if (isOpen && !tabletPanel.activeSelf)
            externalMenuOpen = true;
        else if (!isOpen)
        {
            externalMenuOpen = false;
            suppressEscapeFrame = Time.frameCount;
        }
    }
}