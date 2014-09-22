using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ColorPresetsManager : MonoBehaviour
{

    class ColorPage
    {
        public bool ContainsSelectedColor { get; set; }
        public int SelectedColorIndexInPage { get; set; }
        public List<Color> PageColors { get; set; }
    }

    public static Color SelectedPresetColor { get; private set; }

    public GUITexture[] presetsDisplay;
    public Color[] defaultColors;
    public ColorPresetsBrowsingButton nextPageButton;
    public ColorPresetsBrowsingButton previousPageButton;
    public float buttonsScalingRatio = 1.2f;
    private bool nextPageAvailable;
    public static bool SelectedColorHasBeenInitialized { get; private set; }
    public bool NextPageAvailable
    {
        get { return selectedPageIndex < colorPages.Count - 1; }
        private set { nextPageAvailable = value; }
    }
    private bool previousPageAvailable;
    public bool PreviousPageAvailable
    {
        get { return selectedPageIndex > 0; }
        private set { previousPageAvailable = value; }
    }
    int selectedPageIndex;
    static string COLOR_PRESETS_KEY = "colorPresetsKey";
    Color[] savedPresets;
    List<ColorPage> colorPages;
    Vector3 initialButtonsScale;

    void Awake()
    {
        initialButtonsScale = presetsDisplay[0].transform.localScale;
        nextPageButton.PresetsManage = this;
        nextPageButton.ReverseBrowsing = false;
        previousPageButton.PresetsManage = this;
        previousPageButton.ReverseBrowsing = true;
        ReInitializePresetsBrowser();
        DisplayPage(0);
        previousPageButton.EnableButton(false);
        if (!NextPageAvailable)
            nextPageButton.EnableButton(false);
        for (int i = 0; i < presetsDisplay.Length; i++)
        {
            var b = presetsDisplay[i].gameObject.GetComponent<ColorPresetButton>();
            if (b != null)
            {
                b.ButtonIndex = i;
                b.PresetsManager = this;
            }
        }
    }

    public void ChangeSelectedPresetColor(int presetButtonIndex)
    {
        if (!SelectedColorHasBeenInitialized)
            SelectedColorHasBeenInitialized = true;
        var page = colorPages.ElementAt(selectedPageIndex);
        if (!page.ContainsSelectedColor || presetButtonIndex != page.SelectedColorIndexInPage)
        {
            for (int i = 0; i < colorPages.Count; i++)
            {
                var cp = colorPages.ElementAt(i);
                cp.ContainsSelectedColor = false;
            }
            SelectedPresetColor = page.PageColors.ElementAt(presetButtonIndex);
            page.ContainsSelectedColor = true;
            page.SelectedColorIndexInPage = presetButtonIndex;
            presetsDisplay[presetButtonIndex].transform.localScale *= buttonsScalingRatio;
            for (int i = 0; i < presetsDisplay.Length; i++)
            {
                if (i != presetButtonIndex)
                    presetsDisplay[i].transform.localScale = initialButtonsScale;
            }
        }
    }

    void ReInitializePresetsBrowser()
    {
        SavePresets();
        colorPages = new List<ColorPage>();
        savedPresets = PlayerPrefsX.GetColorArray(COLOR_PRESETS_KEY);
        if (savedPresets == null || savedPresets.Length == 0)
            savedPresets = defaultColors;
        int pagesCount = Mathf.CeilToInt((float)savedPresets.Length / (float)presetsDisplay.Length);
        int presetIndex = 0;
        int remainingPresetsCount = savedPresets.Length;
        for (int i = 0; i < pagesCount; i++)
        {
            var cp = new ColorPage()
            {
                ContainsSelectedColor = false,
                PageColors = new List<Color>()
            };
            int j = 0;
            while (presetIndex < savedPresets.Length && j < presetsDisplay.Length)
            {
                cp.PageColors.Add(savedPresets[presetIndex]);
                presetIndex++;
                j++;
            }
            colorPages.Add(cp);
        }
    }

    bool DisplayPage(int pageNumber)
    {
        if (pageNumber < colorPages.Count)
        {
            selectedPageIndex = pageNumber;
            for (int i = 0; i < presetsDisplay.Length; i++)
            {
                if (!presetsDisplay[i].gameObject.activeSelf)
                    presetsDisplay[i].gameObject.SetActive(true);
                presetsDisplay[i].transform.localScale = initialButtonsScale;
            }
            var selectedPage = colorPages.ElementAt(selectedPageIndex);
            for (int i = selectedPage.PageColors.Count; i < presetsDisplay.Length; i++)
            {
                presetsDisplay[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < selectedPage.PageColors.Count; i++)
            {
                presetsDisplay[i].guiTexture.color = selectedPage.PageColors.ElementAt(i);
            }
            if (selectedPage.ContainsSelectedColor)
                presetsDisplay[selectedPage.SelectedColorIndexInPage].transform.localScale *= buttonsScalingRatio;
            return true;
        }
        return false;
    }

    public void DisplayNextPage()
    {
        if (DisplayPage(selectedPageIndex + 1))
        {
            if (!NextPageAvailable)
                nextPageButton.EnableButton(false);
            if (!previousPageButton.ButtonEnabled)
                previousPageButton.EnableButton(true);
        }
    }

    public void DisplayPreviousPage()
    {
        if (DisplayPage(selectedPageIndex - 1))
        {
            if (!PreviousPageAvailable)
                previousPageButton.EnableButton(false);
            if (!nextPageButton.ButtonEnabled)
                nextPageButton.EnableButton(true);
        }
    }

    private void SavePresets()
    {
        if (colorPages != null)
        {
            int colorsCount = 0;
            foreach (var cp in colorPages)
            {
                colorsCount += cp.PageColors.Count;
            }
            var colors = new List<Color>();
            foreach (var cp in colorPages)
            {
                colors = colors.Concat(cp.PageColors).ToList();
            }
            var presets = new Color[colorsCount];
            for (int i = 0; i < colors.Count; i++)
            {
                presets[i] = colors.ElementAt(i);
            }
            PlayerPrefsX.SetColorArray(COLOR_PRESETS_KEY, presets);
        }
    }

    public void AddNewColor(Color newColor)
    {
        newColor.a = 1;
        var lastPageIndex = colorPages.Count - 1;
        var lastPage = colorPages.ElementAt(lastPageIndex);
        if (lastPage.PageColors.Count < presetsDisplay.Length)
        {
            lastPage.PageColors.Add(newColor);
            lastPage.SelectedColorIndexInPage = lastPage.PageColors.Count - 1;
            lastPage.ContainsSelectedColor = true;
            DisplayPage(lastPageIndex);
            nextPageButton.EnableButton(NextPageAvailable);
            previousPageButton.EnableButton(PreviousPageAvailable);
        }
        else
        {
            var cp = new ColorPage()
            {
                ContainsSelectedColor = true,
                PageColors = new List<Color>(),
                SelectedColorIndexInPage = 0
            };
            cp.PageColors.Add(newColor);
            colorPages.Add(cp);
            DisplayPage(colorPages.Count - 1);
            if (!previousPageButton.ButtonEnabled)
                previousPageButton.EnableButton(true);
            if (nextPageButton.ButtonEnabled)
                nextPageButton.EnableButton(false);
        }
        SelectedPresetColor = newColor;
        SavePresets();
    }

    public void DeleteSelectedColor()
    {
        var cp = colorPages.ElementAt(selectedPageIndex);
        if (cp.ContainsSelectedColor)
        {
            cp.PageColors.RemoveAt(cp.SelectedColorIndexInPage);
            ReInitializePresetsBrowser();
            if (selectedPageIndex < colorPages.Count)
                DisplayPage(selectedPageIndex);
            else
                DisplayPage(selectedPageIndex - 1);
            nextPageButton.EnableButton(NextPageAvailable);
            previousPageButton.EnableButton(PreviousPageAvailable);
        }
    }

}
