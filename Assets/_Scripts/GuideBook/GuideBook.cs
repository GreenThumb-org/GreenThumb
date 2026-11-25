using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuideBook : MonoBehaviour
{
    public static GuideBook Instance { get; private set; }
    public static Action<bool> OnBookToggled;

    [Serializable]
    public struct BookPage
    {
        public string topic;
        [TextArea(4, 12)]
        public string info;
    }

    [Serializable]
    public struct ChapterEntry
    {
        public string chapterName;
        public Color tabColor;
        public int startPage;
        public int endPage;
    }

    [Header("Book Visibility")]
    [SerializeField] private GameObject bookRoot;
    [SerializeField] private KeyCode toggleKey = KeyCode.B;

    [Header("UI References")]
    [SerializeField] private TMP_Text topicText;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text pageCounterText;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button closeButton;

    [Header("Chapter Tabs")]
    [SerializeField] private Transform chapterNavigationParent;
    [SerializeField] private GameObject chapterButtonPrefab;
    [SerializeField] private Vector2 firstChapterPosition = new Vector2(175f, 175f);
    [SerializeField] private float yStep = -50f;

    [Header("Book Content (Inspector)")]
    [SerializeField] private List<ChapterEntry> chapters = new List<ChapterEntry>();
    [SerializeField] private List<BookPage> pages = new List<BookPage>();

    private readonly List<GameObject> spawnedChapterButtons = new List<GameObject>();
    private int currentPageIndex = 0;
    private bool isOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (bookRoot == null)
        {
            bookRoot = gameObject;
        }

        if (leftButton != null)
        {
            leftButton.onClick.AddListener(PreviousPage);
        }

        if (rightButton != null)
        {
            rightButton.onClick.AddListener(NextPage);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseBook);
        }

        RebuildChapterButtons();
        SetBookState(false);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }

        if (leftButton != null)
        {
            leftButton.onClick.RemoveListener(PreviousPage);
        }

        if (rightButton != null)
        {
            rightButton.onClick.RemoveListener(NextPage);
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseBook);
        }
    }

    private void OnValidate()
    {
        if (pages == null) return;

        for (int i = 0; i < chapters.Count; i++)
        {
            ChapterEntry ch = chapters[i];
            if (pages.Count == 0)
            {
                ch.startPage = 0;
                ch.endPage = 0;
            }
            else
            {
                ch.startPage = Mathf.Clamp(ch.startPage <= 0 ? 1 : ch.startPage, 1, pages.Count);
                ch.endPage = Mathf.Clamp(ch.endPage < ch.startPage ? ch.startPage : ch.endPage, ch.startPage, pages.Count);
            }
            chapters[i] = ch;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            SetBookState(!isOpen);
            return;
        }

        if (!isOpen) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreviousPage();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            NextPage();
        }
    }

    public void OpenBook()
    {
        SetBookState(true);
    }

    public void CloseBook()
    {
        SetBookState(false);
    }

    public void SetBookState(bool open)
    {
        isOpen = open;

        if (bookRoot != null)
        {
            bookRoot.SetActive(isOpen);
        }

        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
        OnBookToggled?.Invoke(isOpen);

        if (isOpen)
        {
            currentPageIndex = Mathf.Clamp(currentPageIndex, 0, Mathf.Max(0, pages.Count - 1));
            RefreshPageUI();
        }
    }

    public void NextPage()
    {
        if (pages.Count == 0) return;
        currentPageIndex = Mathf.Clamp(currentPageIndex + 1, 0, pages.Count - 1);
        RefreshPageUI();
    }

    public void PreviousPage()
    {
        if (pages.Count == 0) return;
        currentPageIndex = Mathf.Clamp(currentPageIndex - 1, 0, pages.Count - 1);
        RefreshPageUI();
    }

    public void GoToPage(int oneBasedPageNumber)
    {
        if (pages.Count == 0) return;
        currentPageIndex = Mathf.Clamp(oneBasedPageNumber - 1, 0, pages.Count - 1);
        RefreshPageUI();
    }

    [ContextMenu("Rebuild Chapter Buttons")]
    public void RebuildChapterButtons()
    {
        for (int i = spawnedChapterButtons.Count - 1; i >= 0; i--)
        {
            if (spawnedChapterButtons[i] != null)
            {
                if (Application.isPlaying)
                    Destroy(spawnedChapterButtons[i]);
                else
                    DestroyImmediate(spawnedChapterButtons[i]);
            }
        }
        spawnedChapterButtons.Clear();

        if (chapterButtonPrefab == null || chapterNavigationParent == null) return;

        for (int i = 0; i < chapters.Count; i++)
        {
            ChapterEntry chapter = chapters[i];
            GameObject btnObj = Instantiate(chapterButtonPrefab, chapterNavigationParent);
            btnObj.name = string.IsNullOrWhiteSpace(chapter.chapterName)
                ? $"ChapterButton_{i + 1}"
                : $"ChapterButton_{chapter.chapterName}";

            RectTransform rect = btnObj.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = new Vector2(
                    firstChapterPosition.x,
                    firstChapterPosition.y + (i * yStep)
                );
            }

            Image img = btnObj.GetComponent<Image>();
            if (img != null)
            {
                img.color = chapter.tabColor;
            }

            TMP_Text label = btnObj.GetComponentInChildren<TMP_Text>();
            if (label != null)
            {
                label.text = chapter.chapterName;
            }

            Button btn = btnObj.GetComponent<Button>();
            if (btn != null)
            {
                int targetPage = chapter.startPage;
                btn.onClick.AddListener(() => GoToPage(targetPage));
            }

            spawnedChapterButtons.Add(btnObj);
        }
    }

    private void RefreshPageUI()
    {
        if (pages.Count == 0)
        {
            if (topicText != null) topicText.text = string.Empty;
            if (infoText != null) infoText.text = string.Empty;
            if (pageCounterText != null) pageCounterText.text = "0 / 0";
            if (leftButton != null) leftButton.interactable = false;
            if (rightButton != null) rightButton.interactable = false;
            return;
        }

        BookPage page = pages[currentPageIndex];

        if (topicText != null)
        {
            topicText.text = page.topic;
        }

        if (infoText != null)
        {
            infoText.text = page.info;
        }

        if (pageCounterText != null)
        {
            pageCounterText.text = $"{currentPageIndex + 1} / {pages.Count}";
        }

        if (leftButton != null)
        {
            leftButton.interactable = currentPageIndex > 0;
        }

        if (rightButton != null)
        {
            rightButton.interactable = currentPageIndex < pages.Count - 1;
        }
    }
}